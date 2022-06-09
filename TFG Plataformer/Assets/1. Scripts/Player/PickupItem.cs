using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Weapon weapon;
    public GameObject boomerangPrefab;
    public GameObject cfPrefab;

    private void OnTriggerEnter2D(Collider2D item)
    {
        if(item.tag == "Boomerang")
        {
            Weapon.prefabs.Add(boomerangPrefab);
            Destroy(item.gameObject);

        }else if (item.tag == "CuchillaFolio"){
            Weapon.prefabs.Add(cfPrefab);
            Destroy(item.gameObject);
        }
        
    }
}
