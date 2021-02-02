using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Log : MonoBehaviour
{
    [SerializeField] private List<AppleChance> apples;
    [SerializeField] private List<KnifeInLog> knifes;
    //[SerializeField] private float rotationTime;
    //[SerializeField] private float rotationSpeed;
    private float[] speed = new float[] { 60, 150, 100, 200 };
    private float rotationSpeed;

    public GameObject brokenLogObject;
    public bool isGameActive = true;

    void Start()
    {
        SpawnApple();
        SpawnKnife();
        GameController.Instance.NextLevel += GameController_NextLevel;
        GameController.Instance.GameIsOver += Instance_GameIsOver;
        rotationSpeed = speed[Random.Range(0, speed.Length)];
        StartCoroutine("ChangeSpeed");
    }

    private void Instance_GameIsOver()
    {
        isGameActive = false;
        Destroy(gameObject, 1.2f);
    }

    private void GameController_NextLevel()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (isGameActive)
        {
            RotateLog();
        }
    }

    private void RotateLog()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    private IEnumerator ChangeSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            rotationSpeed = rotationSpeed - 10f;
            if (rotationSpeed == 0)
            {
                rotationSpeed = speed[Random.Range(0, speed.Length)];
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private void SpawnApple()
    {
        float chanceApple = Random.Range(0f, 1f);

        foreach (var apple in apples)
        {
            if (chanceApple <= apple.Chance)
            {
                GameObject newApple = Instantiate(apple.Apple);
                float appleAngle = apple.Angle;
                newApple.transform.SetParent(transform);
                SetRotationFromLog(transform, newApple.transform, appleAngle, 0.9f, 0);
            }
        }
    }

    private void SpawnKnife()
    {
        int knifeCountInLog = UnityEngine.Random.Range(1, 4);

            for (int i = 0; i < knifeCountInLog; i++)
            {
                GameObject newKnife = Instantiate(knifes[i].Knife);
                float knifeAngle = knifes[i].Angle;
                newKnife.transform.SetParent(transform);
                SetRotationFromLog(gameObject.transform, newKnife.transform, knifeAngle, 0.55f, 180f);
            }
    }

    private void SetRotationFromLog(Transform log, Transform objectPoint, float angle, float space, float rot)
    {
        Vector2 offset = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)) * (log.GetComponent<CircleCollider2D>().radius + space);
        objectPoint.localPosition = (Vector2)log.localPosition + offset;
        objectPoint.localRotation = Quaternion.Euler(0f, 0f, -angle + rot);
    }

    private void OnDestroy()
    {
        GameController.Instance.NextLevel -= GameController_NextLevel;
        GameController.Instance.GameIsOver -= Instance_GameIsOver;
    }
}
