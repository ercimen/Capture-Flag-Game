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
    [Header("Other")]
    public Text Lives, Diamond, ButtonText;

    public int liveCount;
    public int Score = 0;
    private bool click;

    public int PlayerCaptureCount;
    private int EnemyCaptureCount;

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
    PlayerCaptureCount=1;
    EnemyCaptureCount=2;
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
        Lives.text = liveCount.ToString();
        if (PlayerCaptureCount == 0) // GameOver Controlünü yapacağım
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        inGamePanel.SetActive(false);
        GameOverPanel.SetActive(true);
        StartCoroutine(ResourceTickOver(3f, 0));
    }
    public void NextLevel()
    {
        ButtonText.text = "GET " + liveCount;
        inGamePanel.SetActive(false);
        NextLevelPanel.SetActive(true);
    }
    public void NextLevelButton()
    {
        Score += liveCount;
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

        Debug.Log("PlayerCapture:"+PlayerCaptureCount);
        Debug.Log("EnemyCapture:" + EnemyCaptureCount);

    }

    public int CaptureCount()
    {
        return PlayerCaptureCount;
    }
}
