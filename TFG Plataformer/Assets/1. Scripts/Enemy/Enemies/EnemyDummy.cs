using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummy : MonoBehaviour
{
    private static EnemyHealth enemyHealthScript;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealthScript = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealthScript.gotHit)
        {
            enemyHealthScript.currentHealth = enemyHealthScript.maxHealth;
            enemyHealthScript.gotHit = false;
        }
    }
}
