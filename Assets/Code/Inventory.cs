using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 7;

    public List<Inventory_Item> itemsList = new List<Inventory_Item>();

    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> HoverFirst;


    public GameObject spikeTrap;


    
    public void AddItem(Inventory_Item item){

	if(itemsList.Count < SLOTS){
	    BoxCollider2D collider = (item as MonoBehaviour).GetComponent<BoxCollider2D>();
	    if(collider.enabled){
		collider.enabled = false;

		itemsList.Add(item);
		item.OnPickup();

		if(ItemAdded != null){
		    ItemAdded(this, new InventoryEventArgs(item));
		}
	     }
	}

//	Debug.Log(itemsList[0]);
	
    }

    public void SelectItem(Inventory_Item item, int i){
	item.Slot = i;
	HoverFirst(this, new InventoryEventArgs(item));
    }

    public void PlaceItem(){
	Debug.Log("Got here");
	for(int i = 0; i < itemsList.Count; i++){
	    if(itemsList[i].Selected == true){
		Debug.Log(itemsList[i].Name);
		Vector3 playerPos = FindObjectOfType<MovePlayer>().getPlayerPosition();
		Instantiate(spikeTrap, playerPos , transform.rotation);
		if(i == itemsList.Count - 1){
		    itemsList[i-1].Selected = true;
		    break;
		}
		for(; i < itemsList.Count-1; i++){
		    itemsList[i] = itemsList[i+1];
		}
		renderInventory();
	    }
	}
    }

    public void renderInventory(){
	//TODO create a function to refresh the inventory
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
	
    }

    // Update is called once per frame
    void Update()
    {
	
    }
}

