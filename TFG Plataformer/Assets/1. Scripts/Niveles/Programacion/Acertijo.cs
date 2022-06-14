using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;

public class Acertijo : MonoBehaviour
{

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject mensajePuerta;


    private void Update()
    {
        if (comprueba())
        {
            resuelto();
        }
    }

    private bool comprueba()
    {
        bool result = true;
        for(int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).GetComponent<ChangeTile>().completado)
            {
                result = false;
                break;
            }
        }
        return result;
    }

    private void resuelto()
    {
        tilemap.ClearAllTiles();
        mensajePuerta.SetActive(false);
    }
}
