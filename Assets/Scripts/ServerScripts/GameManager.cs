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

        spawnCollectibleTrap(gameObject.GetComponent<Trap_positions>().smallMapCoordinates);
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

    public void spawnTrap(int id, GameObject trap, Vector3 pos, Quaternion rot)
    {
        if (id == Client.instance.myId)
        {
            StartCoroutine(markTrap(pos, rot));
        }
       
        if(trap.name == "Zeusmain_Trap")
        {
            pos.y = pos.y + 0.5f;
        }
        
        GameObject laid_trap = Instantiate(trap, pos, rot);
        laid_trap.GetComponent<TrapInteraction>().killer = players[id].username;
        Debug.Log("Trap laid by player: " + laid_trap.GetComponent<TrapInteraction>().killer);
    }

    public void spawnCollectibleTrap(Vector2[] positions){
        var rand = new System.Random();
        int randomIndex = rand.Next(positions.Length);

        GameObject a =(GameObject)  Resources.Load("Prefabs/CollectableTraps/Hades_Collectable");
        Instantiate(a, positions[randomIndex], Quaternion.identity);
        Debug.Log("collectible trap spawned");
    }

    void Update(){
        
    }


    IEnumerator markTrap (Vector3 pos, Quaternion rot)
    {
        GameObject cross = (GameObject)Resources.Load("Prefabs/Traps/Cross", typeof(GameObject));
        GameObject actualCross = Instantiate(cross, pos, rot);
        yield return new WaitForSeconds(1.5f);
        actualCross.GetComponent<SpriteRenderer>().enabled = false;
        actualCross.SetActive(false);
    }
}
