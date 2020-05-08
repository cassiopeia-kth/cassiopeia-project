using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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


    public void waitForInit(int id, Vector3 position){
	StartCoroutine(waitForGM(id, position));

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
    

    public void SpawnPlayer(int _id, string _username, Vector3 _position, string _charType) {
        
        
        Debug.Log("PPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP");
        Debug.Log(_charType);
        GameObject _player;
        if (_id == Client.instance.myId) {
	    _player = Instantiate((GameObject)Resources.Load($"Prefabs/Player/{_charType}", typeof(GameObject)), _position, new Quaternion(0,0,0,0));
	    mp = _player.AddComponent<MovePlayer>();
	    mp.rb = FindObjectOfType<Rigidbody2D>();
	    mp.ani = FindObjectOfType<Animator>();	    
	    GameObject inventoryHUD = Instantiate(inventoryPrefab);
	    mp.inventory = inventoryHUD.transform.GetChild(0).gameObject.AddComponent<Inventory>();
	    inventoryCanvas = inventoryHUD.transform.GetComponent<Canvas>();
	    inventoryCanvas.enabled = false;
	    
        }
        else {
	    
	    Debug.Log(_position);
	    _player = Instantiate((GameObject)Resources.Load($"Prefabs/Player/{_charType}", typeof(GameObject)), _position, new Quaternion(0,0,0,0));
	    mpo = _player.AddComponent<MovePlayerOnline>();
	    mpo.rb = FindObjectOfType<Rigidbody2D>();
	    mpo.ani = FindObjectOfType<Animator>();	    
	    GameObject inventoryHUD = Instantiate(inventoryPrefab);
	    mpo.inventory = inventoryHUD.transform.GetChild(0).gameObject.AddComponent<Inventory>();
	    inventoryCanvasOnline = inventoryHUD.transform.GetComponent<Canvas>();
	    inventoryHUD.SetActive(false);
	}

	for(int i = 0; i < 4; i++){
	    if(nameList[i] == null){
		nameList[i] = _username;
		break;
	    }
	}
        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;
        players.Add(_id, _player.GetComponent<PlayerManager>());
	fillUsername();
    }

    public float restartDelay = 2f;
    public RectTransform panelGameOver;
    public Text txtGameOver;
    public Canvas gameOverCanvas;
    public Button playAgain;
    public Button mainMenu;


    public void displayGameOverHUD() {
        gameOverCanvas.enabled = true;
        inventoryCanvas.enabled = false;
    }

    public void displayMainMenu() {
        SceneManager.LoadScene("Menu");
    }
    public void Start() {
	//	inventoryCanvas = inventoryPrefab.GetComponent<Canvas>();
        gameOverCanvas.enabled = false;
	//        inventoryCanvas.enabled = true;
        startOfRound = true; 
        //        inventoryCanvas.enabled = true;
        //CountdownTimer.instance.StartTimer();
	GameObject.Find("CountdownTimer").GetComponent<CountdownTimer>().StartTimer();

    }


    public void EndGame() {
        Debug.Log("Game Over!");
        FindObjectOfType<MovePlayer>().enabled = false;
        Invoke("displayGameOverHUD", restartDelay);
        playAgain.onClick.AddListener(Restart);
        mainMenu.onClick.AddListener(displayMainMenu);
    }

    public void AddItemToInventory(Inventory_Item item){
	//	mp.inventory.AddItem(item);
    }


    public void spawnCollectibleTrap(Vector2[] positions){
        var rand1 = new System.Random();
        int randomIndex1 = rand1.Next(positions.Length);
        int randomIndex11 = rand1.Next(positions.Length);


        GameObject[] traps = {  (GameObject)  Resources.Load("Prefabs/CollectableTraps/Hades_Collectable"),
                                (GameObject)  Resources.Load("Prefabs/CollectableTraps/Fire_Collectable"),
                                (GameObject)  Resources.Load("Prefabs/CollectableTraps/Hermes_Collectable"),
                                (GameObject)  Resources.Load("Prefabs/CollectableTraps/Poseidon_Collectable"),
                                (GameObject)  Resources.Load("Prefabs/CollectableTraps/Spike_Collectable"),
                                (GameObject)  Resources.Load("Prefabs/CollectableTraps/Zeusmain_Collectable")
        };

        var rand2 = new System.Random();
        int randomIndex2 = rand2.Next(traps.Length);
        int randomIndex22 = rand2.Next(traps.Length);

        GameObject a = traps[randomIndex2];
        GameObject b = traps[randomIndex22];
        Instantiate(a, positions[randomIndex1], Quaternion.identity);
        Instantiate(b, positions[randomIndex11], Quaternion.identity);
        Debug.Log("collectible trap " + traps[randomIndex2] + " spawned at position " + positions[randomIndex1]);
        Debug.Log("collectible trap " + traps[randomIndex22] + " spawned at position " + positions[randomIndex11]);
    }

    void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
    public static IEnumerator waitForGM(int id, Vector3 position){
	while(GameManager.players.ContainsKey(id) == false){
	    yield return null;
    public void FixedUpdate(){
        if(startOfRound == true){
            spawnCollectibleTrap(gameObject.GetComponent<Trap_positions>().smallMapCoordinates);
            startOfRound = false; 
        }
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
}
