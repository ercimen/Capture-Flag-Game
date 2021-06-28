using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject inGamePanel;
    public GameObject GameOverPanel;
    public GameObject NextLevelPanel;
    public GameObject TapToPlay;
    [SerializeField] GameObject EnemyCastle,PlayerCastle;
    [Header("Other")]
    public Text Lives, Diamond, ButtonText;

    [Header("TowerImages")]
    [SerializeField] Image TowerImage1;
    [SerializeField] Sprite[] Sprites;

    public int liveCount;
    public bool isGameStarted;
    public int Score = 0;
    private bool click;

    public int PlayerCaptureCount;
    public int EnemyCaptureCount;
    public int EnemyHP;

    #region Singleton

    private static GameManager _ınstance;

    public static GameManager Instance
    {
        get
        {
            if (_ınstance == null)
                _ınstance = FindObjectOfType<GameManager>();
            return _ınstance;
        }
    }
    #endregion
    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex==0)
        {
        isGameStarted = false;
        }
       
}
    private void Start()
    {
        click = false;
        if (PlayerPrefs.HasKey("score"))
        {
            Score = PlayerPrefs.GetInt("score");
            Diamond.text = Score.ToString();
        }
    }
    void Update()
    {
      //  Lives.text = liveCount.ToString();
        if (PlayerCaptureCount == 0) // GameOver Controlünü yapacağım
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        Camera_Control.Instance.LoseGame();
        PlayerCastle.transform.position = new Vector3(0, 4.73f, 22);
        PlayerCastle.GetComponent<Animator>().enabled = true;

        inGamePanel.SetActive(false);
        GameOverPanel.SetActive(true);
    }
    public void NextLevel()
    {
        EnemyCastle.GetComponent<Animator>().enabled =true;
      //  ButtonText.text = "GET " + liveCount;
        inGamePanel.SetActive(false);
        NextLevelPanel.SetActive(true);
    }
    public void NextLevelButton()
    {
        Score += liveCount;
        isGameStarted = true;
        if (click == false)
        {
            PlayerPrefs.SetInt("score", Score);
            Diamond.text = Score.ToString();
            StartCoroutine(ResourceTickOver(.5f, 1));
            click = true;
            
        }
    }
    IEnumerator ResourceTickOver(float waitTime, int level)
    {
        yield return new WaitForSeconds(waitTime);
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + level);
        }

    }
    public void ChangeCountText(int count)
    {
        liveCount += count;
    }

    public void ChangeCountCapture(byte count, string whois)
    {
        if (whois == "Player")
        {
            PlayerCaptureCount += count;
            EnemyCaptureCount -= count;
            Camera_Control.Instance.ChangeCamPos(1);
            
        }

        if (whois == "Enemy")
        {
            EnemyCaptureCount += count;
            PlayerCaptureCount -= count;
           
        }
        TowerImage1.sprite = Sprites[PlayerCaptureCount];
        Debug.Log("PlayerCapture:"+PlayerCaptureCount);
        Debug.Log("EnemyCapture:" + EnemyCaptureCount);

    }

    public int CaptureCount()
    {
        return PlayerCaptureCount;
    }

    public void TapTap()
    {
        isGameStarted = true;
        TapToPlay.gameObject.SetActive(false);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
