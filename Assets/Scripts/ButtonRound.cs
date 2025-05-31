using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRound : MonoBehaviour
{
    public string playerName; // "Player1" or "Player2"
    public Color startColor = Color.white;
    public Color chargedColor = Color.green;
    public float chargeTime = 3f;

    private float timer = 0f;
    private bool isCharging = false;
    private bool fullyCharged = false;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = startColor;
    }

    void Update()
    {
        if (isCharging && !fullyCharged)
        {
            timer += Time.deltaTime;
            rend.material.color = Color.Lerp(startColor, chargedColor, timer / chargeTime);

            if (timer >= chargeTime)
            {
                fullyCharged = true;
                GameManager.Instance.NotifyPlayerCharged(playerName);
            }
        }
    }
    void OnEnable() //called when set active
    {
        isCharging = false;
        fullyCharged = false;
        timer = 0f;

        if (rend == null)
            rend = GetComponent<Renderer>();

        rend.material.color = startColor;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter: " + other.gameObject.name);
        if (other.gameObject.name == playerName)
        {
            isCharging = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == playerName)
        {
            isCharging = false;
            timer = 0f;
            rend.material.color = startColor;
            fullyCharged = false;
            GameManager.Instance.NotifyPlayerUncharged(playerName);
        }
    }
}