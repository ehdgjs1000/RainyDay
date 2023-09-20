using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Animator anim;
    CapsuleCollider2D rainCollider;
    Rigidbody2D rigid;

    private float rainDuration;
    public float rainRotation;
    private float rainTerm;
    private float rainPos;
    private float lightningPos;
    private float itemSPos;
    private float itemUPos;
    public int lightningNum;
    public int gameLevel;


    public double seasonNum;
    private int season;

    public ObjectManager objectManager;
    public GameObject wind;

    public GameObject gameOverSet;
    public GameObject player;

    public Text scoreTxt;
    public Text seasonTxt;
    public Text windSpeed;
    public GameObject waterTile;
    public Text finalScore;

    public GameObject[] backGrounds;
    void Update()
    {
        Player playerLogic = player.GetComponent<Player>();
        scoreTxt.text = string.Format("{0:n0}", playerLogic.score);
    }
    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        rainCollider = GetComponent<CapsuleCollider2D>();
        season = 0;
        gameLevel = 1;

        SpawnItemS();
        SpawnItemU();
        Invoke("SpawnRain", 1f);
    }
    void FixedUpdate()
    {
        RainChange();
        SeasonCheck();
        if(seasonNum == 1)
        {
            lightningNum = Random.Range(1, 500);
            if (lightningNum == 1)
            {
                SpawnLightning();
            }
        }
    }
    private void SpawnItem()
    {
        int a = Random.Range(0, 2);
        if(a == 0)
        {
            SpawnItemS();
        }else
        {
            SpawnItemU();
        }
    }

    public void SpawnItemU()
    {
        itemUPos = Random.Range(-9f, 7f);
        GameObject itemU = objectManager.MakeObj("itemU");
        itemU.transform.position = new Vector3(itemUPos, 10, 0);

        Rigidbody2D rigidItemU = itemU.GetComponent<Rigidbody2D>();
        Item itemLogic = itemU.GetComponent<Item>();

        itemLogic.player = player;
        itemLogic.gameManager = this;
        itemLogic.objectManager = objectManager;
    }

    public void SpawnItemS()
    {
        itemSPos = Random.Range(-9f, 7f);
        GameObject itemS = objectManager.MakeObj("itemS");
        itemS.transform.position = new Vector3(itemSPos, 10, 0);

        Rigidbody2D rigidItemS = itemS.GetComponent<Rigidbody2D>();
        Item itemLogic = itemS.GetComponent<Item>();

        itemLogic.player = player;
        itemLogic.gameManager = this;
        itemLogic.objectManager = objectManager;
    }
    public void SpawnLightning()
    {
        lightningPos = Random.Range(-9f, 7f);
        GameObject lightning = objectManager.MakeObj("lightning");
        lightning.transform.position = new Vector3(lightningPos, 15, 0);

        Rigidbody2D rigidLightning = lightning.GetComponent<Rigidbody2D>();
        Rain lightningLogic = lightning.GetComponent<Rain>();

        lightningLogic.player = player;
        lightningLogic.gameManager = this;
        lightningLogic.objectManager = objectManager;
    }
    public void SpawnRain()
    {
        rainPos = Random.Range(-9f, 7f);
        GameObject rain = objectManager.MakeObj("rain");
        rain.transform.position = new Vector3(rainPos, 10, 0);

        Rigidbody2D rigidRain = rain.GetComponent<Rigidbody2D>();
        Rain rainLogic = rain.GetComponent<Rain>();

        rainLogic.player = player;
        rainLogic.gameManager = this;
        rainLogic.objectManager = objectManager;

        // Rain Falling Controll
        rigidRain.transform.rotation = Quaternion.Euler(0, 0, rainRotation);

        //Rain Falling Term
        if (season == 0)
        {
            rainTerm = Random.Range(0.2f, 0.4f);
            rigidRain.AddForce(new Vector3(rainRotation / 10, 0, 0), ForceMode2D.Impulse);
        }
        else if(season == 1)
        {
            rainTerm = 0.1f; //more rain
            rigidRain.AddForce(new Vector3(rainRotation / 10, 0, 0), ForceMode2D.Impulse);
        }
        else if (season == 2)
        {
            rainTerm = Random.Range(0.2f, 0.4f);
            rigidRain.AddForce(new Vector3(rainRotation / 4, 0, 0), ForceMode2D.Impulse);
        }
        else if (season == 3)
        {
            rainTerm = Random.Range(0.2f, 0.4f);
            rigidRain.AddForce(new Vector3(rainRotation / 10, 0, 0), ForceMode2D.Impulse);
        }

        
        Invoke("SpawnRain", (rainTerm * 2) /gameLevel);
    }

    public void RainChange()
    {
        Wind windLogic = wind.GetComponent<Wind>();
        rainRotation = windLogic.ReturnWindRotation();
        if(season == 2)
        {
            if(rainRotation < 0)
            {
                rainRotation = rainRotation - 5f;
            }else if(rainRotation >= 0)
            {
                rainRotation = rainRotation + 5f;
            }
        }
        rainDuration = windLogic.ReturnWindDuration();


        windSpeed.text = Mathf.Abs(rainRotation).ToString("F1");
    }
    public void SeasonCheck()
    {
        //# 0. 봄  1. 여름 2. 가을 3. 겨울
        Player playerLogic = player.GetComponent<Player>();
        seasonNum = System.Math.Truncate((double)playerLogic.score / 100);
        if(gameLevel != seasonNum + 1)
        {
            SpawnItem();
        }
        gameLevel = (int)seasonNum + 1;


        if ((int)seasonNum % 4 == 0)
        {
            seasonNum = 0;
            backGrounds[0].SetActive(true);
            backGrounds[1].SetActive(false);
            backGrounds[2].SetActive(false);
            backGrounds[3].SetActive(false);
            seasonTxt.text = "봄";
            seasonTxt.color = Color.green;
        }
        else if ((int)seasonNum % 4 == 1)
        {
            seasonNum = 1;
            backGrounds[0].SetActive(false);
            backGrounds[1].SetActive(true);
            backGrounds[2].SetActive(false);
            backGrounds[3].SetActive(false);
            seasonTxt.text = "여름";
            seasonTxt.color = Color.blue;
            waterTile.SetActive(true);
        }
        else if ((int)seasonNum % 4 == 2)
        {
            seasonNum = 2;
            backGrounds[0].SetActive(false);
            backGrounds[1].SetActive(false);
            backGrounds[2].SetActive(true);
            backGrounds[3].SetActive(false);
            seasonTxt.text = "가을";
            seasonTxt.color = Color.yellow;
            waterTile.SetActive(false);
        }
        else if ((int)seasonNum % 4 == 3)
        {
            seasonNum = 3;
            backGrounds[0].SetActive(false);
            backGrounds[1].SetActive(false);
            backGrounds[2].SetActive(false);
            backGrounds[3].SetActive(true);
            seasonTxt.text = "겨울";
            seasonTxt.color = Color.white;
        }

        Invoke("SeasonCheck", 1f);
    }

    public void GameOver()
    {
        Player playerLogic = player.GetComponent<Player>();
        finalScore.text = ((playerLogic.score).ToString());

        int saveScore = PlayerPrefs.GetInt("SaveScore");
        if(saveScore < playerLogic.score)
        {
            PlayerPrefs.SetInt("SaveScore",playerLogic.score);
        }
        playerLogic.score = 0;
        Time.timeScale = 0;

        GameObject rain = objectManager.DestroyObj();

        gameOverSet.SetActive(true);
    }
    public void RetryGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    
        
}
