using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    private PauseMenu pauseMenu;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator animator;

    [HideInInspector] public bool isCasting = false;

    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void Update()
    {
        if (!pauseMenu.IsGamePaused())
        {
            if (IInput.GetMouseButtonDown(1) & !isCasting)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        isCasting = true;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
