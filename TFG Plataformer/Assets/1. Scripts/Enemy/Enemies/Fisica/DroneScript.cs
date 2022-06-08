using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float atkSpeed;
    [SerializeField] private Quaternion aim;
    private float atkSpeedTimer;
    private bool active;

    private EnemyHealth enemyHealthScript;
    private Vector3 position;
    private bool locked = false;

    void Start()
    {
        enemyHealthScript = GetComponentInParent<EnemyHealth>();
        atkSpeedTimer = 0;
    }
    void Update()
    {
        shoot();

        if (enemyHealthScript.gotHit && !locked)
        {
            locked = true;
            StartCoroutine(Recovery());
        }
        if (enemyHealthScript.gotHit)
        {
            transform.parent.position = position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.Equals(player.GetComponent<Collider2D>()))
        {
            active = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.Equals(player.GetComponent<Collider2D>()))
        {
            active = false;
        }
    }


    private void shoot()
    {
        if (atkSpeedTimer - Time.time <= 0)
        {
            atkSpeedTimer = Time.time + atkSpeed;
            if (active)
            {
                Vector2 directionToPlayer = player.transform.position - firePoint.transform.position;
                float angle = Vector3.Angle(Vector3.right, directionToPlayer);
                if (player.transform.position.y < transform.position.y) angle *= -1;
                aim = Quaternion.AngleAxis(angle, Vector3.forward);

                GameObject shot = Instantiate(bulletPrefab, firePoint.position, aim);
                Destroy(shot, 5);
            }
        }
    }
    private IEnumerator Recovery()
    {
        position = transform.parent.transform.position;
        yield return new WaitForSeconds(.5f);
        enemyHealthScript.gotHit = false;
        locked = false;
    }
}
