using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour, Inventory_Item
{

    public string Name{
	get{
	    return "SpikeTrap";
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
}
