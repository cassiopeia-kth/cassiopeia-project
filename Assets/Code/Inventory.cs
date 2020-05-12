using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public Inventory_Item[] itemList = new Inventory_Item[7];
    //    public Inventory_Item[] itemList;
    public GameObject spikeTrap;
    public GameObject fireTrap;
    public GameObject zeusmainTrap;
    public GameObject hadesTrap;
    public GameObject poseidonTrap;
    public GameObject hermesTrap;



    public void addToArray(Inventory_Item item){
	for(int i = 0; i < 7; i++){
	    if(itemList[i] == null){
		itemList[i] = item;
		return;
	    }
	}
    }


    public bool isEmpty(){
	for(int i = 0; i < 7; i++){
	    if(itemList[i] != null)
		return false;
	}
	return true;
    }
    
    public void removeFromArray(int index){
	itemList[index] = null;
    }
    
    
    public void AddItem(Inventory_Item item){
	bool full = true;
	int i;
	for(i = 0; i < 7; i++){
	    if(itemList[i] == null){
		full = false;
		break;
	    }
	}
	if(!full){
	    BoxCollider2D collider = (item as MonoBehaviour).GetComponent<BoxCollider2D>();
	    if(collider.enabled){
		collider.enabled = false;
		addToArray(item);
		item.OnPickup();
		itemList[i] = item;
		Image image = transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>();
		Debug.Log(image.enabled);
		image.enabled = true;
		image.sprite = item.Image;
		Debug.Log("got here but the image is not displayed.");
	    }
	}
	//for(i = 0; i < 7; i++){
	//Debug.Log(itemList[i]);
	//}
    }

    public void hoverRight(){
	if(isEmpty()){
	    Image image = transform.GetChild(0).GetChild(0).GetComponent<Image>();
	    image.color = new Color(image.color.r, image.color.g, image.color.b, 0.7f);
	    return;
	}
	
	int i;
	for(i = 0; i < 7; i++){
	    Image image = transform.GetChild(i).GetChild(0).GetComponent<Image>();
	    if(image.color.a == 0.7f){
		image.color = new Color(image.color.r, image.color.g, image.color.b, 0.3f);
		break;
	    }
	}
	for(int j = i+1; j < 7; j++){
	    if(itemList[j] != null){
		Image image = transform.GetChild(j).GetChild(0).GetComponent<Image>();
		image.color = new Color(image.color.r, image.color.g, image.color.b, 0.7f);
		return;
	    }
	}
	for(int j = 0; j < 7; j++){
	    if(itemList[j] != null){
		Image image = transform.GetChild(j).GetChild(0).GetComponent<Image>();
		image.color = new Color(image.color.r, image.color.g, image.color.b, 0.7f);
		return;
	    }

	}
    }

    public void hoverLeft(){
	if(isEmpty()){
	    Image image = transform.GetChild(0).GetChild(0).GetComponent<Image>();
	    image.color = new Color(image.color.r, image.color.g, image.color.b, 0.7f);
	    return;
	}

	int i;
	for(i = 6; i >= 0; i--){
	    Image image = transform.GetChild(i).GetChild(0).GetComponent<Image>();
	    if(image.color.a == 0.7f){
		image.color = new Color(image.color.r, image.color.g, image.color.b, 0.3f);
		break;
	    }
	}
	for(int j = i-1; j >= 0; j--){
	    if(itemList[j] != null){
		Image image = transform.GetChild(j).GetChild(0).GetComponent<Image>();
		image.color = new Color(image.color.r, image.color.g, image.color.b, 0.7f);
		return;
	    }   
	}
	for(int j = 6; j >= 0; j--){
	    if(itemList[j] != null){
		Image image = transform.GetChild(j).GetChild(0).GetComponent<Image>();
		image.color = new Color(image.color.r, image.color.g, image.color.b, 0.7f);
		return;
	    }
	    
	}
    }

    
    public void PlaceItem(){
	for(int i = 0; i < 7; i++){
	    Image sprite = transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>();
	    Image hover = transform.GetChild(i).GetChild(0).GetComponent<Image>();
	    if(hover.color.a == 0.7f && itemList[i] != null){
		Vector3 playerPos = FindObjectOfType<MovePlayer>().getPlayerPosition();
		//Debug.Log(itemList[i].Name);
		switch(itemList[i].Name){
		    case "Hades_Collectable":
			FindObjectOfType<GameManager>().spawnTrap(Client.instance.myId, hadesTrap, playerPos, transform.rotation);
			break;
		    case "Hades_Collectable(Clone)":
			FindObjectOfType<GameManager>().spawnTrap(Client.instance.myId, hadesTrap, playerPos, transform.rotation);
			break;
		    case "Spike_Collectable":
			FindObjectOfType<GameManager>().spawnTrap(Client.instance.myId, spikeTrap, playerPos, transform.rotation);
			break;
		    case "Spike_Collectable(Clone)":
			FindObjectOfType<GameManager>().spawnTrap(Client.instance.myId, spikeTrap, playerPos, transform.rotation);
			break;
		    case "Poseidon_Collectable":
			FindObjectOfType<GameManager>().spawnTrap(Client.instance.myId, poseidonTrap, playerPos, transform.rotation);
			break;
		    case "Poseidon_Collectable(Clone)":
			FindObjectOfType<GameManager>().spawnTrap(Client.instance.myId, poseidonTrap, playerPos, transform.rotation);
			break;
		    case "Zeusmain_Collectable":
			FindObjectOfType<GameManager>().spawnTrap(Client.instance.myId, zeusmainTrap, playerPos, transform.rotation);
			break;
		    case "Zeusmain_Collectable(Clone)":
			FindObjectOfType<GameManager>().spawnTrap(Client.instance.myId, zeusmainTrap, playerPos, transform.rotation);
			break;
		    case "Fire_Collectable":
			FindObjectOfType<GameManager>().spawnTrap(Client.instance.myId, fireTrap, playerPos, transform.rotation);
			break;
		    case "Fire_Collectable(Clone)":
			FindObjectOfType<GameManager>().spawnTrap(Client.instance.myId, fireTrap, playerPos, transform.rotation);
			break;

		}
		removeFromArray(i);
		sprite.enabled = false;
		hover.color = new Color(hover.color.r, hover.color.g, hover.color.b, 0.3f);
		fixHoverToClosest(i);
		break;
	    }
	}
    }

    public void fixHoverToClosest(int index){
	for(int i = index-1; i >= 0; i--){
	    Image hover = transform.GetChild(i).GetChild(0).GetComponent<Image>();
	    if(itemList[i] != null){
		hover.color = new Color(hover.color.r, hover.color.g, hover.color.b, 0.7f);
		return;
	    }
	}
	for(int i = index+1; i < 7; i++){
	    Image hover = transform.GetChild(i).GetChild(0).GetComponent<Image>();
	    if(itemList[i] != null){
		hover.color = new Color(hover.color.r, hover.color.g, hover.color.b, 0.7f);
		return;
	    }
	}

	Image hover_image = transform.GetChild(0).GetChild(0).GetComponent<Image>();
	hover_image.color = new Color(hover_image.color.r, hover_image.color.g, hover_image.color.b, 0.7f);
	

    }
    
    void Start(){
	for(int i = 0; i < 7; i++){
	    itemList[i] = null;
	    Image sprite = transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>();
	    sprite.enabled = false;
	    sprite.sprite = null;
	    Image frame = transform.GetChild(i).GetChild(0).GetComponent<Image>();
	    frame.color = new Color(frame.color.r, frame.color.g, frame.color.b, 0.3f);
	}

	Image image = transform.GetChild(0).GetChild(0).GetComponent<Image>();
	image.color = new Color(image.color.r, image.color.g, image.color.b, 0.7f);


	spikeTrap = (GameObject) Resources.Load("Prefabs/Traps/Spike_Trap");
	poseidonTrap = (GameObject) Resources.Load("Prefabs/Traps/Poseidon_Trap");
	hermesTrap = (GameObject) Resources.Load("Prefabs/Traps/Hermes_Trap");
	hadesTrap = (GameObject) Resources.Load("Prefabs/Traps/Hades_Trap");
	fireTrap = (GameObject) Resources.Load("Prefabs/Traps/Fire_Trap");
	zeusmainTrap = (GameObject) Resources.Load("Prefabs/Traps/Zeusmain_Trap");
    }

    void Update(){
	
    }
}

