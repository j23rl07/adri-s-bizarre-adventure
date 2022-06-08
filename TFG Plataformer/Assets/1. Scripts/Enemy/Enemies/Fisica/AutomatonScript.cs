using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatonScript : MonoBehaviour
{
    private EnemyHealth enemyHealthScript;
    private Vector3 position;
    private bool locked = false;

    private void Start()
    {
        enemyHealthScript = GetComponent<EnemyHealth>();
    }
    void Update()
    {
        if (enemyHealthScript.gotHit && !locked)
        {
            locked = true;
            StartCoroutine(Recovery());
        }
        if (enemyHealthScript.gotHit)
        {
            this.gameObject.transform.position = position;
        }
    }

    private IEnumerator Recovery()
    {
        enemyHealthScript.animator.SetBool("hurt", true);
        position = this.gameObject.transform.position;
        yield return new WaitForSeconds(.5f);
        enemyHealthScript.gotHit = false;
        enemyHealthScript.animator.SetBool("hurt", false);
        locked = false;
    }
}
