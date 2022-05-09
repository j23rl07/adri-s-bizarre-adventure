using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeTile : MonoBehaviour
{
    [SerializeField] private TileBase Tile11;
    [SerializeField] private TileBase Tile12;
    [SerializeField] private TileBase Tile21;
    [SerializeField] private TileBase Tile22;
    [SerializeField] private TileBase Tile31;
    [SerializeField] private TileBase Tile32;

    [SerializeField] private Vector3Int position;

    [SerializeField] private Tilemap tilemap;

    private bool activo = false;

    public bool completado = false;



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
        if(activo && Input.GetKeyDown(KeyCode.E))
        {
            changeTiles();
        }
    }

    private void changeTiles()
    {
        Vector3Int positionaux = new Vector3Int();
        positionaux = position;

        tilemap.SetTile(positionaux, Tile11);

        positionaux = position;
        positionaux.Set(position.x + 1, position.y, 0);
        tilemap.SetTile(positionaux, Tile12);

        positionaux = position;
        positionaux.Set(position.x, position.y-1, 0);
        tilemap.SetTile(positionaux, Tile21);

        positionaux = position;
        positionaux.Set(position.x + 1, position.y-1, 0);
        tilemap.SetTile(positionaux, Tile22);

        positionaux = position;
        positionaux.Set(position.x, position.y-2, 0);
        tilemap.SetTile(positionaux, Tile31);

        positionaux = position;
        positionaux.Set(position.x + 1, position.y-2, 0);
        tilemap.SetTile(positionaux, Tile32);

        completado = true;
    }
}
