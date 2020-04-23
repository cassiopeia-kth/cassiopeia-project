using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 7;

    private List<Inventory_Item> itemsList = new List<Inventory_Item>();

    public event EventHandler<InventoryEventArgs> ItemAdded;


    public void AddItem(Inventory_Item item){

	if(itemsList.Count < SLOTS){
	    Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
	    if(collider.enabled){
		collider.enabled = false;

		itemsList.Add(item);

		item.OnPickup();

		if(ItemAdded != null){
		    ItemAdded(this, new InventoryEventArgs(item));
		}
	     }
	}
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
