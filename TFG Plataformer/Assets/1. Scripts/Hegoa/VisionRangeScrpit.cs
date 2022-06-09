using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionRangeScrpit : MonoBehaviour
{
    public Player player;
    public static bool isActive;
    [SerializeField] protected private int damage;

    private void Start()
    {
        isActive = true;
    }
    protected private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isActive)
        {
            player.TakeDamage(damage);
        }
    }
}
