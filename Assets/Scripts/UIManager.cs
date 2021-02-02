using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;

    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI totalStageText;
    [SerializeField] private TextMeshProUGUI totalAppleText;
    //[SerializeField] private GameUI gameUI;
    [SerializeField] private GameObject gameOverWindow;
    [SerializeField] private TextMeshProUGUI gameOverTextScore;
    [SerializeField] private TextMeshProUGUI gameOverTextStage;

    public static UIManager Instance { get { return instance; } }

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

    public void SetupScoreAndStage(int pointScore, int pointStage)
    {
        totalScoreText.text = "Score " + pointScore;
        totalStageText.text = "Stage " + pointStage;
    }

    public void RestartGameMenu(int scorePoint, int stagePoint)
    {
        gameOverTextScore.text = "" + scorePoint;
        gameOverTextStage.text = "Stage " + stagePoint;
        StartCoroutine("GameOverDelay");
    }

    public void SetupApple(int apple)
    {
        totalAppleText.text = "" + apple;
    }

    private IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(1.2f);
        gameOverWindow.SetActive(true);
    }
}
