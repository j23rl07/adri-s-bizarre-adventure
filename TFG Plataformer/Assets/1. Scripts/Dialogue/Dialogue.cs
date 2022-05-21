using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("Text to modify")]
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI nameComponent;

    [Header("Inputs")]
    public string[] lines;
    public Sprite[] faces;
    public string[] names;

    [Header("Misc")]
    public float textSpeed;
    public Image image;
    private int index;

    [Header("Player references")]
    public PlayerCombat playerCombat;
    public PlayerMovement playerMovement;
    public Player player;
    private Rigidbody2D rb;
    public Animator anim;


    void OnEnable()
    {
        gameObject.SetActive(true);
        textComponent.text = string.Empty;
        StartDialogue();
        //Desactivamos los componentes necesarios para hacer que el jugador se quede estático
        playerCombat.enabled = false;
        playerMovement.enabled = false;
        rb = player.GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;
        anim.Play("Player_idle");
    }
            
    void StartDialogue()
    {
        index = 0;
        image.sprite = faces[index];
        nameComponent.text = names[index];
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        //Para mostrar las lineas de una en una
        foreach(char c in lines[index].ToCharArray()) {
            
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == lines[index])
            {
                
                NextLine();
            }
            else
            {                
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            nameComponent.text = names[index];
            image.sprite = faces[index];
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            playerCombat.enabled = true;
            playerMovement.enabled = true;
            gameObject.SetActive(false);
        }
    }
}
