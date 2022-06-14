using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireballHolder : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    //Arreglar direcci�n de disparo determinando la orientaci�n del enemigo
    private void Update()
    {
        transform.localScale = enemy.localScale;
    }
}
