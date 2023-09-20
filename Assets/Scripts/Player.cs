using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public int umbrellaNum;

    public GameObject playerUmbrella;

    public Transform groundCheck;
    public float isGroundRadius;
    public LayerMask groundLayer;
    private bool isGround;

    public int score = 0;

    private bool isControl;
    public bool []btnControl;

    public GameManager gameManager;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        playerUmbrella.SetActive(false);
    }

    void Update()
    {
        playerUmbrella.transform.position = new Vector3(rigid.position.x, rigid.position.y + 0.5f, 1);

        isGround = Physics2D.OverlapCircle(groundCheck.position, isGroundRadius, groundLayer);
        float h = Input.GetAxisRaw("Horizontal");
        if (btnControl[0] && !isControl)
        {
            anim.SetBool("isWalking", true);
            rigid.AddForce(Vector2.right * -1  * h, ForceMode2D.Impulse);
            rigid.velocity = new Vector2(maxSpeed* -1, rigid.velocity.y);
            spriteRenderer.flipX = true;
        }
        else if (btnControl[1] && isGround && !isControl) // jump
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }else if (btnControl[2] && !isControl)
        {
            anim.SetBool("isWalking", true);
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            spriteRenderer.flipX = false;
        }
        if(rigid.velocity.normalized.x == 0)
        {
            anim.SetBool("isWalking", false);
        }

    }
    public void BtnJumpOnClick()
    {
        Debug.Log("มกวม");
        isGround = Physics2D.OverlapCircle(groundCheck.position, isGroundRadius, groundLayer);
        if (isGround)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    public void BtnPanel(int type)
    {
        for(int index = 0; index < 3; index++)
        {
            btnControl[index] = index == type;
        }
    }
    public void ButtonUp()
    {
        isControl = true;
    }
    public void ButtonDown()
    {
        isControl = false;
    }

    public void PlusScore(int a)
    {
        score += a;
    }
    void FixedUpdate()
    {
        //Keyboard Move
        
    }


    void OnCollisionEnter2D(Collision2D co)
    {
        if (co.gameObject.tag == "Rain")
        {
            gameManager.GameOver();
        }
    }
    
    void OnTriggerEnter2D(Collider2D co)
    {
        if (co.gameObject.tag == "Water")
        {
            maxSpeed--;
        }
    }
    void OnTriggerExit2D(Collider2D co)
    {
        maxSpeed++;
    }

}
