using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSprite : MonoBehaviour
{
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
