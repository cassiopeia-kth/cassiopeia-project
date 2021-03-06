using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MovePlayerOnline : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator ani;
    public Inventory inventory;
    public bool arrowKeysEnabled;
    private bool flying = false;
    private float timer;
    private bool activateSleep = false;
    public PlayerManager pm;
    public float moveSpeed = 10f;
    public Transform movePoint;
    public LayerMask whatStopsMovement;
    private bool flyingAllowed = true;

    public int id;

    public bool WAVE = false;
    
  
    // Start is called before the first frame update
    void Start(){
	FindObjectOfType<GameManager>().Start();

	arrowKeysEnabled = true;
	pm = FindObjectOfType<PlayerManager>();

  foreach (PlayerManager player in GameManager.instance.getPlayers().Values) {
      if (player.id == id) {
          pm = player;
      }
  }

//	inventory = GameObject.Find("InventoryPanel").GetComponent<Inventory>();
	movePoint = transform.GetChild(0);
	movePoint.parent = null;
	whatStopsMovement = LayerMask.GetMask("StopMovement");

    }

    public void movePlayer(Vector3 position){
//	transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed*Time.deltaTime);
	if(!WAVE){
	transform.position = movePoint.position;
	if(Vector3.Distance(transform.position, movePoint.position) <= .05f){

		if(!Physics2D.OverlapCircle(movePoint.position + position, .2f, whatStopsMovement))
		    movePoint.position += position;
	}
	}
    }

    void Update()
    {

	bool timerElapsed = FindObjectOfType<GameManager>().timerZero;

	if (timerElapsed && flying)
	{
	    flyingAllowed = false;
		}

	if (!timerElapsed && !flyingAllowed)
	{
	    ani.SetBool("Flying", false);
	    flying = false; // change animation
	    flyingAllowed = true;
	}



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

		if (arrowKeysEnabled)
		{
			if (Input.GetKey(KeyCode.UpArrow))
			{
//				rb.MovePosition(rb.position + new Vector2(0, 1));
				ani.SetFloat("up", 1f);
//				ActivateSleep(0.25f);
			}
			if (Input.GetKey(KeyCode.DownArrow))
			{
//				rb.MovePosition(rb.position - new Vector2(0, 1));
				ani.SetFloat("down", 1f);
//				ActivateSleep(0.25f);
			}
			if (Input.GetKey(KeyCode.RightArrow))
			{
//				rb.MovePosition(rb.position + new Vector2(1, 0));
				ani.SetFloat("right", 1f);
				ani.SetFloat("FacingLeft", 0f);
//				ActivateSleep(0.25f);
			}
			if (Input.GetKey(KeyCode.LeftArrow))
			{
//				rb.MovePosition(rb.position - new Vector2(1, 0));
				ani.SetFloat("left", 1f);
				ani.SetFloat("FacingLeft", 1f);
//				ActivateSleep(0.25f);
			}

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
	if(col.gameObject.name == "Hole"){
	}
    }

    public void ActivateSleep(float forSeconds)
    {
	timer = forSeconds;
	activateSleep = true;
    }


  public bool isOverAHole = false;

    private void OnTriggerExit2D(Collider2D other){
	if(other.gameObject.name == "Hole"){
	    isOverAHole = false;
	    Debug.Log("On collision exit is called");
	}
    }
    public void holeDeathStartRound(){
	    arrowKeysEnabled = false;
	    pm.isAlive = false;
	    StartCoroutine(HoleDeath());
    }
    


    
    private void OnTriggerEnter2D(Collider2D other)
    {
	if(other.gameObject.name == "Hole"){

	    isOverAHole = true;
	    Debug.Log("shortest debug ever");
	}

	if(other.gameObject.name == "Hole" && flying == false){
	    	    WAVE = true;
			//	    Debug.Log("OnCollisionEnter2D TRIGGER");
		arrowKeysEnabled = false;
		//pm.isAlive = false;
		pm.isDead = true; // ADDED FOR WINNER LOGIC
		StartCoroutine(HoleDeath());
	}

	// Deals with trap interaction. (e.g. kills character if they stand on a trap)
	else if (other.GetComponent<TrapInteraction>() != null && flying == false)
	{
	    arrowKeysEnabled = false;
//	    pm.isAlive = false;
	    TrapInteraction TrapScript = other.GetComponent<TrapInteraction>();
	    string name = TrapScript.trap.trapName;
			GameManager.killer = TrapScript.killer;
	    if(name == "PoseidonTrap"){
		//pm.isDead = true; // ADDED FOR WINNER LOGIC
		StartCoroutine(findPoseidonDirection(TrapScript));
	    }
	    else if(name == "HadesTrap"){
		Debug.Log("Death by Hades!");
		pm.isDead = true; // ADDED FOR WINNER LOGIC
		WAVE = true;
		StartCoroutine(HadesDeath());
		//FindObjectOfType<GameManager>().EndGame();
	    }
	    else if(name == "FireTrap"){
            pm.isDead = true; // ADDED FOR WINNER LOGIC
		WAVE = true;
		StartCoroutine(FireTrap());
		Debug.Log("Death by Fire!");
	    }
	     else if(name == "SpikeTrap"){
    WAVE = true;
    pm.isDead = true; // ADDED FOR WINNER LOGIC
		StartCoroutine(spikeTrap());
		Debug.Log("Death by Spike!");
	    }
	    else if(name == "ZeusMainTrap"){
            pm.isDead = true; // ADDED FOR WINNER LOGIC
            WAVE = true;
		Debug.Log("Death by Zeus!");
		StartCoroutine(ZeusDeath());
		//FindObjectOfType<GameManager>().EndGame();
	    }
	}

	// Deals with Zeus' diagonal bolts of lightning.
	else if (other.GetComponent<ZeusDiagonal>() != null && flying == false)
	{
	    Debug.Log("Death by Zeus Diagonal!");
//	    pm.isAlive = false;
    pm.isDead = true; // ADDED FOR WINNER LOGIC
		StartCoroutine(ZeusDiagonalDeath());
		//FindObjectOfType<GameManager>().EndGame();
	}

	// Deals with pickup interaction. (e.g. the Hermes status effect)
	else if (other.GetComponent<Pickup>() != null)
	{
	    other.enabled = false;
	    Pickup PickupScript = other.GetComponent<Pickup>();
	    string name = PickupScript.trap.trapName;
	    if(name == "HermesPickup"){
		Debug.Log("Red Bull gives you Wiiings");
		ani.SetBool("Flying", true);
		flying = true;
	    }
	}
	Inventory_Item item = other.GetComponent<Inventory_Item>();

	if(item != null){
	    inventory.AddItem(item);
	}
    }


    private void setAllAnimatorZero(){
	ani.SetFloat("left", 0f);
	ani.SetFloat("right", 0f);
	ani.SetFloat("up", 0f);
	//This comment here is for Manu, who asked me to comment my code. There you go buddy! :*
	ani.SetFloat("down", 0f);
	//ani.SetFloat("hole", 0f);
    }

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
	    ani.SetFloat("up", 1f);
	    yield return new WaitForSeconds(0.1f);
	    ani.SetFloat("up", 0f);
	}
	// If the poseidon direction is right, move the player right.
	else if(direction == 1)
	{
	    ani.SetFloat("right", 1f);
		ani.SetFloat("FacingLeft", 0f);
		yield return new WaitForSeconds(0.1f);
	    ani.SetFloat("right", 0f);
	}
	// If the poseidon direction is down, move the player down.
	else if(direction == 2)
	{
	    ani.SetFloat("down", 1f);
	    yield return new WaitForSeconds(0.1f);
	    ani.SetFloat("down", 0f);
	}
	// If the poseidon direction is left, move the player left.
	else
	{
	    ani.SetFloat("left", 1f);
	    ani.SetFloat("FacingLeft", 1f);
	    yield return new WaitForSeconds(0.1f);
	    ani.SetFloat("left", 0f);
	}

	yield return 0;
    }
    public Vector3 getPlayerPosition(){
	return transform.position;
    }

	IEnumerator HoleDeath()
	{
		yield return new WaitForSeconds(0.5f);
		ani.SetFloat("HadesTrap", 1f);
		yield return new WaitForSeconds(1f);
    FindObjectOfType<GameManager>().checkWinner();//ADDED FOR WINNER LOGIC
		//FindObjectOfType<GameManager>().EndGame();
		gameObject.SetActive(false);
		yield return 0;
	}

	IEnumerator HadesDeath()
	{
		Vector3 pos = gameObject.transform.position;
		yield return new WaitForSeconds(3f);
		ani.SetFloat("HadesTrap", 1f);
		yield return new WaitForSeconds(1f);
    FindObjectOfType<GameManager>().checkWinner();//ADDED FOR WINNER LOGIC
		//FindObjectOfType<GameManager>().EndGame();
		yield return new WaitForSeconds(2f);
		gameObject.SetActive(false);
		GameObject BishopGrave = (GameObject)Resources.Load("Prefabs/Graves/BishopGrave", typeof(GameObject));
		GameObject actualGrave = Instantiate(BishopGrave, pos, Quaternion.identity);
		yield return 0;
	}

	IEnumerator ZeusDeath()
	{
		Vector3 pos = gameObject.transform.position;
		yield return new WaitForSeconds(3f);
		ani.SetFloat("ZeusTrap", 1f);
		yield return new WaitForSeconds(1f);
    FindObjectOfType<GameManager>().checkWinner();//ADDED FOR WINNER LOGIC
		//FindObjectOfType<GameManager>().EndGame();
		yield return new WaitForSeconds(2f);
		gameObject.SetActive(false);
		GameObject BishopGrave = (GameObject)Resources.Load("Prefabs/Graves/BishopGrave", typeof(GameObject));
		GameObject actualGrave = Instantiate(BishopGrave, pos, Quaternion.identity);
		yield return 0;
	}

	IEnumerator ZeusDiagonalDeath()
	{
		Vector3 pos = gameObject.transform.position;
		yield return new WaitForSeconds(1f);
		ani.SetFloat("ZeusTrap", 1.5f);
		yield return new WaitForSeconds(1f);
    FindObjectOfType<GameManager>().checkWinner();//ADDED FOR WINNER LOGIC
		//FindObjectOfType<GameManager>().EndGame();
		yield return new WaitForSeconds(2f);
		gameObject.SetActive(false);
		GameObject BishopGrave = (GameObject)Resources.Load("Prefabs/Graves/BishopGrave", typeof(GameObject));
		GameObject actualGrave = Instantiate(BishopGrave, pos, Quaternion.identity);
		yield return 0;
	}

  IEnumerator spikeTrap() {
		Vector3 pos = gameObject.transform.position;
		yield return new WaitForSeconds(3f);
		ani.SetFloat("SpikeTrap", 1f);
		yield return new WaitForSeconds(1f);
    FindObjectOfType<GameManager>().checkWinner(); //ADDED FOR WINNER LOGIC
		//FindObjectOfType<GameManager>().EndGame();
		yield return new WaitForSeconds(2f);
		gameObject.SetActive(false);
		GameObject BishopGrave = (GameObject)Resources.Load("Prefabs/Graves/BishopGrave", typeof(GameObject));
		GameObject actualGrave = Instantiate(BishopGrave, pos, Quaternion.identity);
		yield return 0;
	}

IEnumerator FireTrap()
    {
		Vector3 pos = gameObject.transform.position;
		yield return new WaitForSeconds(3f);
		ani.SetFloat("FireTrap", 1f);
		yield return new WaitForSeconds(1f);
    FindObjectOfType<GameManager>().checkWinner();//ADDED FOR WINNER LOGIC
	//	FindObjectOfType<GameManager>().EndGame();
		yield return new WaitForSeconds(2f);
		gameObject.SetActive(false);
		GameObject BishopGrave = (GameObject)Resources.Load("Prefabs/Graves/BishopGrave", typeof(GameObject));
		GameObject actualGrave = Instantiate(BishopGrave, pos, Quaternion.identity);
		yield return 0;

	}

}
