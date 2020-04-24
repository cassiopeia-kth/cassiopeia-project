using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHUD : MonoBehaviour
{

    public Inventory inventory;
    
    // Start is called before the first frame update
    void Start()
    {
        inventory.ItemAdded += InventoryScript_ItemAdded;	
        inventory.HoverFirst += InventoryScript_HoverFirst;

	Transform inventoryPanel = transform.Find("InventoryPanel");
	foreach(Transform slot in inventoryPanel){
	    Image image = slot.GetChild(0).GetComponent<Image>();
	    image.color = new Color(image.color.r, image.color.g, image.color.b, 0.3f);
	}
	

    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e){
	Transform inventoryPanel = transform.Find("InventoryPanel");
	foreach(Transform slot in inventoryPanel){
	    Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
//	    image.sprite = e.Item.Image;
	    if(image.enabled == false){
		image.enabled = true;
		image.sprite = e.Item.Image;

		break;
	    }
	}
    }


    private void InventoryScript_HoverFirst(object sender, InventoryEventArgs e){
	Transform inventoryPanel = transform.Find("InventoryPanel");
	foreach(Transform slot in inventoryPanel){
	    Image imageHolder = slot.GetChild(0).GetComponent<Image>();
	    imageHolder.color = new Color(imageHolder.color.r, imageHolder.color.g, imageHolder.color.b, 0.3f);
	}
	Debug.Log("RIGHT BEFORE IT BECOMING TRUE");
	Debug.Log(e.Item.Slot);
	Image image = inventoryPanel.GetChild(e.Item.Slot).GetChild(0).GetComponent<Image>();
	image.color = new Color(image.color.r, image.color.g, image.color.b, 0.7f);
	e.Item.Selected = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
