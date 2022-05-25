using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeDoor : MonoBehaviour
{
    public GameObject puerta;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            puerta.active = true;
        }
    }

}
