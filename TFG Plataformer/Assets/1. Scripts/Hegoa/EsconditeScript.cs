using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsconditeScript : MonoBehaviour
{
    private BoxCollider2D escondite;
    private Vector2[] vertices = new Vector2[4];
    [SerializeField] private BoxCollider2D player;

    void Start()
    {
        escondite = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        Debug.Log(VisionRangeScrpit.isActive);
        if (IsPlayerHidden())
        {
            VisionRangeScrpit.isActive = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.Equals(player))
        {
            VisionRangeScrpit.isActive = true;
        }
    }

    private bool IsPlayerHidden()
    {
        bool result = true;
        
        vertices[0] = player.bounds.max;
        vertices[1] = new Vector2(player.bounds.max.x, player.bounds.min.y);
        vertices[2] = player.bounds.min;
        vertices[3] = new Vector2(player.bounds.min.x, player.bounds.max.y);
        
        foreach(Vector2 vertice in vertices)
        {
            if (!escondite.bounds.Contains(vertice))
            {
                result = false;
                break;
            }
        }
        return result;
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach(Vector2 vertice in vertices)
        {
            Gizmos.DrawSphere(vertice, .05f);
        }
    }
}
