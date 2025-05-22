using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int redScore = 0;
    public int blueScore = 0;

    public TextMesh redScoreText;
    public TextMesh blueScoreText;
    public TextMesh winText;

    public HeartManager heartManager1;
    public HeartManager heartManager2;

    private bool finish = false;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void update()
    {
        if (!finish)
        {
            if (redScore >= 3)
            {
                winText.text = "Red Wins!";
                winText.color = Color.red;
                finish = true;
            }
            else if (blueScore >= 3)
            {
                winText.text = "Blue Wins!";
                winText.color = Color.blue;
                finish = true;
            }
        }
       
    }

    public void RegisterHit(string hitPlayerTag)
    {
        if (hitPlayerTag == "Red")
        {
            blueScore++;
            heartManager1.LoseLife();
        }
        else if (hitPlayerTag == "Blue")
        {
            redScore++;
            heartManager2.LoseLife();
        }

        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        redScoreText.text = "Red: " + redScore;
        blueScoreText.text = "Blue: " + blueScore;
    }
}