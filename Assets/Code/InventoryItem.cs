using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public interface Inventory_Item {
    string Name { get; }

    Sprite Image { get; }

    bool Selected { get; set;}

    int Slot { get; set;}

    void OnPickup();
}

public class InventoryEventArgs : EventArgs {
    public InventoryEventArgs(Inventory_Item item){
	Item = item;
    }
    
    public Inventory_Item Item;
}
