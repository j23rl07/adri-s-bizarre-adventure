using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationZone : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private GameObject activator;


    private void Start()
    {
        foreach (GameObject target in targets)
        {
            target.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == (activator))
        {
            foreach (GameObject target in targets)
            {
                target.SetActive(true);
                Destroy(target, 20);
            }
        }
    }
}
