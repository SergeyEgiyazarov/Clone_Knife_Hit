using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    private const string TOTAL_SCORE = "TotalScore";
    private const string TOTAL_STAGE = "TotalStaeg";
    private const string TOTAL_APPLE = "TotalApple";

    private GameController gameController;
    private UIManager uiManager;
    private int totalScore = 0;
    private int totalStage = 0;
    private int score = 0;
    private int stage = 1;
    private int totalApple = 0;
    
    //public GameUI gameUI;
    
    public static GameManager Instance { get { return instance; } }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameController = GameController.Instance;
        uiManager = UIManager.Instance;
        gameController.ScoreUpdateOnHit += GameController_ScoreUpdateOnHit;
        gameController.NextLevel += GameController_NextLevel;
        gameController.GameIsOver += GameController_GameIsOver;
        LoadData();
        uiManager.SetupScoreAndStage(totalScore, totalStage);
        uiManager.SetupApple(totalApple);
    }

    private void GameController_NextLevel()
    {
        stage++;
    }

    private void GameController_GameIsOver()
    {
        totalScore = totalScore < score ? score : totalScore;
        totalStage = totalStage < stage ? stage : totalStage;
        uiManager.SetupScoreAndStage(totalScore, totalStage);
        uiManager.RestartGameMenu(score, stage);
        PlayerPrefs.SetInt(TOTAL_SCORE, totalScore);
        PlayerPrefs.SetInt(TOTAL_STAGE, totalStage);
        PlayerPrefs.Save();
        score = 0;
        stage = 1;
    }

    private void GameController_ScoreUpdateOnHit(bool isGameActive)
    {
        score++;
        //gameUI.UpdateScore();
    }

    private void LoadData()
    {
        totalScore = PlayerPrefs.GetInt(TOTAL_SCORE, 0);
        totalStage = PlayerPrefs.GetInt(TOTAL_STAGE, 1);
        totalApple = PlayerPrefs.GetInt(TOTAL_APPLE, 0);
    }

    public void AppleUppdate()
    {
        totalApple++;
        PlayerPrefs.SetInt(TOTAL_APPLE, totalApple);
        uiManager.SetupApple(totalApple);
    }
}
