using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrinketPickUpScript : MonoBehaviour
{
    public GameObject trinket;
    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance.GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inventory.CheckTSlotsAvailability(trinket, trinket.name, 1);
            GameObject.Destroy(this.gameObject);
        }
    }
}
