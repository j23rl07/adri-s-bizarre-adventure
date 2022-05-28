using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ActivateBoss : MonoBehaviour
{
    private bool activo = false;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Tilemap tilemap;

    [SerializeField] private TileBase Tile11;
    [SerializeField] private TileBase Tile12;
    [SerializeField] private TileBase Tile21;
    [SerializeField] private TileBase Tile22;

    [SerializeField] private Vector3Int position;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            activo = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            activo = false;
        }
    }

    private void Update()
    {
        if (activo)
        {
            cierra();
            activate();
        }
    }

    private void activate()
    {
        boxCollider.enabled = true;
        transform.gameObject.GetComponentInParent<Animator>().enabled = true;
    }

    private void bloque(Vector3Int vector)
    {
        Vector3Int positionaux = new Vector3Int();
        positionaux = vector;

        tilemap.SetTile(positionaux, Tile11);

        positionaux = vector;
        positionaux.Set(vector.x + 1, vector.y, 0);
        tilemap.SetTile(positionaux, Tile12);

        positionaux = vector;
        positionaux.Set(vector.x, vector.y - 1, 0);
        tilemap.SetTile(positionaux, Tile21);

        positionaux = vector;
        positionaux.Set(vector.x + 1, vector.y - 1, 0);
        tilemap.SetTile(positionaux, Tile22);
    }

    private void cierra()
    {
        Vector3Int positionaux = new Vector3Int();
        bloque(position);

        positionaux = position;
        positionaux.Set(position.x, position.y - 2, 0);
        bloque(positionaux);

        positionaux = position;
        positionaux.Set(position.x, position.y - 4, 0);
        bloque(positionaux);

        positionaux = position;
        positionaux.Set(position.x, position.y - 6, 0);
        bloque(positionaux);
    }

}
