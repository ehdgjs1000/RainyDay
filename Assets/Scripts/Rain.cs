using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    [SerializeField]
    GameObject rainObj;

    public GameObject player;
    CapsuleCollider2D rainCollider;
    Rigidbody2D rigid;
    private float rainRotation;

    public GameManager gameManager;
    public ObjectManager objectManager;

    private int umbrellaHP = 5;

    void OnCollisionEnter2D(Collision2D co)
    {
        if (co.gameObject.tag == "Player")
        {
            DeActive();
        }
        else if (co.gameObject.tag == "Ground")
        {
            OnCrash();
        }
        else if (co.gameObject.tag == "Umbrella")
        {
            DeActive();
            GameObject umbrella = GameObject.FindGameObjectWithTag("Umbrella");
            umbrella.SetActive(false);
        }
    }
    public void OnCrash()
    {
        Player playerLogic = player.GetComponent<Player>();
        playerLogic.PlusScore(1);
        DeActive();
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }

}
