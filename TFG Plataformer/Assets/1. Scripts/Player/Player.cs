using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public static int currentHealth;
    public HealthBar healthBar;

    [Header("Mana")]
    public int maxMana = 100;
    public int currentMana;
    public WaitForSeconds regenTick = new WaitForSeconds(1f);
    public ManaBar manaBar;

    [Header("iFrames")]
    [SerializeField] private float invulDuration;
    [SerializeField] private int nFlashes;
    private SpriteRenderer spr;

    public Animator animator;
    private Weapon cast;
    private Rigidbody2D rb2d;

    [Header("Other")]
    public int enemyDamage = 20;


    [HideInInspector] public bool isHurt = false;
    [HideInInspector] public bool isDead = false;


    // Start is called before the first frame update
    void Start()
    {
        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);

        cast = GetComponent<Weapon>();
        spr = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(enemyDamage);
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            useMana(20);
        }
        
    }
    
    public void TakeDamage(int damage)
    {
        isHurt = true;
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        StartCoroutine(invulnerability());

        if(currentHealth <= 0)
        {
            GetComponent<PlayerMovement>().enabled = false;
            isDead = true;
            GameObject.Destroy(gameObject, 2);
        }
    }

    public void heal(int heal)
    {
        currentHealth += heal;
        healthBar.SetHealth(currentHealth);
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void healMana(int heal)
    {
        currentMana += heal;
        manaBar.SetMana(currentMana);
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }

    public void useMana(int manaUse)
    {
        if(manaUse <= currentMana) {
            cast.enabled = cast.enabled;
            currentMana -= manaUse;
            manaBar.SetMana(currentMana);
            //StartCoroutine(RegenMana());
        }else {
            cast.enabled = !cast.enabled;
        }
    }

    //private IEnumerator RegenMana()
    //{
        //yield return new WaitForSeconds(2);
        //while(currentMana < maxMana)
        //{
            //currentMana += maxMana / 100;
            //manaBar.SetMana(currentMana);
            //yield return regenTick;
        //}
    //}

    private IEnumerator invulnerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < nFlashes; i++)
        {
            spr.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(invulDuration / (nFlashes * 2));
            spr.color = Color.white;
            yield return new WaitForSeconds(invulDuration / (nFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }

    public IEnumerator playerKnockback(float kbDuration, float kbPow, Vector3 kbDir)
    {
        float timer = 0;
        while (kbDuration > timer)
        {
            timer += Time.deltaTime;
            rb2d.AddForce(new Vector3(kbDir.x * -10, kbDir.y + kbPow, transform.position.z));
        }

        yield return 0;
    }
}
