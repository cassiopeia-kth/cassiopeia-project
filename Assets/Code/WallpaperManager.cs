
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

//using System.Diagnostics;

public class WallpaperManager : MonoBehaviour
{
    [SerializeField] private Gradient colorOverTime;
    [SerializeField] private float timeMultiplier = 0.5f;

    [SerializeField] private TextMeshProUGUI dokano;
    [SerializeField] private bool useThisText = false;

    [SerializeField] private bool changeColor = false;
    [SerializeField] private bool goBack = false;

    [SerializeField] private TextMeshProUGUI pressKey;
    [SerializeField] private GameObject toDisable;

   // [SerializeField] private AudioSource music;

    private float timer;
    private float currentTimeStep;
   // public GameObject obj;
   // public AudioSource source;



    private void Start()
    {
       
        StartChangingColor(dokano, colorOverTime, timeMultiplier);
       
    }

    private void Update()
    {
       
        timer += Time.deltaTime;
        if(timer >= 0.5)
        {
            pressKey.enabled = true; 
        }

        if(timer >= 1)
        {
            pressKey.enabled = false;
            timer = 0;
        }

        if (Input.anyKeyDown)
        {
           // toDisable.SetActive(false);
           //Send to next scene using SceneManager should be added her 
            Debug.Log("mouse or a key pressed");
            SceneManager.LoadScene("Menu-test");
        }

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