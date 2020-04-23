using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using UnityEngine.UI;
=======

>>>>>>> 2880080ce02949ee9bb39ddeaef254e0111063b2
public class MovePlayer : MonoBehaviour
{
    float speed = 0.0001f;
    public Rigidbody2D rb;
    public Vector3 posInit;
    public int i = 0;
<<<<<<< HEAD
    public Collider cd;
    public Text GameOver;
    public Animator ani;
    public Inventory inventory;
=======
>>>>>>> 2880080ce02949ee9bb39ddeaef254e0111063b2
    
    // Start is called before the first frame update
    void Start(){	
	FindObjectOfType<GameManager>().Start();
    }

    // Update is called once per frame
    void Update()
    {
	if(Input.GetKey(KeyCode.UpArrow) && i > 10){
	    rb.MovePosition(rb.position + new Vector2(0,1));
	    i = 0;
	}
	if(Input.GetKey(KeyCode.DownArrow) && i > 10){
	    rb.MovePosition(rb.position - new Vector2(0,1));
	    i = 0;
	}
	if(Input.GetKey(KeyCode.RightArrow) && i > 10){
	    rb.MovePosition(rb.position + new Vector2(1,0));
	    i = 0;
	}
	if(Input.GetKey(KeyCode.LeftArrow) && i > 10){
	    rb.MovePosition(rb.position - new Vector2(1,0));
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
	}
	if(Input.GetKeyUp(KeyCode.DownArrow)){
	    i = 10;
	}
	if(Input.GetKeyUp(KeyCode.RightArrow)){
	    i = 10;
	}
	if(Input.GetKeyUp(KeyCode.LeftArrow)){
	    i = 10;
<<<<<<< HEAD
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
=======
	}

	    
>>>>>>> 2880080ce02949ee9bb39ddeaef254e0111063b2
    }

    

}
