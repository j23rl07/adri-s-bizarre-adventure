using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    private PauseMenu pauseMenu;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator animator;

    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void Update()
    {
        if (!pauseMenu.IsGamePaused())
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        animator.SetTrigger("cast");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
