using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class CountdownTimer : MonoBehaviour {

    public static CountdownTimer instance;

    public float currentTime = 19f;
    public float startTime = 19f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this){
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    public void StartTimer()
    {
        currentTime = startTime;
    }

    // Update is called once per frame
    public void UpdateTimer()
    {
        GameObject.Find("Timer").GetComponent<TextMeshProUGUI>().text = currentTime.ToString("0");


        if (currentTime <= 19 && currentTime > 18)
        {
            GameObject.Find("Timer").GetComponent<TextMeshProUGUI>().text = "3";
            Debug.Log("3 countdown");
        }
        else if (currentTime <= 18 && currentTime > 17)
        {
            GameObject.Find("Timer").GetComponent<TextMeshProUGUI>().text = "2";
        }
        else if (currentTime <= 17 && currentTime > 16)
        {
            GameObject.Find("Timer").GetComponent<TextMeshProUGUI>().text = "1";
        }
        else if (currentTime <= 16 && currentTime > 15)
        {
            GameObject.Find("Timer").GetComponent<TextMeshProUGUI>().text = "Round Start!";
        }

        //currentTime -= 1 * Time.deltaTime;
     //countdownText.text = currentTime.ToString("0");

        else if (currentTime <= 0){
            currentTime = 0;
	        GameObject.Find("Timer").GetComponent<TextMeshProUGUI>().text = "Time's up!";
        }
    }

    public void Update()
    {
        UpdateTimer();
    }


}
