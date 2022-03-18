using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator animator;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        animator.SetTrigger("cast");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
