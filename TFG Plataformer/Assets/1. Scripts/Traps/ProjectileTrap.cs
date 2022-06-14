using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrap : MonoBehaviour
{
    [SerializeField] private float attackCd;
    [SerializeField] private Transform firePointRef;
    [SerializeField] private GameObject[] projectiles;
    private float timerCd;

    /*En esta primera función determinamos que tipo de proyectiles activar dependiendo del
    tipo de trampa con el que estemos tratando*/
    private void Attack()
    {
        timerCd = 0;

        projectiles[FindFireball()].transform.position = firePointRef.position;
        if (projectiles[FindFireball()].GetComponent<SinEnemyProjectile>() != null)
        {
            projectiles[FindFireball()].GetComponent<SinEnemyProjectile>().triggerProjectile();
        } else if (projectiles[FindFireball()].GetComponent<EnemyProjectile>() != null)
        {
            projectiles[FindFireball()].GetComponent<EnemyProjectile>().triggerProjectile();
        }
        else
        {
            projectiles[FindFireball()].GetComponent<ArcEnemyProjectile>().triggerProjectile();
        }
        

    }

    //Rotar entre proyectiles en la jerarquía
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
