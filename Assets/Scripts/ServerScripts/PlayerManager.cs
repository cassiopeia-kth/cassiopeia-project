using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public int id;
    public string username;
    public bool isAlive = false;
    public bool isReady;
    public bool checkChange = false;
    public bool everyoneReady = false;
    public bool startPressed = false;
}
