using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    GameObject itemSObj;
    [SerializeField]
    GameObject itemUObj;

    public GameObject player;
    public GameObject playerUmbrella;

    public GameManager gameManager;
    public ObjectManager objectManager;
    // Start is called before the first frame update

    void OnCollisionEnter2D(Collision2D co)
    {
        if (co.gameObject.tag == "Player" && this.name == "ItemSpeed(Clone)")
        {
            SpeedUp();
            DeActive();
        }else if (co.gameObject.tag == "Player" && this.name == "ItemUmbrella(Clone)")
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.umbrellaNum--;
            playerUmbrella = playerLogic.playerUmbrella;
            playerUmbrella.SetActive(true);
            DeActive();
        }
        if(co.gameObject.tag == "Ground")
        {
            DeActive();
        }
    }
    private void SpeedUp()
    {
        Player playerLogic = player.GetComponent<Player>();
        playerLogic.maxSpeed = playerLogic.maxSpeed + 0.2f;
    }
    void DeActive()
    {
        gameObject.SetActive(false);
    }

}
