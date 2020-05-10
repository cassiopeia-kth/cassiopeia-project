﻿
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WallpaperManager : MonoBehaviour
{
    [SerializeField] private Gradient colorOverTime;
    [SerializeField] private float timeMultiplier = 0.5f;

    [SerializeField] private TextMeshProUGUI dokano;
    [SerializeField] private bool useThisText = false;

    [SerializeField] private bool changeColor = false;
    [SerializeField] private bool goBack = false;

    private float currentTimeStep;

    private void Start()
    {
       // if (useThisText)
       // {
       //     dokano = GetComponent<TextMeshProUGUI>();
       // }

      //  if (changeColor)
       // {
            StartChangingColor(dokano, colorOverTime, timeMultiplier);
      //  }
    }

    private IEnumerator ChangeTextColor(TextMeshProUGUI newText, Gradient newGradient, float timeSpeed)
    {
        while (true)
        {
            if (goBack)
            {
                currentTimeStep = Mathf.PingPong(Time.time * timeSpeed, 1);
            }
            else
            {
                currentTimeStep = Mathf.Repeat(Time.time * timeSpeed, 1);
            }

            newText.color = newGradient.Evaluate(currentTimeStep);

            yield return null;
        }
    }

    public void StartChangingColor(TextMeshProUGUI newText = null, Gradient newGradient = null, float timeSpeed = -1.0f)
    {
        if (newText != null && newGradient != null && timeSpeed > 0.0f)
        {
            StartCoroutine(ChangeTextColor(newText, newGradient, timeSpeed));
        }
        else if (newText != null && newGradient != null)
        {
            StartCoroutine(ChangeTextColor(newText, newGradient, timeMultiplier));
        }
        else if (newGradient != null && timeSpeed > 0.0f)
        {
            StartCoroutine(ChangeTextColor(dokano, newGradient, timeSpeed));
        }
        else if (newText != null && timeSpeed > 0.0f)
        {
            StartCoroutine(ChangeTextColor(newText, colorOverTime, timeSpeed));
        }
        else if (newText != null)
        {
            StartCoroutine(ChangeTextColor(newText, colorOverTime, timeMultiplier));
        }
        else if (newGradient != null)
        {
            StartCoroutine(ChangeTextColor(dokano, newGradient, timeMultiplier));
        }
        else if (timeSpeed > 0.0f)
        {
            StartCoroutine(ChangeTextColor(dokano, colorOverTime, timeSpeed));
        }
    }

    public void StopChangingColor()
    {
        StopCoroutine(ChangeTextColor(dokano, colorOverTime, timeMultiplier));
    }
}