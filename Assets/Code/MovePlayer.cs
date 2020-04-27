using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MovePlayer : MonoBehaviour
{
    float speed = 0.0001f;
    public Rigidbody2D rb;
    public Vector3 posInit;
    public int i = 0;
    public Collider cd;
    public Text GameOver;
    public Animator ani;
    public Inventory inventory;
<<<<<<< HEAD
	public bool arrowKeysEnabled;
=======
    private float timer;
    private bool activateSleep = false;
>>>>>>> develop
    
    // Start is called before the first frame update
    void Start(){	
	FindObjectOfType<GameManager>().Start();
	arrowKeysEnabled = true;
    }
    
    void Update()
    {
<<<<<<< HEAD

	// If the arrows keys are enabled, allow movement
	if(arrowKeysEnabled == true)
	{
		if(Input.GetKey(KeyCode.UpArrow) && i > 10){
	    	rb.MovePosition(rb.position + new Vector2(0,1));
	    	ani.SetFloat("up", 1f);
	    	//	    	    rb.velocity = new Vector3(0,1,0);
	    	i = 0;
		}
		if(Input.GetKey(KeyCode.DownArrow) && i > 10){
	    	rb.MovePosition(rb.position - new Vector2(0,1));
	    	// 	    	    rb.velocity = new Vector3(0,-1,0);
	    	i = 0;
	    	ani.SetFloat("down", 1f);
		}
		if(Input.GetKey(KeyCode.RightArrow) && i > 10){
	    	rb.MovePosition(rb.position + new Vector2(1,0));
	    	ani.SetFloat("right", 1f);
	    	//	    rb.velocity = new Vector3(1,0,0);
	    	i = 0;
		}
		if(Input.GetKey(KeyCode.LeftArrow) && i > 10){
	    	rb.MovePosition(rb.position - new Vector2(1,0));
	    	ani.SetFloat("left", 1f);
	    	//	    	    rb.velocity = new Vector3(-1,0,0);
	    	i = 0;
		}
=======
	if(activateSleep)
	{
	    timer -= Time.deltaTime;
//	    Debug.Log(timer);
	    if(timer <= 0){
		activateSleep = false;
		setAllAnimatorZero();
	    }
	    else{
		return;
	    }
	}
	if(Input.GetKey(KeyCode.UpArrow)){
	    rb.MovePosition(rb.position + new Vector2(0,1));
	    ani.SetFloat("up", 1f);
	    ActivateSleep(0.25f);
	}
	if(Input.GetKey(KeyCode.DownArrow)){
	    rb.MovePosition(rb.position - new Vector2(0,1));
	    ani.SetFloat("down", 1f);
	    ActivateSleep(0.25f);
	}
	if(Input.GetKey(KeyCode.RightArrow)){
	    rb.MovePosition(rb.position + new Vector2(1,0));
	    ani.SetFloat("right", 1f);
	    ActivateSleep(0.25f);
	}
	if(Input.GetKey(KeyCode.LeftArrow)){
	    rb.MovePosition(rb.position - new Vector2(1,0));
	    ani.SetFloat("left", 1f);
	    ActivateSleep(0.25f);
	}

	if(Input.GetKey(KeyCode.Space)){
	    inventory.PlaceItem();
	    ActivateSleep(0.25f);
>>>>>>> develop
	}

	if(Input.GetKey(KeyCode.L) ){
	    int j = 0;
	    for(j = 0; j < inventory.itemsList.Count; j++){
			if(inventory.itemsList[j].Selected == true){
		    	inventory.itemsList[j].Selected = false;
		    	break;
			}
	    }
	   
	    if(j+1 < inventory.itemsList.Count){
			inventory.SelectItem(inventory.itemsList[j+1], j+1);
	    }
		else 
		{
			inventory.itemsList[j].Selected = true;
	    }
	    ActivateSleep(0.25f);
	}

	if(Input.GetKey(KeyCode.H)){
	    int j = 0;
	    for(j = 0; j < inventory.itemsList.Count; j++){
			if(inventory.itemsList[j].Selected == true){
		    	inventory.itemsList[j].Selected = false;
		    	break;
			}
	    }
	    Debug.Log(inventory.itemsList.Count);
	    Debug.Log(inventory.itemsList[0].Name);
	    Debug.Log(inventory.itemsList[0].Slot);
	    Debug.Log(inventory.itemsList[0].Selected);
	    Debug.Log("j = " + j);
	    if(j-1 >= 0) 
		{
			Debug.Log("This Happened, Item currently displayed is j: ");
			Debug.Log(j);  

			inventory.SelectItem(inventory.itemsList[j-1], j-1);
	    }
		else 
		{
			inventory.itemsList[j].Selected = true;
	    }
	    ActivateSleep(0.25f);
	}
	
	
	
	if(Input.GetKeyUp(KeyCode.UpArrow)){
	    setAllAnimatorZero();
	}
	else if(Input.GetKeyUp(KeyCode.DownArrow)){
	    setAllAnimatorZero();
	}
	else if(Input.GetKeyUp(KeyCode.RightArrow)){
	    setAllAnimatorZero();
	}
	else if(Input.GetKeyUp(KeyCode.LeftArrow)){
	    setAllAnimatorZero();
	}	    
    }

    void OnCollisionEnter2D(Collision2D col){	
<<<<<<< HEAD
		if(col.gameObject.name == "Hole"){
		//	    Debug.Log("OnCollisionEnter2D");
		}
    }



=======
	if(col.gameObject.name == "Hole"){
	    //	    Debug.Log("OnCollisionEnter2D");

	}
	    
    }

    public void ActivateSleep(float forSeconds)
    {
	timer = forSeconds;
	activateSleep = true;
    }
>>>>>>> develop

    private void OnTriggerEnter2D(Collider2D other)
    {

	if(other.gameObject.name == "Hole"){
<<<<<<< HEAD
	//	    Debug.Log("OnCollisionEnter2D TRIGGER");
	    FindObjectOfType<GameManager>().EndGame();
	}

	// Deals with trap interaction. (e.g. kills character if they stand on a trap)
	else if (other.GetComponent<TrapInteraction>() != null)
	{
		//arrowKeysEnabled = false;
		TrapInteraction TrapScript = other.GetComponent<TrapInteraction>();
		string name = TrapScript.trap.trapName;
		if(name == "PoseidonTrap"){
			StartCoroutine(findPoseidonDirection(TrapScript));
		}
		else if(name == "HadesTrap"){
			Debug.Log("Death by Hades!");
			FindObjectOfType<GameManager>().EndGame();
		}
		else if(name == "FireTrap"){
			Debug.Log("Death by Fire!");
			FindObjectOfType<GameManager>().EndGame();
		}
		else if(name == "SpikeTrap"){
			Debug.Log("Death by Spike!");
			FindObjectOfType<GameManager>().EndGame();
		}
		else if(name == "ZeusMainTrap"){
			Debug.Log("Death by Zeus!");
			FindObjectOfType<GameManager>().EndGame();
		}
	}

	// Deals with Zeus' diagonal bolts of lightning.
	else if (other.GetComponent<ZeusDiagonal>() != null)
	{
		Debug.Log("Death by Zeus Diagonal!");
		FindObjectOfType<GameManager>().EndGame();
	}

	// Deals with pickup interaction. (e.g. the Hermes status effect)
	else if (other.GetComponent<Pickup>() != null)
	{
		Pickup PickupScript = other.GetComponent<Pickup>();
		string name = PickupScript.trap.trapName;
		if(name == "HermesPickup"){
			Debug.Log("Red Bull gives you Wiiings");
		}
	}
=======
	    //	    Debug.Log("OnCollisionEnter2D TRIGGER");
	    FindObjectOfType<GameManager>().EndGame();
	}

	/*if(other.gameObject.name == "PoseidonTrap"){
	  Debug.Log("Go to sea");
	  TrapInteraction PoseidonScript = other.GetComponent<TrapInteraction>();
	  Debug.Log("The direction of Poseidon is: " + PoseidonScript.poseidonDirection);
	  }*/
>>>>>>> develop

	
	Inventory_Item item = other.GetComponent<Inventory_Item>();

	if(item != null){
	    inventory.AddItem(item);
	    if(inventory.itemsList.Count == 1)
		inventory.SelectItem(item, 0);
	    //	    Debug.Log(item.Selected);
	}
    }


    private void setAllAnimatorZero(){
	ani.SetFloat("left", 0f);
	ani.SetFloat("right", 0f);
	ani.SetFloat("up", 0f);
	//This comment here is for Manu, who asked me to comment my code. There you go buddy! :*
	ani.SetFloat("down", 0f);
	ani.SetFloat("hole", 0f);
    }

<<<<<<< HEAD
	// Function for moving the player with the Poseidon trap.
	// For best use, arrowKeysEnabled should be set to false.
	// Can be cleaned up with a seperate function for each of these movements.
	IEnumerator findPoseidonDirection(TrapInteraction TrapScript)
	{
		// Wait while the poseidon trap is configuring its rotation.
		while(TrapScript.poseidonDirectionReady == false)
		{
			yield return null;
		}

		// Get the direction 0 to 3, which indicates how many times the
		// trap has been rotates by 90 degrees.
		int direction = TrapScript.poseidonDirection;
		Debug.Log("The direction of Poseidon is: " + direction);

		// If the poseidon direction is up, move the player up.
		if(direction == 0)
		{
			rb.MovePosition(rb.position + new Vector2(0,1));
	    	ani.SetFloat("up", 1f);
			yield return new WaitForSeconds(0.1f);
			ani.SetFloat("up", 0f);
		}
		// If the poseidon direction is right, move the player right.
		else if(direction == 1)
		{
			rb.MovePosition(rb.position + new Vector2(-1,0));
	    	ani.SetFloat("right", 1f);
			yield return new WaitForSeconds(0.1f);
			ani.SetFloat("right", 0f);
		}
		// If the poseidon direction is down, move the player down.
		else if(direction == 2)
		{
			rb.MovePosition(rb.position + new Vector2(0,-1));
	    	ani.SetFloat("down", 1f);
			yield return new WaitForSeconds(0.1f);
			ani.SetFloat("down", 0f);
		}
		// If the poseidon direction is left, move the player left.
		else
		{
			rb.MovePosition(rb.position + new Vector2(1,0));
	    	ani.SetFloat("left", 1f);
			yield return new WaitForSeconds(0.1f);
			ani.SetFloat("left", 0f);
		}

		yield return 0;
	}
=======
    public Vector3 getPlayerPosition(){
	return transform.position;
    }

    
>>>>>>> develop

}
