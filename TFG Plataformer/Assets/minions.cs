using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minions : MonoBehaviour
{
    public bool paraR = false;

    public GameObject trigger;

    // Update is called once per frame
    void Update()
    {
        if (paraR && trigger.GetComponent<bossZoneR>().respawn)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Collider2D>().enabled=true;
            }
        }
    }
}
