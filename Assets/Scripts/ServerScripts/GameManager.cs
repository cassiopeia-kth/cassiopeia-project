using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Tilemaps;
using System;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    public GameObject localPlayerPrefab;
    public GameObject inventoryPrefab;
    public GameObject playerPrefab;
    public Canvas inventoryCanvas;
    public MovePlayer mp;
    public MovePlayerOnline mpo;
    public Canvas inventoryCanvasOnline;
    public string[] nameList;
    public bool startOfRound;
    public bool timerZero;
    public int HermesBuffer = 0;
    private int HermesSpawn = 0;

    private bool isThereAWinner = false;
    public GameObject _player;
    public static Dictionary<int, GameObject> playersNotManager = new Dictionary<int, GameObject>();
    public bool movedThisRound = false;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
	nameList = new string[4];
    }


    public void waitForInit(int id, Vector3 position, bool movedPoseidon){
	StartCoroutine(waitForGM(id, position, movedPoseidon));

    }

    public Dictionary<int, PlayerManager> getPlayers() {
      return players;
    }

    public void fillUsername(){
	if(nameList[0] != null){
	    GameObject.Find("Username_Player_1").GetComponent<TextMeshProUGUI>().text = nameList[0];
	    GameObject.Find("Text_Ready_Player_1").GetComponent<TextMeshProUGUI>().text = "Not Ready";
	    GameObject.Find("Text_Ready_Player_1").GetComponent<TextMeshProUGUI>().color = new Color32(255,0,0,255);
	    GameObject.Find("Text_Name_Player_1").GetComponent<TextMeshProUGUI>().text = "Connected";
	}
	if(nameList[1] != null){
	    GameObject.Find("Username_Player_2").GetComponent<TextMeshProUGUI>().text = nameList[1];
	    GameObject.Find("Text_Ready_Player_2").GetComponent<TextMeshProUGUI>().text = "Not Ready";
	    GameObject.Find("Text_Ready_Player_2").GetComponent<TextMeshProUGUI>().color = new Color32(255,0,0,255);
	    GameObject.Find("Text_Name_Player_2").GetComponent<TextMeshProUGUI>().text = "Connected";
	}
	if(nameList[2] != null){
	    GameObject.Find("Username_Player_3").GetComponent<TextMeshProUGUI>().text = nameList[2];
	    GameObject.Find("Text_Ready_Player_3").GetComponent<TextMeshProUGUI>().text = "Not Ready";
	    GameObject.Find("Text_Ready_Player_3").GetComponent<TextMeshProUGUI>().color = new Color32(255,0,0,255);
	    GameObject.Find("Text_Name_Player_3").GetComponent<TextMeshProUGUI>().text = "Connected";
	}
	if(nameList[3] != null){
	    GameObject.Find("Username_Player_4").GetComponent<TextMeshProUGUI>().text = nameList[3];
	    GameObject.Find("Text_Ready_Player_4").GetComponent<TextMeshProUGUI>().text = "Not Ready";
	    GameObject.Find("Text_Ready_Player_4").GetComponent<TextMeshProUGUI>().color = new Color32(255,0,0,255);
	    GameObject.Find("Text_Name_Player_4").GetComponent<TextMeshProUGUI>().text = "Connected";
	}

    }


    public void SpawnPlayer(int _id, string _username, Vector3 _position, string _charType, bool isReady) {


        //Debug.Log(_charType);
        if (_id == Client.instance.myId) {
            _player = Instantiate((GameObject)Resources.Load($"Prefabs/Player/{_charType}", typeof(GameObject)), _position, new Quaternion(0, 0, 0, 0));
	    mp = _player.AddComponent<MovePlayer>();
	    mp.rb = FindObjectOfType<Rigidbody2D>();
	    mp.ani = FindObjectOfType<Animator>();
	    GameObject inventoryHUD = Instantiate(inventoryPrefab);
	    mp.inventory = inventoryHUD.transform.GetChild(0).gameObject.AddComponent<Inventory>();
	    inventoryCanvas = inventoryHUD.transform.GetComponent<Canvas>();
	    inventoryCanvas.enabled = false;

        }
        else {

	    //Debug.Log(_position);
	    _player = Instantiate((GameObject)Resources.Load($"Prefabs/Player/{_charType}", typeof(GameObject)), _position, new Quaternion(0,0,0,0));
	    mpo = _player.AddComponent<MovePlayerOnline>();
	    mpo.rb = FindObjectOfType<Rigidbody2D>();
	    mpo.ani = FindObjectOfType<Animator>();
      mpo.id = _id; //ADDED FOR WINNER LOGIC
	    GameObject inventoryHUD = Instantiate(inventoryPrefab);
	    mpo.inventory = inventoryHUD.transform.GetChild(0).gameObject.AddComponent<Inventory>();
	    inventoryCanvasOnline = inventoryHUD.transform.GetComponent<Canvas>();
	    inventoryHUD.SetActive(false);
	}

	for(int i = 0; i < 4; i++){
	    if(nameList[i] == null){
		nameList[i] = _username;
		HermesSpawn = HermesSpawn + Math.Abs(_username.GetHashCode());
		break;
	    }
	}
        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;

	_player.GetComponent<PlayerManager>().isReady = isReady;
	playersNotManager.Add(_id, _player);
        players.Add(_id, _player.GetComponent<PlayerManager>());
	fillUsername();
	Lobby.instance.displayReadyorNot(_id);
	//Debug.Log(players[Client.instance.myId].isReady + "this is after spawingng a new playeralsknnaks"); //UP UNTIL HERE IT IS FINE

    }

    public float restartDelay = 2f;
    public RectTransform panelGameOver;
    public Text txtGameOver;
    public Canvas gameOverCanvas;
    public Button playAgain;
    public Button mainMenu;

    public Canvas SpectateCanvas;
    public Button returnFromSpectate;


    public void displayGameOverHUD() {
        gameOverCanvas.enabled = true;
        inventoryCanvas.enabled = false;
    }

    public void displayMainMenu() {
        Client.instance.tcp.Disconnect();
        Client.instance.udp.Disconnect();
        SceneManager.LoadScene("Menu-test"); // Do I need to disconnect here?
    }
    public void Start() {
	//	inventoryCanvas = inventoryPrefab.GetComponent<Canvas>();
        gameOverCanvas.enabled = false;
	//        inventoryCanvas.enabled = true;
        startOfRound = true;
        //        inventoryCanvas.enabled = true;
        //CountdownTimer.instance.StartTimer();
	GameObject.Find("CountdownTimer").GetComponent<CountdownTimer>().StartTimer();
        SpectateCanvas.enabled = false;
    }

    public bool checkWinner() {
      /*
      this block of code is added for win-menu
      */
      if(!isThereAWinner) {
        int countOfDead = 0;
        if(players.Count > 1) {
          foreach (PlayerManager player in players.Values) {
            if(player.isDead) {
              countOfDead++;
            }
          }
          if(countOfDead == players.Count - 1) {
            string name = "";

            foreach (PlayerManager playerInner in players.Values) {
                if(!playerInner.isDead) {
                  name = playerInner.username;
                }
              }
              txtGameOver.text = $"{name} wins";
              playAgain.gameObject.SetActive(false);
              EndGame();
              isThereAWinner = true;
              return true;
          }
          return false;
        }
        return false;
        /*
        this block of code is added for win-menu
        */
      }
      return false;
    }

    public void EndGame() {
      if(!isThereAWinner) { //ADDED FOR WINNER LOGIG
        Debug.Log("Game Over!");
        FindObjectOfType<MovePlayer>().enabled = false;
        Invoke("displayGameOverHUD", restartDelay);
        playAgain.onClick.AddListener(spectate);
        mainMenu.onClick.AddListener(displayMainMenu);
      }
    }

    public void AddItemToInventory(Inventory_Item item){
	//	mp.inventory.AddItem(item);
    }


    public void spawnCollectibleTrap(Vector2[] positions){

        int index = Math.Abs(HermesSpawn % positions.Length);
        var rand1 = new System.Random();
        int randomIndex1 = rand1.Next(positions.Length);

        //int randomIndex11 = rand1.Next(positions.Length);

        if (HermesBuffer > 600)
        {
            Debug.Log(HermesBuffer);
            GameObject c = (GameObject)Resources.Load("Prefabs/Traps/Hermes_Trap");
            Instantiate(c, positions[index], Quaternion.identity);
            HermesBuffer = 0;
            HermesSpawn = (HermesSpawn + index) * 2;

            if (randomIndex1 == index)
            {
                randomIndex1 = rand1.Next(positions.Length);
            }

            /*if (randomIndex11 == index)
            {
                randomIndex11 = rand1.Next(positions.Length);
            }*/
        }
        //HermesBuffer++;


        GameObject[] traps = {  (GameObject)  Resources.Load("Prefabs/CollectableTraps/Hades_Collectable"),
                                (GameObject)  Resources.Load("Prefabs/CollectableTraps/Fire_Collectable"),
                                //(GameObject)  Resources.Load("Prefabs/Traps/Hermes_Trap"), TOO OP
                                (GameObject)  Resources.Load("Prefabs/CollectableTraps/Poseidon_Collectable"),
                                (GameObject)  Resources.Load("Prefabs/CollectableTraps/Spike_Collectable"),
                                (GameObject)  Resources.Load("Prefabs/CollectableTraps/Zeusmain_Collectable")
        };

        var rand2 = new System.Random();
        int randomIndex2 = rand2.Next(traps.Length);
       // int randomIndex22 = rand2.Next(traps.Length);

        GameObject a = traps[randomIndex2];
       // GameObject b = traps[randomIndex22];
        Instantiate(a, positions[randomIndex1], Quaternion.identity);
        //Instantiate(b, positions[randomIndex11], Quaternion.identity);
        //Debug.Log("collectible trap " + traps[randomIndex2] + " spawned at position " + positions[randomIndex1]);
        //Debug.Log("collectible trap " + traps[randomIndex22] + " spawned at position " + positions[randomIndex11]);
    }

    void spectate() {
	gameOverCanvas.enabled = false;
    SpectateCanvas.enabled = true;
    //FindObjectOfType<MovePlayer>().enabled = false;
    returnFromSpectate.onClick.AddListener(displayMainMenu);
    }

    
    public static IEnumerator waitForGM(int id, Vector3 position, bool movedPoseidon){
	while(GameManager.players.ContainsKey(id) == false){
	    yield return null;
	}
	if(GameManager.players[id].GetComponent<MovePlayer>() != null)
	    GameManager.players[id].GetComponent<MovePlayer>().movePlayer(position, movedPoseidon);
	else if(GameManager.players[id].GetComponent<MovePlayerOnline>() != null)
	    GameManager.players[id].GetComponent<MovePlayerOnline>().movePlayer(position);
	
    }


    public int roundCount = 0;
    public void FixedUpdate(){

      checkWinner();
//      Debug.Log("Have I moved is " + movedThisRound  + ", and the button press " + (GameManager.players[Client.instance.myId].startPressed == true) + ", round number is" + roundCount);
        
        if(startOfRound == true && !timerZero){
	    MovePlayer.movedThisRound = false;
	    roundCount++;
            
            spawnCollectibleTrap(gameObject.GetComponent<Trap_positions>().smallMapCoordinates);
            startOfRound = false;
	    MovePlayer.arrowKeysEnabled = true;
	    foreach(GameObject go in playersNotManager.Values){
		if(go.GetComponent<MovePlayer>() != null)
		    if(go.GetComponent<MovePlayer>().isOverAHole)
			go.GetComponent<MovePlayer>().holeDeathStartRound();
		    if(go.GetComponent<MovePlayerOnline>() != null)
		    if(go.GetComponent<MovePlayerOnline>().isOverAHole)
			go.GetComponent<MovePlayerOnline>().holeDeathStartRound();
	    }
	    
        }
        else if (timerZero)
        {
            startOfRound = true;

	    /*  if (!movedThisRound && GameManager.players[Client.instance.myId].startPressed == true && roundCount > 1)
            {
                Debug.Log("I did not move! Kill me");
                ClientSend.sendTrap(Client.instance.myId, _player.transform.position, 1);
                GameObject laid_trap = Instantiate(Inventory.instance.hadesTrap, _player.transform.position, Quaternion.identity);
                laid_trap.GetComponent<TrapInteraction>().killer = "Not Moving";
                Debug.Log("Trap laid by player: " + laid_trap.GetComponent<TrapInteraction>().killer);
                movedThisRound = true;
            }*/
	    MovePlayer.arrowKeysEnabled = false;
        }
    }
    public void spawnTrapInvisible(Vector3 pos, int trapId){
	switch(trapId){
	    case 1:
		Instantiate(Inventory.instance.hadesTrap, pos, new Quaternion(0,0,0,0));
		break;
	    case 2:
		Instantiate(Inventory.instance.spikeTrap, pos, new Quaternion(0,0,0,0));
		break;
	    case 3:
		Instantiate(Inventory.instance.poseidonTrap, pos, new Quaternion(0,0,0,0));
		break;
	    case 4:
		Instantiate(Inventory.instance.zeusmainTrap, pos, new Quaternion(0,0,0,0));
		break;
	    case 5:
		Instantiate(Inventory.instance.fireTrap, pos, new Quaternion(0,0,0,0));
		break;


	}
    }

    public void spawnTrap(int id, GameObject trap, Vector3 pos, Quaternion rot, int trapId)
    {
        Debug.Log("Trapdrop Id: " + id);
        Debug.Log("My Id: " + Client.instance.myId);
        if (id == Client.instance.myId)
        {
            StartCoroutine(markTrap(pos, rot));
        }

        if(trap.name == "Zeusmain_Trap")
        {
            pos.y = pos.y + 0.5f;
        }
	ClientSend.sendTrap(Client.instance.myId, pos, trapId);
        GameObject laid_trap = Instantiate(trap, pos, rot);
        laid_trap.GetComponent<TrapInteraction>().killer = players[id].username;
        Debug.Log("Trap laid by player: " + laid_trap.GetComponent<TrapInteraction>().killer);
    }


    IEnumerator markTrap (Vector3 pos, Quaternion rot)
    {
        Debug.Log("Play cross animation");
        GameObject cross = (GameObject)Resources.Load("Prefabs/Traps/Cross", typeof(GameObject));
        GameObject actualCross = Instantiate(cross, pos, rot);
        yield return new WaitForSeconds(1.5f);
        actualCross.GetComponent<SpriteRenderer>().enabled = false;
        actualCross.SetActive(false);
    }
}
    /*
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public GameObject localPlayerPrefab;
    public GameObject inventoryPrefab;
    public GameObject playerPrefab;
    public Canvas inventoryCanvas;
    public MovePlayer mp;
    public MovePlayerOnline mpo;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }
    public void SpawnPlayer(int _id, string _username, Vector3 _position) {
        GameObject _player;
        if (_id == Client.instance.myId) {
	    _player = Instantiate(localPlayerPrefab, _position, new Quaternion(0,0,0,0));
	    mp = _player.AddComponent<MovePlayer>();
	    mp.rb = FindObjectOfType<Rigidbody2D>();
	    mp.ani = FindObjectOfType<Animator>();
	    GameObject inventoryHUD = Instantiate(inventoryPrefab);
	    mp.inventory = inventoryHUD.transform.GetChild(0).gameObject.AddComponent<Inventory>();
	    inventoryCanvas = inventoryHUD.transform.GetComponent<Canvas>();
	    inventoryCanvas.enabled = true;
        }
        else {
	    Debug.Log(_position);
	    _player = Instantiate(playerPrefab, _position, new Quaternion(0,0,0,0));
	    mpo = _player.AddComponent<MovePlayerOnline>();
	    mpo.rb = FindObjectOfType<Rigidbody2D>();
	    mpo.ani = FindObjectOfType<Animator>();
	    GameObject inventoryHUD = Instantiate(inventoryPrefab);
	    mpo.inventory = inventoryHUD.transform.GetChild(0).gameObject.AddComponent<Inventory>();
	    inventoryCanvas = inventoryHUD.transform.GetComponent<Canvas>();
	    inventoryHUD.SetActive(false);
	}
	 if(GameManager.players[id].GetComponent<MovePlayer>() != null)
		GameManager.players[id].GetComponent<MovePlayer>().movePlayer(position);
	    else if(GameManager.players[id].GetComponent<MovePlayerOnline>() != null)
		GameManager.players[id].GetComponent<MovePlayerOnline>().movePlayer(position);
    }
}*/
