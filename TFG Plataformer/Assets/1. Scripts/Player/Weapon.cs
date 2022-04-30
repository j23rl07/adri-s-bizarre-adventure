using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour {

    [Header("FireBall FirePoints")]

    public Transform fbMidFirePoint;
    public Sprite fbSprite;

    [Header("CuchillaFolio FirePoints")]
    public Transform cfTopFirePoint;
    public Transform cfMidFirePoint;
    public Transform cfBottomFirePoint;
    public Sprite cfSprite;

    [Header("Horizontal Projectile FirePoints")]
    public Transform hpLeftFirePoint;
    public Transform hpCenterFirePoint;
    public Transform hpRightFirePoint;
    public Sprite hpSprite;

    [Header("Descarga FirePoints")]
    public Transform dsLeftBottomFirePoint;
    public Transform dsRightBottomFirePoint;
    public Transform dsLeftTopFirePoint;
    public Transform dsRightTopFirePoint;
    public Sprite dsSprite;

    [Header("GammaRay FirePoints")]
    public Transform bRightFirePoint;
    public Transform bLeftFirePoint;
    public Transform bTopFirePoint;
    public Transform bBottomFirePoint;
    public Sprite grSprite;

    [Header("Boomerang Options")]
    public Sprite bSprite;

    [Header("Sprite Options")]
    public SpriteRenderer rend;

    [Header("Other")]
    public GameObject bulletPrefab;
    public Animator animator;
    public List<GameObject> prefabs;
    public List<int> allowedLayerCollisions;
    private int i = 1;
    public Player player;
    private PauseMenu pauseMenu;
    [HideInInspector] public bool isCasting = false;

    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        bulletPrefab.GetComponent<Bullet>().allowedLayerCollisions = allowedLayerCollisions;
    }

    void Shoot()
    {
        if (!isCasting)
        {
            if (bulletPrefab.name == "Cuchilla folio")
            {
                if (player.useMana(20))
                {
                    isCasting = true;
                    Instantiate(bulletPrefab, cfTopFirePoint.position, cfTopFirePoint.rotation);
                    Instantiate(bulletPrefab, cfMidFirePoint.position, cfMidFirePoint.rotation);
                    Instantiate(bulletPrefab, cfBottomFirePoint.position, cfBottomFirePoint.rotation);
                    
                }
            }

            if (bulletPrefab.name == "Fireball")
            {
                if (player.useMana(3))
                {
                    isCasting = true;
                    Instantiate(bulletPrefab, fbMidFirePoint.position, fbMidFirePoint.rotation);
                    
                }
            }

            if (bulletPrefab.name == "HorizontalProjectile")
            {
                if (player.useMana(10))
                {
                    isCasting = true;
                    Instantiate(bulletPrefab, hpLeftFirePoint.position, hpLeftFirePoint.rotation);
                    Instantiate(bulletPrefab, hpCenterFirePoint.position, hpCenterFirePoint.rotation);
                    Instantiate(bulletPrefab, hpRightFirePoint.position, hpRightFirePoint.rotation);
                    
                }
            }

            if (bulletPrefab.name == "Descarga")
            {
                if (player.useMana(15))
                {
                    isCasting = true;
                    Instantiate(bulletPrefab, dsLeftBottomFirePoint.position, dsLeftBottomFirePoint.rotation);
                    Instantiate(bulletPrefab, dsRightBottomFirePoint.position, dsRightBottomFirePoint.rotation);
                    Instantiate(bulletPrefab, dsLeftTopFirePoint.position, dsLeftTopFirePoint.rotation);
                    Instantiate(bulletPrefab, dsRightTopFirePoint.position, dsRightTopFirePoint.rotation);
                    
                }
            }

            if (bulletPrefab.name == "GammaRay")
            {
                if (player.useMana(20))
                {
                    isCasting = true;
                    Instantiate(bulletPrefab, bRightFirePoint.position, bRightFirePoint.rotation);
                    Instantiate(bulletPrefab, bLeftFirePoint.position, bLeftFirePoint.rotation);
                    Instantiate(bulletPrefab, bTopFirePoint.position, bTopFirePoint.rotation);
                    Instantiate(bulletPrefab, bBottomFirePoint.position, bBottomFirePoint.rotation);
                    
                }

            }

            if (bulletPrefab.name == "Boomerang")
            {
                if (player.useMana(10))
                {
                    isCasting = true;
                    Instantiate(bulletPrefab, fbMidFirePoint.position, fbMidFirePoint.rotation);
                    
                }
            }
        }
    }

    void Update()
    {
        if (!pauseMenu.IsGamePaused()) { 
            if (Input.GetMouseButtonDown(1))
            {
                Shoot();
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                changeSkill();
            }
        }
    }

    void changeSkill()
    {
        bulletPrefab = prefabs[i++];
        

        if (i == prefabs.Count)
        {
            i = 0;
        }

        switch(bulletPrefab.name)
        {
            case "Fireball":
                rend.sprite = fbSprite;
                break;
            case "Cuchilla folio":
                rend.sprite = cfSprite;
                break;
            case "HorizontalProjectile":
                rend.sprite = hpSprite;
                break;
            case "Descarga":
                rend.sprite = dsSprite;
                break;
            case "GammaRay":
                rend.sprite = grSprite;
                break;
            case "Boomerang":
                rend.sprite = bSprite;
                break;
        }

        switch (bulletPrefab.name)
        {
            case "GammaRay":
                bulletPrefab.GetComponent<RayScript>().allowedLayerCollisions = allowedLayerCollisions;
                break;
            case "Cuchilla folio":
                bulletPrefab.GetComponent<CuchillaFolioScript>().allowedLayerCollisions = allowedLayerCollisions;
                break;
            case "Boomerang":
                bulletPrefab.GetComponent<BoomerangScript>().allowedLayerCollisions = allowedLayerCollisions;
                break;
            default:
                bulletPrefab.GetComponent<Bullet>().allowedLayerCollisions = allowedLayerCollisions;
                break;
        }
    }
}
