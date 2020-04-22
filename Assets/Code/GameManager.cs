using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    public float restartDelay = 2f;
    public RectTransform panelGameOver;
    public Text txtGameOver;
    public Canvas gameOverCanvas;
    public Button playAgain;
    public Button mainMenu;
    
    
    public void displayGameOverHUD(){
	gameOverCanvas.enabled = true;
    }

    public void displayMainMenu(){
	SceneManager.LoadScene("Menu");
    }
    public void Start(){
	gameOverCanvas.enabled = false;
    }

    
    public void EndGame(){
	Debug.Log("Game Over!");
	FindObjectOfType<MovePlayer>().enabled = false;
	Invoke("displayGameOverHUD", restartDelay);
	playAgain.onClick.AddListener(Restart);
	mainMenu.onClick.AddListener(displayMainMenu);
    }


    void Restart(){
	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
