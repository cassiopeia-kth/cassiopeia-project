using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameServer
{
    public class ServerCountdownTimer
    {
        public static ServerCountdownTimer instance;

        public float currentTime = 0f;
        public float startTime = 10f;
        public bool isZero = false;

        /*private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(this);
            }
        }*/
        // Start is called before the first frame update
        public void StartTimer()
        {

            currentTime = startTime;
            isZero = false;
        }

        // Update is called once per frame
        public void UpdateTimer()
        {
            currentTime -= 1 * Constants.MS_PER_TICK; //* Time.deltaTime;
                              //GameObject.Find("Timer").GetComponent<TextMeshProUGUI>().text = currentTime.ToString("0");
                              //countdownText.text = currentTime.ToString("0");

            if (currentTime <= 0)
            {
                currentTime = 0;
                //GameObject.Find("Timer").GetComponent<TextMeshProUGUI>().text = "Time's up!";
                isZero = true;
            }
        }

        public void FixedUpdate()
        {
            UpdateTimer();
            ServerSend.TimerInfo(instance);
            Console.Write("sent currentTime");
        }
    }
}


