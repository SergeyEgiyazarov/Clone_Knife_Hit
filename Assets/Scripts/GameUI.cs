using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private List<Image> knifeIcon;
    [SerializeField] private List<Image> stageIcon;
    private int score = 0;
    private int stageActive = 0;
    private int stage = 1;

    public Color iconColorLigth;
    public Color iconColorDark;
    public Color iconColorYellow;

    private int knifeCount;
    private int activeKnife = 0;

    private void Awake()
    {
        
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = "" + score;
    }

    public void GameOver()
    {
        score = 0;
        stage = 1;
        scoreText.text = "" + score;
        stageText.text = "Stage " + stage;
        foreach (var item in knifeIcon)
        {
            item.gameObject.SetActive(false);
        }
        foreach (var item in stageIcon)
        {
            item.color = iconColorLigth;
        }
        activeKnife = 0;
        stageActive = 0;
        gameObject.SetActive(false);
    }

    public void SetActiveKnifeIcon(int knife)
    {
        knifeCount = knife;
        for (int i = 0; i < knifeCount; i++)
        {
            knifeIcon[i].gameObject.SetActive(true);
            knifeIcon[i].color = iconColorLigth;
        }
    }

    public void ShadowKnife()
    {
        if (activeKnife <= knifeCount)
        {
            knifeIcon[activeKnife].color = iconColorDark;
            activeKnife++;
        }
        
    }

    public void Next()
    {
        foreach (var item in knifeIcon)
        {
            item.gameObject.SetActive(false);
        }

        activeKnife = 0;

        stage++;

        if (stage % 5 == 0)
        {
            stageText.text = "Boss";
        }
        else
        {
            stageText.text = "Stage " + stage;
        }

        if (stageActive < 4)
        {
            stageIcon[stageActive].color = iconColorYellow;
        }
        

        stageActive++;

        if (stageActive > 4)
        {
            foreach (var item in stageIcon)
            {
                item.color = iconColorLigth;
            }
            stageActive = 0;
        }
    }
}
