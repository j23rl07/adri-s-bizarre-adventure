using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueLogic : MonoBehaviour
{
    public GameObject attackEffect1;
    public GameObject attackEffect2;
    public GameObject attackEffect3;
    public GameObject attackEffect4;
    public GameObject attackEffect5;
    public GameObject attackEffect6;
    public GameObject attackEffect7;
    public GameObject attackEffect8;
    public GameObject attackEffect9;
    public GameObject attackEffect10;
    public GameObject attackEffect11;


    void Start()
    {
        InvokeRepeating("EnableExplosion4", 2f, 5f);
        InvokeRepeating("EnableExplosion1", 2f, 10f);
        InvokeRepeating("EnableExplosion2", 3f, 15f);
        InvokeRepeating("EnableExplosion3", 4f, 20f);

        InvokeRepeating("EnableExplosion5", 2f, 5f);
        InvokeRepeating("EnableExplosion6", 3f, 10f);
        InvokeRepeating("EnableExplosion7", 4f, 15f);
        InvokeRepeating("EnableExplosion8", 2f, 20f);

        InvokeRepeating("EnableExplosion9", 5f, 5f);
        InvokeRepeating("EnableExplosion10", 5f, 5f);
        InvokeRepeating("EnableExplosion11", 5f, 5f);

    }

    void EnableExplosion1()
    {
        attackEffect1.active = true;
    }
    
    void EnableExplosion2()
    {
        attackEffect2.active = true;
    }

    void EnableExplosion3()
    {
        attackEffect3.active = true;
    }

    void EnableExplosion4()
    {
        attackEffect4.active = true;
    }

    void EnableExplosion5()
    {
        attackEffect5.active = true;
    }

    void EnableExplosion6()
    {
        attackEffect6.active = true;
    }

    void EnableExplosion7()
    {
        attackEffect7.active = true;
    }

    void EnableExplosion8()
    {
        attackEffect8.active = true;
    }

    void EnableExplosion9()
    {
        attackEffect9.active = true;
    }

    void EnableExplosion10()
    {
        attackEffect10.active = true;
    }

    void EnableExplosion11()
    {
        attackEffect11.active = true;
    }


    void OnEnable()
    {
        Instantiate(attackEffect1, transform.position, transform.rotation);
        Instantiate(attackEffect2, transform.position, transform.rotation);
        Instantiate(attackEffect3, transform.position, transform.rotation);
        Instantiate(attackEffect4, transform.position, transform.rotation);
        Instantiate(attackEffect5, transform.position, transform.rotation);
        Instantiate(attackEffect6, transform.position, transform.rotation);
        Instantiate(attackEffect7, transform.position, transform.rotation);
        Instantiate(attackEffect8, transform.position, transform.rotation);
        Instantiate(attackEffect9, transform.position, transform.rotation);
        Instantiate(attackEffect10, transform.position, transform.rotation);
        Instantiate(attackEffect11, transform.position, transform.rotation);
    }
}
