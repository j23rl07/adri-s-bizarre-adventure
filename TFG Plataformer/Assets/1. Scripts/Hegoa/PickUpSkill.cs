using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSkill : MonoBehaviour
{
    public GameObject itemToAdd;
    public int amountToAdd;
    Inventory inventory;
    [SerializeField] private Weapon weaponScript;
    [SerializeField] private GameObject skillPrefab;

    private void Start()
    {
        inventory = Inventory.instance.GetComponent<Inventory>();
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Player"))
        {
            inventory.CheckASlotsAvailability(itemToAdd, itemToAdd.name, amountToAdd);
            Weapon.prefabs.Add(skillPrefab);
            Destroy(gameObject);
        }
    }
}
