using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireballHolder : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    //Arreglar dirección de disparo determinando la orientación del enemigo
    private void Update()
    {
        transform.localScale = enemy.localScale;
    }
}
