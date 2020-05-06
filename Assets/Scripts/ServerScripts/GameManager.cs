using System.Collections;
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

    public void SpawnPlayer(int _id, string _username, Vector3 _position, string _charType) {
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
        //here call the prefab with name
        

	    _player = Instantiate((GameObject)Resources.Load($"Prefabs/Player/{_charType}", typeof(GameObject)), _position, new Quaternion(0,0,0,0));
	    mpo = _player.AddComponent<MovePlayerOnline>();
	    mpo.rb = FindObjectOfType<Rigidbody2D>();
	    mpo.ani = FindObjectOfType<Animator>();	    
	    GameObject inventoryHUD = Instantiate(inventoryPrefab);
	    mpo.inventory = inventoryHUD.transform.GetChild(0).gameObject.AddComponent<Inventory>();
	    inventoryCanvas = inventoryHUD.transform.GetComponent<Canvas>();
	    inventoryHUD.SetActive(false);
	}

        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;
        players.Add(_id, _player.GetComponent<PlayerManager>());
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


    void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
