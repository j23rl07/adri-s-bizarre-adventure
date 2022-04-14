using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;

    public static Inventory instance;

    public GameObject[] slots;
    public GameObject[] backPack;
    private bool isInstanciated;

    public Dictionary<string, int> inventoryItems = new Dictionary<string, int>();

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        OpenInv();
    }

    public void OpenInv()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isPaused)
        {
            Time.timeScale = 0;
            inventory.SetActive(true);
            isPaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && isPaused)
        {
            Time.timeScale = 1;
            inventory.SetActive(false);
            isPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void CheckSlotsAvailability(GameObject itemToAdd, string itemName, int itemAmount)
    {
        isInstanciated = false;
        for(int i = 0; i<slots.Length; i++)
        {
            if (slots[i].transform.childCount>0)
            {
                slots[i].GetComponent<SlotsScript>().isUsed = true;
            }
            else if (!isInstanciated & !slots[i].GetComponent<SlotsScript>().isUsed)
            {
                //crear el item en el slot vacío
                if (!inventoryItems.ContainsKey(itemName))
                {
                    GameObject item = Instantiate(itemToAdd, slots[i].transform.position, Quaternion.identity);
                    item.transform.SetParent(slots[i].transform, false);
                    item.transform.localPosition = new Vector3(0, 0, 0);
                    item.name = item.name.Replace("(Clone)", "");
                    isInstanciated = true;
                    slots[i].GetComponent<SlotsScript>().isUsed = true;
                    inventoryItems.Add(itemName, itemAmount);
                    break;
                }
            }
        }
    }

    public void useInventoriItems(string itemName)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.GetChild(0).gameObject.name == itemName)
            {
                Destroy(slots[i].transform.GetChild(0).gameObject);
                slots[i].GetComponent<SlotsScript>().isUsed = false;
                inventoryItems.Remove(itemName);
                ReorganizeInventory();
                break;
            }
        }
    }

    private void ReorganizeInventory()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].GetComponent<SlotsScript>().isUsed)
            {
                for(int j = i+1; j < slots.Length; j++)
                {
                    if (slots[j].GetComponent<SlotsScript>().isUsed)
                    {
                        Transform itemToMove = slots[j].transform.GetChild(0).transform;
                        itemToMove.transform.SetParent(slots[i].transform, false);
                        itemToMove.transform.localPosition = new Vector3(0, 0, 0);
                        slots[i].GetComponent<SlotsScript>().isUsed = true;
                        slots[j].GetComponent<SlotsScript>().isUsed = false;
                        break;
                    }
                }
            }
        }
    }
}
