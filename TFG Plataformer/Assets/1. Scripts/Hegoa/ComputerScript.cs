using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerScript : MonoBehaviour
{
    [SerializeField] private GameObject hintText;
    private bool hasAccess;
    [HideInInspector] public bool isOff;
    public Coroutine timer = null;

    [Header("Time Params")]
    public bool runningTimer;
    public int currentTime;
    public int maxTime;

    [Header("References")]
    public Player player;
    public Text timerText;
    public GameObject text;
    public ComputerBoss bossScript;

    private void Start()
    {
        text.SetActive(false);
        runningTimer = false;
        hintText.gameObject.SetActive(false);
    }
    void Update()
    {
        if (hasAccess && Input.GetKeyDown(KeyCode.E))
        {
            ActivateDefense();
        }
        if (runningTimer == false && timer != null)
        {
            StopCoroutine(timer);
            text.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isOff)
        {
            hasAccess = true;
            hintText.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isOff)
        {
            hasAccess = false;
            hintText.gameObject.SetActive(false);
        }
    }
    
    private void ActivateDefense()
    {
        isOff = true;
        hasAccess = false;
        hintText.gameObject.SetActive(false);
        if (runningTimer == false)
        {
            currentTime = maxTime;
            timerText.text = "" + currentTime;
            timer = StartCoroutine(Timer());
            text.SetActive(true);
        }
    }
    public IEnumerator Timer()
    {
        runningTimer = true;
        while (currentTime > 0)
        {
            bossScript.currentTime = this.currentTime;
            yield return new WaitForSeconds(1);
            currentTime--;
            timerText.text = "" + currentTime;
        }
        GetComponent<ComputerBoss>().exit.gameObject.SetActive(false);
    }
}
