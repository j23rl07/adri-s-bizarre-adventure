using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisicaBoss : EnemyHealth
{
    [HideInInspector] public bool active;
    [SerializeField] private GameObject player;
    private bool gettingReady;
    [SerializeField] private List<Transform> tpLocations;
    public HealthBar healthBar;
    [SerializeField] private float delay;
    private float delayTimer;
    [SerializeField] private float atkSpeed;
    private float atkSpeedTimer;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    private Quaternion aim;
    

    private bool recovering;

    // Start is called before the first frame update
    void Start()
    {
        active = false;

        gettingReady = false;
        healthBar.SetMaxHealth(maxHealth);
        delayTimer = Time.time + delay;
        atkSpeedTimer = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            Fight();
        }
    }
    
    void Fight()
    {
        if (!gotHit)
        {
            Shoot();
        }
        else if (!recovering)
        {
            StartCoroutine(GetHurt());
        }
    }

    void Teleport()
    {
        int chosenTP = Random.Range(0, tpLocations.Count);
        transform.position = tpLocations[chosenTP].position;
        transform.rotation = tpLocations[chosenTP].rotation;
    }
    void Shoot()
    {
        if (delayTimer-Time.time<= 0 && atkSpeedTimer - Time.time <= 0)
        {
            Vector2 directionToPlayer = player.transform.position - firePoint.transform.position;
            float angle = Vector3.Angle(Vector3.right, directionToPlayer);
            if (player.transform.position.y < transform.position.y) angle *= -1;
            aim = Quaternion.AngleAxis(angle, Vector3.forward);
            animator.SetTrigger("Shoot");
            GameObject shot = Instantiate(bulletPrefab, firePoint.position, aim);
            Destroy(shot, 5);
            atkSpeedTimer = Time.time + atkSpeed;
            
        }
    }
    IEnumerator GetHurt()
    {
        recovering = true;
        healthBar.SetHealth(currentHealth);
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(.35f);
        Teleport();
        GetComponent<Collider2D>().enabled = true;
        gotHit = false;
        recovering = false;
        delayTimer = Time.time + delay;

        if (currentHealth <= maxHealth / 2)
            atkSpeed = .5f;
    }
}
