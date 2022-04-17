using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrap : MonoBehaviour
{
    [SerializeField] private float attackCd;
    [SerializeField] private Transform firePointRef;
    [SerializeField] private GameObject[] projectiles;
    private float timerCd;

    private void Attack()
    {
        timerCd = 0;

        projectiles[FindFireball()].transform.position = firePointRef.position;
        projectiles[FindFireball()].GetComponent<EnemyProjectile>().triggerProjectile();
    }
    private int FindFireball()
    {
        for(int i=0; i < projectiles.Length; i++)
        {
            if (!projectiles[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void Update()
    {
        timerCd += Time.deltaTime;

        if(timerCd >= attackCd)
        {
            Attack();
        } 
    }
}
