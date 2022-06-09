using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ckeckPlayerInside : MonoBehaviour
{
    public bool isInside;

    private void Start()
    {
        isInside = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInside = false;
        }
    }
}
