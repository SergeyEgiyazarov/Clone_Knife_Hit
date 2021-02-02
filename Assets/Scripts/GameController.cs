using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ChangeLevel();
public class GameController : MonoBehaviour
{
    private static GameController instance = null;

    [SerializeField] private Transform knifeSpawnPosition;
    [SerializeField] private Transform logSpawnPosition;
    [SerializeField] private Knife knifePrefab;
    [SerializeField] private List<Log> logs;
    [SerializeField] private List<Log> bosses;
    private GameObject brokenLog;
    private bool isBoss = false;

    public int level = 1;
    public int knifeCount;
    public GameUI gameUI;

    public event ChangeLevel NextLevel;
    public event ChangeLevel GameIsOver;
    public event KnifeFire ScoreUpdateOnHit;

    public static GameController Instance { get { return instance; } }

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
        Vibration.Init();
    }

    public void StartLevel()
    {
        if (isBoss)
        {
            isBoss = false;
            knifeCount = Random.Range(6, 11);
            gameUI.SetActiveKnifeIcon(knifeCount);
            int randomBoss = Random.Range(0, bosses.Count);
            brokenLog = bosses[randomBoss].brokenLogObject;
            Instantiate(bosses[randomBoss], logSpawnPosition);
            SpawnKnife();
        }
        else
        {
            knifeCount = Random.Range(6, 11);
            gameUI.SetActiveKnifeIcon(knifeCount);
            int randomLog = Random.Range(0, logs.Count);
            brokenLog = logs[randomLog].brokenLogObject;
            Instantiate(logs[randomLog], logSpawnPosition);
            SpawnKnife();
        }
    }

    private IEnumerator StartLevelDelay()
    {
        GameObject broken = Instantiate(brokenLog, logSpawnPosition);
        Destroy(broken, 1.2f);
        yield return new WaitForSeconds(1.3f);
        StartLevel();
    }

    public void SpawnKnife()
    {
        StartCoroutine("NewKnife");
    }

    private IEnumerator NewKnife()
    {
        yield return new WaitForSeconds(0.1f);
        Knife knife = Instantiate(knifePrefab, knifeSpawnPosition);
        knife.KnifeHitLog += Knife_KnifeHitLog;
    }

    private void Knife_KnifeHitLog(bool isGame)
    {
        if (isGame)
        {
            knifeCount--;
            Vibration.Vibrate();
            ScoreUpdateOnHit(true);
            gameUI.UpdateScore();
            gameUI.ShadowKnife();

            if (knifeCount == 0)
            {
                level++;
                if (level % 5 == 0)
                {
                    isBoss = true;
                    NextLevel();
                    Vibration.Vibrate();
                    gameUI.Next();
                    StartCoroutine("StartLevelDelay");
                }
                else
                {
                    NextLevel();
                    Vibration.Vibrate();
                    gameUI.Next();
                    StartCoroutine("StartLevelDelay");
                }
            }
            else
            {
                SpawnKnife();
            }
        }
        else
        {
            Vibration.Vibrate();
            GameOver();
        }
    }

    

    private void GameOver()
    {
        level = 1;
        gameUI.GameOver();
        GameIsOver();
    }
}
