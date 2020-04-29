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

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation) {
        GameObject _player;
        if (_id == Client.instance.myId) {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
	    MovePlayer mp = _player.GetComponent<MovePlayer>();	
	    Instantiate(inventoryPrefab);
	    mp.inventory = inventoryPrefab.transform.GetChild(0).GetComponent<Inventory>();
        }
        else {
            _player = Instantiate(playerPrefab, _position, _rotation);
        }

        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;
        players.Add(_id, _player.GetComponent<PlayerManager>());
    }

    public float restartDelay = 2f;
    public RectTransform panelGameOver;
    public Text txtGameOver;
    public Canvas gameOverCanvas;
    public Canvas inventoryCanvas;
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
	inventoryCanvas = inventoryPrefab.GetComponent<Canvas>();
        gameOverCanvas.enabled = false;
        inventoryCanvas.enabled = true;
    }


    public void EndGame() {
        Debug.Log("Game Over!");
        FindObjectOfType<MovePlayer>().enabled = false;
        Invoke("displayGameOverHUD", restartDelay);
        playAgain.onClick.AddListener(Restart);
        mainMenu.onClick.AddListener(displayMainMenu);
    }


    void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
