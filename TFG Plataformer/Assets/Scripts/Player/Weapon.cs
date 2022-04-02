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

    [Header("Other")]
    public GameObject bulletPrefab;
    public Animator animator;
    public GameObject[] prefabs;
    private int i = 1;
    public Player player;


    void Shoot()
    {
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
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (Input.GetKeyDown("v"))
        {
            changeSkill();
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
