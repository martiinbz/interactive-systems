using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int redScore = 0;
    public int blueScore = 0;


    //Debug text
    public TextMesh redScoreText;
    public TextMesh blueScoreText;
    public TextMesh winText;

    public TextMesh red_state;
    public TextMesh blue_state;

    public HeartManager heartManager1;
    public HeartManager heartManager2;

    private bool finish = false;
    public int stateplayer1 = 0; // 0: normal, 1: crouch, 2: jump
    public int stateplayer2 = 0; // 0: normal, 1: crouch, 2: jump

    public int bullet_play1 = 3; //red
    public int bullet_play2 = 2; //blue

    public bool play1_safe = false;
    public bool play2_safe = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void update()
    {
        red_state.text = "State Red: " + stateplayer1;
       
    }

    public void RegisterHit(string hitPlayerTag, bool wave_color)
    {
        red_state.text = "State Red: " + stateplayer1;
        blue_state.text = "State Blue: " + stateplayer2;
        if (hitPlayerTag == "Red" && CheckWaveHit("Red", wave_color) && !play1_safe)
        {
            blueScore++;
            heartManager1.LoseLife();
        }
        else if (hitPlayerTag == "Blue" && CheckWaveHit("Blue", wave_color) && !play2_safe)
        {
            redScore++;
            heartManager2.LoseLife();
        }
        isWin();
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        redScoreText.text = "Red: " + redScore;
        blueScoreText.text = "Blue: " + blueScore;
    }

    void isWin()
    {
        if (redScore >= 3)
        {
            winText.text = "Red Wins!";
            winText.color = Color.red;
        }
        else if (blueScore >= 3)
        {
            winText.text = "Blue Wins!";
            winText.color = Color.blue;
        }
    }
    bool CheckWaveHit(string player, bool wave_color) //wave_color = true for red, false for blue
    {
        if (player == "Red" && wave_color)
        {
            if (!(stateplayer1 == 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (player == "Red" && !wave_color)
        {
            if (!(stateplayer1 == 2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (player == "Blue" && wave_color)
        {
            if (!(stateplayer2 == 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (player == "Blue" && !wave_color)
        {
            if (!(stateplayer2 == 2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    }