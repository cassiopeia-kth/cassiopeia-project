using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class CountdownTimer : MonoBehaviour {

    public static CountdownTimer instance;

    public float currentTime = 0f;
    public float startTime = 10f;

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

     //currentTime -= 1 * Time.deltaTime;
	 GameObject.Find("Timer").GetComponent<TextMeshProUGUI>().text = currentTime.ToString("0");
     //countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0){
            currentTime = 0;
	        GameObject.Find("Timer").GetComponent<TextMeshProUGUI>().text = "Time's up!";
        }
    }

    public void Update()
    {
        UpdateTimer();
    }


}
