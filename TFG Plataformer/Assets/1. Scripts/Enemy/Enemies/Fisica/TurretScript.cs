using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] private float delay;
    private float delayTimer;
    [SerializeField] private float atkSpeed;
    private float atkSpeedTimer;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    void Start()
    {
        delayTimer = Time.time + delay;
        atkSpeedTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }

    private void shoot()
    {
        if(delayTimer-Time.time <= 0 && atkSpeedTimer-Time.time <= 0)
        {
            gameObject.GetComponent<Animator>().SetTrigger("shoot");
            GameObject shot = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Destroy(shot, 9);
            atkSpeedTimer = Time.time + atkSpeed;
        }
    }
}
