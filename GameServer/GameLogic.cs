using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer {
    class GameLogic {
        public static void Update() {
            foreach (Client _client in Server.clients.Values) {
                if (_client.player != null) {
                    _client.player.Update();
                }
            }
	    ServerCountdownTimer.instance.Update();
            ThreadManager.UpdateMain();
            Console.WriteLine($"{ServerCountdownTimer.instance.currentTime}");
        }
    }
}
