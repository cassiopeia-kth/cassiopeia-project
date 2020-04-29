using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MovePlayer : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator ani;
    public Inventory inventory;
    public bool arrowKeysEnabled;
	private bool flying = false;
    private float timer;
    private bool activateSleep = false;
    
    // Start is called before the first frame update
    void Start(){	
	FindObjectOfType<GameManager>().Start();
	arrowKeysEnabled = true;
    }
    
    void Update()
    {
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
				rb.MovePosition(rb.position + new Vector2(0, 1));
				ani.SetFloat("up", 1f);
				ActivateSleep(0.25f);
			}
			if (Input.GetKey(KeyCode.DownArrow))
			{
				rb.MovePosition(rb.position - new Vector2(0, 1));
				ani.SetFloat("down", 1f);
				ActivateSleep(0.25f);
			}
			if (Input.GetKey(KeyCode.RightArrow))
			{
				rb.MovePosition(rb.position + new Vector2(1, 0));
				ani.SetFloat("right", 1f);
				ani.SetFloat("FacingLeft", 0f);
				ActivateSleep(0.25f);
			}
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				rb.MovePosition(rb.position - new Vector2(1, 0));
				ani.SetFloat("left", 1f);
				ani.SetFloat("FacingLeft", 1f);
				ActivateSleep(0.25f);
			}

		}

	if(Input.GetKey(KeyCode.Space)){
	    inventory.PlaceItem();
	    ActivateSleep(0.25f);
	}

	if(Input.GetKey(KeyCode.L) ){
	    inventory.hoverRight();
	    ActivateSleep(0.25f);
	}

	if(Input.GetKey(KeyCode.H)){
	    inventory.hoverLeft();
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
	if(col.gameObject.name == "Hole"){
	}   
    }

    public void ActivateSleep(float forSeconds)
    {
	timer = forSeconds;
	activateSleep = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

	if(other.gameObject.name == "Hole" && flying == false){
			//	    Debug.Log("OnCollisionEnter2D TRIGGER");
		arrowKeysEnabled = false;
		StartCoroutine(HoleDeath());
	}

	// Deals with trap interaction. (e.g. kills character if they stand on a trap)
	else if (other.GetComponent<TrapInteraction>() != null && flying == false)
	{
	    arrowKeysEnabled = false;
	    TrapInteraction TrapScript = other.GetComponent<TrapInteraction>();
	    string name = TrapScript.trap.trapName;
	    if(name == "PoseidonTrap"){
		StartCoroutine(findPoseidonDirection(TrapScript));
	    }
	    else if(name == "HadesTrap"){
		Debug.Log("Death by Hades!");
		StartCoroutine(HadesDeath());
		//FindObjectOfType<GameManager>().EndGame();
	    }
	    else if(name == "FireTrap"){
		StartCoroutine(FireTrap());
		Debug.Log("Death by Fire!");
	    }
	     else if(name == "SpikeTrap"){
		StartCoroutine(spikeTrap());
		Debug.Log("Death by Spike!");
	    }
	    else if(name == "ZeusMainTrap"){
		Debug.Log("Death by Zeus!");
		StartCoroutine(ZeusDeath());
		//FindObjectOfType<GameManager>().EndGame();
	    }
	}

	// Deals with Zeus' diagonal bolts of lightning.
	else if (other.GetComponent<ZeusDiagonal>() != null && flying == false)
	{
	    Debug.Log("Death by Zeus Diagonal!");
		StartCoroutine(ZeusDiagonalDeath());
		FindObjectOfType<GameManager>().EndGame();
	}

	// Deals with pickup interaction. (e.g. the Hermes status effect)
	else if (other.GetComponent<Pickup>() != null)
	{
	    Pickup PickupScript = other.GetComponent<Pickup>();
	    string name = PickupScript.trap.trapName;
	    if(name == "HermesPickup"){
			ani.SetBool("Flying", true);
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
		ani.SetFloat("FacingLeft", 0f);
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
		FindObjectOfType<GameManager>().EndGame();
		gameObject.SetActive(false);
		yield return 0;
	}

	IEnumerator HadesDeath()
	{
		Vector3 pos = gameObject.transform.position;
		yield return new WaitForSeconds(3f);
		ani.SetFloat("HadesTrap", 1f);
		yield return new WaitForSeconds(1f);
		FindObjectOfType<GameManager>().EndGame();
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
		FindObjectOfType<GameManager>().EndGame();
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
		FindObjectOfType<GameManager>().EndGame();
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
		FindObjectOfType<GameManager>().EndGame();
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
		FindObjectOfType<GameManager>().EndGame();
		yield return new WaitForSeconds(2f);
		gameObject.SetActive(false);
		GameObject BishopGrave = (GameObject)Resources.Load("Prefabs/Graves/BishopGrave", typeof(GameObject));
		GameObject actualGrave = Instantiate(BishopGrave, pos, Quaternion.identity);
		yield return 0;

	}


}
