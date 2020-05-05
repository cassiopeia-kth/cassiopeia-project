﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{


    public float currentTime = 0f;
    public float startTime = 10f;

    [SerializeField] TextMeshProUGUI countdownText;
    // Start is called before the first frame update
    public void StartTimer()
    {
        currentTime = startTime;
    }

    // Update is called once per frame
    public void UpdateTimer()
    {
        currentTime -= 1 * Time.deltaTime;
        //countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
            countdownText.text = "Time Up!";
        }
    }

    public void Update()
    {
        UpdateTimer();
    }
}
