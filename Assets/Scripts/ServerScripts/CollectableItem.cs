﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour, Inventory_Item
{
    public bool selected = false;
    public string Name{
	get{
	    return this.name;
	}
	set{
	    Name = value;
	}
    }

    public bool Selected{
	get{
	    return selected;
	}
	set{
	    selected = value;
	}
    }

    public int slot;
    public int Slot{
	get{
	    return slot;
	}
	set{
	    slot = value;
	}
    }
    
    public Sprite _Image = null;

    public Sprite Image{
	get{
	    return _Image;
	}
    }

    public void OnPickup(){
	gameObject.SetActive(false);
    }
    


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collectable")
        {
            Debug.Log("Two collectables on same spot. Kill them both!");
            gameObject.SetActive(false);
        }
    }
}
