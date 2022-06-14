using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGraphics : MonoBehaviour
{
    public AIPath path;

    // Update is called once per frame
    void Update()
    {
        //Si la velocidad del componente es mayor que un mínimo valor de referencia, significa que se mueve a la derecha
        if(path.desiredVelocity.x >= 0.01f)
        {
            //Aplicamos la transformación del sprite para orientarlo hacia la derecha
            transform.localScale = new Vector3(-4f, 4f, 1f);

            //Comprobamos el caso contrario: hacia la izquierda
        }else if (path.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(4f, 4f, 1f);
        }
    }
}
