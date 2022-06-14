using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;

    public static Inventory instance;

    public GameObject firstSkill;

    public GameObject[] Aslots;
    public GameObject[] Tslots;
    public GameObject[] backPack;
    private bool isAInstanciated;
    private bool isTInstanciated;

    public Dictionary<string, int> inventoryItems = new Dictionary<string, int>();

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
            if (Weapon.prefabs.Count==0)
            {
                Weapon.prefabs.Add(firstSkill);
            }
            
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        inventory.GetComponent<Canvas>().enabled = false;
    }

    void Update()
    {
        if (!PauseMenu.isPauseMenuOn)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                OpenInv();
            }
        }
    }

    public void OpenInv()
    {
        if (!inventory.GetComponent<Canvas>().enabled & !PauseMenu.otherMenuOn)
        {
            Time.timeScale = 0;
            inventory.GetComponent<Canvas>().enabled=true;
            PauseMenu.isGamePaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PauseMenu.otherMenuOn = true;
        }
        else if (inventory.GetComponent<Canvas>().enabled)
        {
            Time.timeScale = 1;
            inventory.GetComponent<Canvas>().enabled = false;
            PauseMenu.isGamePaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PauseMenu.otherMenuOn = false;
        }
    }

    public void CheckASlotsAvailability(GameObject itemToAdd, string itemName, int itemAmount)
    {
        isAInstanciated = false;
        for(int i = 0; i< Aslots.Length; i++)
        {
            if (Aslots[i].transform.childCount>0)
            {
                Aslots[i].GetComponent<ASlotsScript>().isUsed = true;
            }
            else if (!isAInstanciated & !Aslots[i].GetComponent<ASlotsScript>().isUsed)
            {
                //crear el item en el slot vacío
                if (!inventoryItems.ContainsKey(itemName))
                {
                    GameObject item = Instantiate(itemToAdd, Aslots[i].transform.position, Quaternion.identity);
                    item.transform.SetParent(Aslots[i].transform, false);
                    item.transform.localPosition = new Vector3(0, 0, 0);
                    item.name = item.name.Replace("(Clone)", "");
                    isAInstanciated = true;
                    Aslots[i].GetComponent<ASlotsScript>().isUsed = true;
                    inventoryItems.Add(itemName, itemAmount);
                    break;
                }
            }
        }
    }

    public void CheckTSlotsAvailability(GameObject itemToAdd, string itemName, int itemAmount)
    {
        isTInstanciated = false;
        for (int i = 0; i < Tslots.Length; i++)
        {
            if (Tslots[i].transform.childCount > 0)
            {
                Tslots[i].GetComponent<TSlotsScript>().isUsed = true;
            }
            else if (!isTInstanciated & !Tslots[i].GetComponent<TSlotsScript>().isUsed)
            {
                //crear el item en el slot vacío
                if (!inventoryItems.ContainsKey(itemName))
                {
                    GameObject item = Instantiate(itemToAdd, Tslots[i].transform.position, Quaternion.identity);
                    item.transform.SetParent(Tslots[i].transform, false);
                    item.transform.localPosition = new Vector3(0, 0, 0);
                    item.name = item.name.Replace("(Clone)", "");
                    isTInstanciated = true;
                    Tslots[i].GetComponent<TSlotsScript>().isUsed = true;
                    inventoryItems.Add(itemName, itemAmount);
                    break;
                }
            }
        }
    }

    public void CheckTrincketEInventory(ItemType type)
    {
        for(int i = 0; i < Tslots.Length; i++)
        {
            if (Tslots[i].GetComponent<TSlotsScript>().isUsed)
            {
                if(Tslots[i].transform.GetComponentInChildren<Items>().itemType == type)
                {
                    if (Tslots[i].transform.GetChild(0).GetChild(0).gameObject.activeSelf)
                    {
                        Tslots[i].transform.GetComponentInChildren<InventoryButtons>().deactivateT();
                    }
                }
            }
        }
    }

    public void CheckAEInventory(ItemType type)
    {
        for (int i = 0; i < Aslots.Length; i++)
        {
            if (Aslots[i].GetComponent<ASlotsScript>().isUsed)
            {
                if (Aslots[i].transform.GetComponentInChildren<ItemsUse>().itemType == type)
                {
                    if (Aslots[i].transform.GetChild(0).GetChild(0).gameObject.activeSelf)
                    {
                        Aslots[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    /*public void useInventoriItems(string itemName)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.GetChild(0).gameObject.name == itemName)
            {
                Destroy(slots[i].transform.GetChild(0).gameObject);
                slots[i].GetComponent<TSlotsScript>().isUsed = false;
                inventoryItems.Remove(itemName);
                ReorganizeInventory();
                break;
            }
        }
    }*/

    /*private void ReorganizeAInventory()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].GetComponent<ASlotsScript>().isUsed)
            {
                for(int j = i+1; j < slots.Length; j++)
                {
                    if (slots[j].GetComponent<ASlotsScript>().isUsed)
                    {
                        Transform itemToMove = slots[j].transform.GetChild(0).transform;
                        itemToMove.transform.SetParent(slots[i].transform, false);
                        itemToMove.transform.localPosition = new Vector3(0, 0, 0);
                        slots[i].GetComponent<ASlotsScript>().isUsed = true;
                        slots[j].GetComponent<ASlotsScript>().isUsed = false;
                        break;
                    }
                }
            }
        }
    }*/
}
