using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
