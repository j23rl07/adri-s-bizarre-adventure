using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtons : MonoBehaviour
{

    Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance.GetComponent<Inventory>();
    }

    public void UseItemT()
    {
        
        if (transform.GetChild(0).gameObject.activeSelf)
        {
            deactivateT();
        }
        else
        {
            activateT();
        }      
    }

    public void deactivateT()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }
    public void activateT()
    {
        inventory.CheckTrincketEInventory(GetComponent<ItemsUse>().itemType);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).transform.position = new Vector2(transform.parent.transform.parent.position.x + 777, transform.parent.transform.parent.position.y);
    }

    public void UseItemH()
    {

        if (transform.GetChild(0).gameObject.activeSelf)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            inventory.CheckAEInventory(GetComponent<ItemsUse>().itemType);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).transform.position = new Vector2(transform.parent.transform.parent.position.x + 774, transform.parent.transform.parent.position.y);
        }
    }
}
