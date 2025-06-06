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

    public int bullet_play1 = 0; //red
    public int bullet_play2 = 0; //blue

    public bool play1_safe = false;
    public bool play2_safe = false;

    //Button round Logic
    public TextMesh countdownText;
    public float roundTime = 90f;

    public GameObject chargeButtonPlayer1;
    public GameObject chargeButtonPlayer2;

    private bool player1Charged = false;
    private bool player2Charged = false;
    public bool roundActive = false;

    // Audio for last 10 seconds
    public AudioSource audioSource;
    public AudioClip lastSecondsSound;
    public AudioClip go123;
    public AudioClip finishroundsound;
    private bool isLastSecondsSoundPlaying = false;
    private bool goended = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        unablebuttonsround();
    }
    public void unablebuttonsround()
    {
        chargeButtonPlayer1.SetActive(false);
        chargeButtonPlayer2.SetActive(false);
    }

    private void Update()
    {
        red_state.text = "State Red: " + stateplayer1;
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(StartRoundCountdown());
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            ResetGame();
        }

    }

    public void NotifyPlayerCharged(string tag)
    {
        if (tag == "Player1")
            player1Charged = true;
        else if (tag == "Player2")
            player2Charged = true;

        CheckStartCountdown();
    }

    public void NotifyPlayerUncharged(string tag)
    {
        if (tag == "Player1")
            player1Charged = false;
        else if (tag == "Player2")
            player2Charged = false;
    }

    private void CheckStartCountdown()
    {
        if (player1Charged && player2Charged && !roundActive)
        {
            StartCoroutine(StartRoundCountdown());
        }
    }

    public void safe_player(string playerTag)
    {
        if (playerTag == "Player1")
        {
            play1_safe = true;
        }
        else if (playerTag == "Player2")
        {
            play2_safe = true;
        }
    }
    public void unsafe_player(string playerTag)
    {
        if (playerTag == "Player1")
        {
            play1_safe = false;
        }
        else if (playerTag == "Player2")
        {
            play2_safe = false;
        }
    }

    IEnumerator StartRoundCountdown()
    {
        heartManager1.ResetHearts();
        heartManager2.ResetHearts();
        goended = false;
        string[] count = { "1", "2", "3", "Go!" };
        audioSource.PlayOneShot(go123, 6.0f);
        for (int i = 0; i < count.Length; i++)
        {
            countdownText.text = count[i];
            yield return new WaitForSeconds(1f);
        }
        goended = true;
        chargeButtonPlayer1.SetActive(false);
        chargeButtonPlayer2.SetActive(false);

        countdownText.text = "";
        StartCoroutine(RoundTimer());
    }

    IEnumerator RoundTimer()
    {
        roundActive = true;
        float timeLeft = roundTime;
        // Optionally show a timer in the UI
        while (timeLeft > 0 && !finish)
        {
            timeLeft -= Time.deltaTime;
            if (Mathf.FloorToInt(timeLeft) > 0)
            {
                countdownText.text = "Time left: " + Mathf.FloorToInt(timeLeft).ToString();
            }
            else
            {
                countdownText.text = "Finish!";
            }
            // Play sound during last 10 seconds
            if (timeLeft <= 10f && !isLastSecondsSoundPlaying && audioSource != null && lastSecondsSound != null)
            {
                countdownText.color = Color.red;
                audioSource.clip = lastSecondsSound;
                audioSource.loop = true;
                audioSource.Play();
                isLastSecondsSoundPlaying = true;
            }
            if(goended == false)
            {
                countdownText.text = "Finish!";
            }

            yield return null;
        }

        // Stop the sound when round ends
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            isLastSecondsSoundPlaying = false;
        }

        // Only check for heart-based win if the round hasn't already ended
        if (!finish)
        {
            int redHearts = heartManager1.GetLives();
            int blueHearts = heartManager2.GetLives();
            
            // Check for heart-based win when time runs out
            if (redHearts > blueHearts)
            {
                winText.text = "Red Wins!";
                winText.color = Color.red;
                finish = true;
            }
            else if (blueHearts > redHearts)
            {
                winText.text = "Blue Wins!";
                winText.color = Color.blue;
                finish = true;
            }
            else
            {
                winText.text = "Draw! ;(";
                winText.color = new Color(1f, 0.4f, 0.7f);
                finish = true;
            }
        }
        StartCoroutine(EndRound());
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
        if (heartManager1.GetLives() <= 0)
        {
            winText.text = "Blue Wins!";
            winText.color = Color.blue;
            countdownText.text = "";
            finish = true;
            StartCoroutine(EndRound());
        }
        else if (heartManager2.GetLives() <= 0)
        {
            winText.text = "Red Wins!";
            winText.color = Color.red;
            countdownText.text = "";
            finish = true;
            StartCoroutine(EndRound());
        }
        else if (redScore >= 3)
        {
            winText.text = "Red Wins!";
            winText.color = Color.red;
            finish = true;
            StartCoroutine(EndRound());
        }
        else if (blueScore >= 3)
        {
            winText.text = "Blue Wins!";
            winText.color = Color.blue;
            finish = true;
            StartCoroutine(EndRound());
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
    IEnumerator EndRound()
    {
        audioSource.PlayOneShot(finishroundsound, 4.0f);
        countdownText.text = "Finish!";

        roundActive = false;

        player1Charged = false;
        player2Charged = false;

        // Reset hearts
        heartManager1.ResetHearts();
        heartManager2.ResetHearts();

        // Reset scores if needed
        redScore = 0;
        blueScore = 0;
        UpdateScoreText();
        yield return new WaitForSeconds(8f);
        ResetGame();
    }
    public void ResetGame()
    {
        // Reset scores
        redScore = 0;
        blueScore = 0;
        UpdateScoreText();

        // Reset heart systems
        heartManager1.ResetHearts();
        heartManager2.ResetHearts();

        // Clear win message
        winText.text = "";

        // Reset player states
        stateplayer1 = 0;
        stateplayer2 = 0;
        bullet_play1 = 0;
        bullet_play2 = 0;
        play1_safe = false;
        play2_safe = false;
        finish = false;

        // Reset charge system
        player1Charged = false;
        player2Charged = false;
        roundActive = false;

        // Reactivate the charge buttons
        chargeButtonPlayer1.SetActive(true);
        chargeButtonPlayer2.SetActive(true);

        // Clear countdown text
        countdownText.text = "";
    }
}