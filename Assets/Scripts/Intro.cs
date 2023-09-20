using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public GameObject springGO;
    public GameObject summerGO;
    public GameObject autumnGO;
    public GameObject winterGO;
    public GameObject GOIntro;

    public GameObject player;
    public Button nextBtn;
    public Text highScoreText;

    private void Start()
    {
        int saveScore = PlayerPrefs.GetInt("SaveScore");
        highScoreText.text = saveScore.ToString();
    }

    void Awake()
    {
        springGO = springGO.GetComponent<GameObject>();
        summerGO = summerGO.GetComponent<GameObject>();
        autumnGO = autumnGO.GetComponent<GameObject>();
        winterGO = winterGO.GetComponent<GameObject>();
        GOIntro = GetComponent<GameObject>();

        nextBtn = GetComponent<Button>();
    }
    public void HowToPlayOnClick()
    {
        GOIntro.SetActive(true);
    }
    public void ButtonOnClick()
    {
        SceneManager.LoadScene(1);
    }



}
