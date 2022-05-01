using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Weapon weapon;
    public GameObject boomerangPrefab;

    private void OnTriggerEnter2D(Collider2D item)
    {
        if(item.tag == "Boomerang")
        {
            weapon.prefabs.Add(boomerangPrefab);
            Destroy(item.gameObject);
        }
        
    }
}
