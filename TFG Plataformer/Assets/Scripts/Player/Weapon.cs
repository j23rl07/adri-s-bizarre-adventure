using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [Header("FireBall FirePoints")]

    public Transform fbMidFirePoint;

    [Header("CuchillaFolio FirePoints")]
    public Transform cfTopFirePoint;
    public Transform cfMidFirePoint;
    public Transform cfBottomFirePoint;

    [Header("Horizontal Projectile FirePoints")]
    public Transform hpLeftFirePoint;
    public Transform hpCenterFirePoint;
    public Transform hpRightFirePoint;

    [Header("Descarga FirePoints")]
    public Transform dsLeftBottomFirePoint;
    public Transform dsRightBottomFirePoint;
    public Transform dsLeftTopFirePoint;
    public Transform dsRightTopFirePoint;

    [Header("GammaRay FirePoints")]
    public Transform bRightFirePoint;
    public Transform bLeftFirePoint;
    public Transform bTopFirePoint;
    public Transform bBottomFirePoint;

    [Header("Other")]
    public GameObject bulletPrefab;
    public Animator animator;
    public GameObject[] prefabs;
    private int i = 1;
    public Player player;
    private PauseMenu pauseMenu;
    [HideInInspector] public bool isCasting = false;

    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void Shoot()
    {
        isCasting = true;
        animator.SetTrigger("cast");
        if (bulletPrefab.name == "Cuchilla folio")
        {
            player.useMana(20);
            Instantiate(bulletPrefab, cfTopFirePoint.position, cfTopFirePoint.rotation);
            Instantiate(bulletPrefab, cfMidFirePoint.position, cfMidFirePoint.rotation);
            Instantiate(bulletPrefab, cfBottomFirePoint.position, cfBottomFirePoint.rotation);
        }

        if (bulletPrefab.name == "Fireball")
        {
            player.useMana(3);
            Instantiate(bulletPrefab, fbMidFirePoint.position, fbMidFirePoint.rotation);
        }

        if (bulletPrefab.name == "HorizontalProjectile")
        {
            player.useMana(10);
            Instantiate(bulletPrefab, hpLeftFirePoint.position, hpLeftFirePoint.rotation);
            Instantiate(bulletPrefab, hpCenterFirePoint.position, hpCenterFirePoint.rotation);
            Instantiate(bulletPrefab, hpRightFirePoint.position, hpRightFirePoint.rotation);
        }

        if (bulletPrefab.name == "Descarga")
        {
            player.useMana(15);
            Instantiate(bulletPrefab, dsLeftBottomFirePoint.position, dsLeftBottomFirePoint.rotation);
            Instantiate(bulletPrefab, dsRightBottomFirePoint.position, dsRightBottomFirePoint.rotation);
            Instantiate(bulletPrefab, dsLeftTopFirePoint.position, dsLeftTopFirePoint.rotation);
            Instantiate(bulletPrefab, dsRightTopFirePoint.position, dsRightTopFirePoint.rotation);
        }

        if (bulletPrefab.name == "GammaRay")
        {
            player.useMana(20);
            Instantiate(bulletPrefab, bRightFirePoint.position, bRightFirePoint.rotation);
            Instantiate(bulletPrefab, bLeftFirePoint.position, bLeftFirePoint.rotation);
            Instantiate(bulletPrefab, bTopFirePoint.position, bTopFirePoint.rotation);
            Instantiate(bulletPrefab, bBottomFirePoint.position, bBottomFirePoint.rotation);

        }
    }

    void Update()
    {
        if (!pauseMenu.IsGamePaused()) { 
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
            if (Input.GetKeyDown("v"))
            {
                changeSkill();
            }
        }
    }

    void changeSkill()
    {
        bulletPrefab = prefabs[i++];
        if (i == prefabs.Length)
        {
            i = 0;
        }
    }
}
