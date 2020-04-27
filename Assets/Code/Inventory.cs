using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 7;

    public Inventory_Item[] itemList = new Inventory_Item[7];

    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> HoverFirst;


    public GameObject spikeTrap;



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
	//This comment is also for Manu, if you actually do read this, it means that I have a secret copy of the whole project in which I indeed commented everything I did, so either try to break into my system and find it or kindly ask for it. Much luv :*
	if(!full){
	    BoxCollider2D collider = (item as MonoBehaviour).GetComponent<BoxCollider2D>();
	    if(collider.enabled){
		collider.enabled = false;
		addToArray(item);
		item.OnPickup();
		itemList[i] = item;
		Image image = transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>();
		image.enabled = true;
		image.sprite = item.Image;
	    }
	}
    }

	


    public void SelectItem(Inventory_Item item, int i){
	item.Slot = i;
	HoverFirst(this, new InventoryEventArgs(item));
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
		Instantiate(spikeTrap, playerPos , transform.rotation);
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
    }

    Image image = transform.GetChild(0).GetChild(0).GetComponent<Image>();
    image.color = new Color(image.color.r, image.color.g, image.color.b, 0.7f);
}

void Update(){
	
}
}

