using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialZone : MonoBehaviour
{
    public GameObject canvas;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canvas.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canvas.SetActive(true);
        }
    }
}
