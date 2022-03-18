using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{

    public GameObject Coin;

    [Header("Box")]
    [SerializeField] private Transform box;

    public void instantiate() { 
        GameObject tempItemSpawn = Instantiate(Coin, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        tempItemSpawn.transform.position = this.transform.position;
        tempItemSpawn.SetActive(true);
    }


}
