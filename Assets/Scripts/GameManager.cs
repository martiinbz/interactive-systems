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

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterHit(string hitPlayerTag)
    {
        if (hitPlayerTag == "Red")
            blueScore++;
        else if (hitPlayerTag == "Blue")
            redScore++;

        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        redScoreText.text = "Red: " + redScore;
        blueScoreText.text = "Blue: " + blueScore;
        Debug.Log("Blue score:"  + blueScore + " Red score: " + redScore  );
    }
}