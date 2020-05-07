using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;

namespace GameServer
{
    public class ServerCountdownTimer
    {
        public static ServerCountdownTimer instance;

        public float currentTime = 0f;
        public float startTime = 15f;
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
            

            currentTime -= 1f / (float)Constants.TICKS_PER_SEC; //* Time.deltaTime;
            isZero = false;
            
            if (currentTime <= 0){
                FreezeGame();
                //currentTime = 15f;
                isZero = true;

            }
            
        }

        public void FreezeGame() {
            foreach (Client item in Server.clients.Values) {
                if (item.player != null) {
                    item.player.isAlive = false;
                }
            }
            //Thread.Sleep(_duration);

            currentTime -= 1f / (float)Constants.TICKS_PER_SEC; //* Time.deltaTime;
            if (currentTime <= -5) {
                
                foreach (Client item in Server.clients.Values) {
                    if (item.player != null) {
                        item.player.isAlive = true;
                    }
                }
                currentTime = 15f;
            }
        }
        
        public void Update(){
            /*if (currentTime <= 0) {
                FreezeGame(5000);
                currentTime = 15f;        
            }*/
        UpdateTimer();
        ServerSend.TimerInfo(instance);
        Console.WriteLine("sent currentTime");
        }
    }
}


