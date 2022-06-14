using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minions : MonoBehaviour
{
    public bool respawn = false;

    public GameObject trigger;

    // Update is called once per frame
    void Update()
    {
        if (respawn)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Collider2D>().enabled=true;
                for (int j = 0; j < transform.GetChild(i).childCount; j++)
                {
                    if (transform.GetChild(i).GetChild(j).gameObject.activeSelf)
                    {
                        transform.GetChild(i).GetChild(j).GetComponent<MinionHealth>().Die();
                    }
                    
                }
            }
            respawn = false;
        }
    }
}
