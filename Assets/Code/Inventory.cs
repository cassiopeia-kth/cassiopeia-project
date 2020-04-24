﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 7;

    public List<Inventory_Item> itemsList = new List<Inventory_Item>();

    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> HoverFirst;

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
    
    
    // Start is called before the first frame update
    void Start()
    {
	
    }

    // Update is called once per frame
    void Update()
    {
	
    }
}

