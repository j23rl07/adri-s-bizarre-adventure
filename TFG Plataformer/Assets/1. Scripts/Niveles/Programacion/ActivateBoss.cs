using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoss : MonoBehaviour
{
    private bool activo = false;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        activo = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        activo = false;
    }

    private void Update()
    {
        if (activo && Input.GetKeyDown(KeyCode.A))
        {
            activate();
        }
    }

    private void activate()
    {
        gameObject.GetComponentInParent<Animator>().enabled = true;
        gameObject.SetActive(false);
    }

}
