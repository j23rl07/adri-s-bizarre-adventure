using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator animator;

    [HideInInspector] public bool isCasting = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) & !isCasting)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        isCasting = true;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
