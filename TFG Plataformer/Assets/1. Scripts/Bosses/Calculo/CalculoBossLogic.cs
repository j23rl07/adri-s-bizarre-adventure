using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculoBossLogic : MonoBehaviour
{
    public bool st1IsDestroyed;
    public bool st2IsDestroyed;
    public bool st3IsDestroyed;
    public bool st4IsDestroyed;
    public CircleCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        st1IsDestroyed = false;
        st2IsDestroyed = false;
        st3IsDestroyed = false;
        st4IsDestroyed = false;
        collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(st1IsDestroyed && st2IsDestroyed && st3IsDestroyed && st4IsDestroyed)
        {
            collider.enabled = !collider.enabled;
        }
    }
}
