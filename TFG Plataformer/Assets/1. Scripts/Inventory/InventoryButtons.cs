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

    public void UseItem()
    {
        if(GetComponent<ItemsUse>().itemType == ItemType.SKILL)
        {
            if (transform.GetChild(0).gameObject.activeSelf)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        else
        {
            inventory.useInventoriItems(gameObject.name);
        }

        
    }
}
