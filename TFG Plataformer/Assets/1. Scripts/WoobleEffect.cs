using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WoobleEffect : MonoBehaviour
{
    float startingY;

    public float woobleForce = 1; 
                                    

    void Start()
    {
        this.startingY = this.transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, startingY + ((float)System.Math.Sin(Time.time) * woobleForce), transform.position.z);
    }
}
