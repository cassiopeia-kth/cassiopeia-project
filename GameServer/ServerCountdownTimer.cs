using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Threading;

namespace GameServer
{
    public class ServerCountdownTimer
    {
        public static ServerCountdownTimer instance;

        public float currentTime = 19f;
        public float startTime = 19f;
        public bool isZero = false;

        public void Awake(){
            if (instance == null){
                instance = this;
            }
        }
        // Start is called before the first frame update
        public void StartTimer() {
            Console.WriteLine("STARTTIMER METHOD CALLED");
            currentTime = startTime;
            isZero = false;
        }

        // Update is called once per frame
        public void UpdateTimer(){
            if (currentTime <= 0)
            {
                isZero = true;
                if(currentTime <= -7)
                {
                    currentTime = 19f;
                    isZero = false;
                }
            }
	    if(currentTime > 0)
		isZero = false;


                currentTime -= 1f / (float)Constants.TICKS_PER_SEC; //* Time.deltaTime;
                

        }

        public void FreezeGame() {
            //Console.WriteLine("Freeze Boi, ice cold");
            foreach (Client item in Server.clients.Values) {
                if (item.player != null) {
                    item.player.isAlive = false;
                    //Console.WriteLine($"{item.player.isAlive} is the isAlive status");
                }
            }

            currentTime -= 1f / (float)Constants.TICKS_PER_SEC; //* Time.deltaTime;
            if (currentTime <= -5) {
                
                foreach (Client item in Server.clients.Values) {
                    if (item.player != null) {
                        item.player.isAlive = true;
                    }
                }
                currentTime = 19f;
            }
        }
        
        public void Update(){
            
                UpdateTimer();
                ServerSend.TimerInfo(instance);
                //Console.WriteLine("sent currentTime");
            
        }
    }
}


