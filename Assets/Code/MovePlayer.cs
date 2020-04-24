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
    
    // Start is called before the first frame update
    void Start(){	
	FindObjectOfType<GameManager>().Start();
    }

    // Update is called once per frame
    void Update()
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

	if(Input.GetKey(KeyCode.L) && i > 10 ){
	    int j = 0;
	    for(j = 0; j < inventory.itemsList.Count; j++){
		if(inventory.itemsList[j].Selected == true){
		    inventory.itemsList[j].Selected = false;
		    break;
		}
	    }
	   
	    if(j+1 < inventory.itemsList.Count){
		inventory.SelectItem(inventory.itemsList[j+1], j+1);
	    }else {
		inventory.itemsList[j].Selected = true;
	    }
	    i = 0;
	}

	if(Input.GetKey(KeyCode.H) && i > 10){
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
	    if(j-1 >= 0) {
		Debug.Log("This Happened, Item currently displayed is j: ");
		Debug.Log(j);  

		inventory.SelectItem(inventory.itemsList[j-1], j-1);
	    }else {
		inventory.itemsList[j].Selected = true;
	    }
	    i = 0;
	}
	
	
	i++;
	
	if(Input.GetKeyUp(KeyCode.UpArrow)){
	    i = 10;
	    setAllAnimatorZero();
	}
	else if(Input.GetKeyUp(KeyCode.DownArrow)){
	    i = 10;
	    setAllAnimatorZero();
	}
	else if(Input.GetKeyUp(KeyCode.RightArrow)){
	    i = 10;
	    setAllAnimatorZero();
	}
	else if(Input.GetKeyUp(KeyCode.LeftArrow)){
	    i = 10;
	    setAllAnimatorZero();
	}	    
    }

    void OnCollisionEnter2D(Collision2D col){	
	if(col.gameObject.name == "Hole"){
//	    Debug.Log("OnCollisionEnter2D");

	}
	    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
	if(other.gameObject.name == "Hole"){
//	    Debug.Log("OnCollisionEnter2D TRIGGER");
	    FindObjectOfType<GameManager>().EndGame();
	}

	// ED ADDING CODE START
	if (other.GetComponent<TrapInteraction>() != null)
	{
		TrapInteraction TrapScript = other.GetComponent<TrapInteraction>();
		string name = TrapScript.trap.trapName;
		if(name == "PoseidonTrap"){
			StartCoroutine(findPoseidonDirection(TrapScript));
		}
	}
	
	// ED ADDING CODE END

	
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
	ani.SetFloat("down", 0f);
	ani.SetFloat("hole", 0f);
    }

	// ED ADD
	IEnumerator findPoseidonDirection(TrapInteraction TrapScript)
	{
		while(TrapScript.poseidonDirectionReady == false)
		{
			yield return null;
		}
		int direction = TrapScript.poseidonDirection;
		Debug.Log("The direction of Poseidon is: " + direction);
		if(direction == 0)
		{
			rb.MovePosition(rb.position + new Vector2(0,1));
	    	ani.SetFloat("up", 1f);
			yield return new WaitForSeconds(0.1f);
			ani.SetFloat("up", 0f);
			// wait then set back to 0f
		}
		else if(direction == 1)
		{
			rb.MovePosition(rb.position + new Vector2(-1,0));
	    	ani.SetFloat("right", 1f);
			yield return new WaitForSeconds(0.1f);
			ani.SetFloat("right", 0f);
			// wait then set back to 0f
		}
		else if(direction == 2)
		{
			rb.MovePosition(rb.position + new Vector2(0,-1));
	    	ani.SetFloat("down", 1f);
			yield return new WaitForSeconds(0.1f);
			ani.SetFloat("down", 0f);
			// wait then set back to 0f
		}
		else
		{
			rb.MovePosition(rb.position + new Vector2(1,0));
	    	ani.SetFloat("left", 1f);
			yield return new WaitForSeconds(0.1f);
			ani.SetFloat("left", 0f);
			// wait then set back to 0f
		}

		yield return 0;
	}
	// ED END ADD

}
