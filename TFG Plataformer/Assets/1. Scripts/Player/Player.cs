using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public Vector3 lastCheckpoint;

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
    [HideInInspector] public bool canRespawn = false;


    // Start is called before the first frame update
    void Start()
    {
        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        lastCheckpoint = transform.position;

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
            healMana(20);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si toca un checkpoint
        if(collision.gameObject.layer == 10)
        {
            Vector3 location = collision.transform.GetChild(0).position;
            lastCheckpoint = new Vector3(location.x, location.y, transform.position.z);
        }
        //Si toca una DeathZone
        if(collision.gameObject.layer == 11)
        {
            StartCoroutine(DeathAndRespawn(lastCheckpoint));
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
            StartCoroutine(DeathAndRespawn(lastCheckpoint));
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

    public bool useMana(int manaUse)
    {
        if(manaUse <= currentMana) {
            currentMana -= manaUse;
            manaBar.SetMana(currentMana);
            return true;
            //StartCoroutine(RegenMana());
        }else {
            return false;
        }
    }

    public void Die()
    {
        currentHealth = 0;
        healthBar.SetHealth(currentHealth);
        currentMana = 0;
        manaBar.SetMana(currentMana);

        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerCombat>().enabled = false;
        GetComponent<Weapon>().enabled = false;
        isDead = true;
    }
    public void Respawn(Vector3 location)
    {
        isDead = false;
        canRespawn = false;

        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
        currentMana = maxMana;
        manaBar.SetMana(currentMana);

        transform.position = location;
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerCombat>().enabled = true;
        GetComponent<Weapon>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
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
            rb2d.AddForce(new Vector3(kbDir.x, kbDir.y , transform.position.z));
        }

        yield return 0;
    }

    public IEnumerator DeathAndRespawn(Vector3 location)
    {
        float g = rb2d.gravityScale;
        rb2d.velocity = new Vector2(0, 0);
        rb2d.gravityScale = 0;
        Die();
        while (!canRespawn)
            yield return null;
        yield return new WaitForSeconds(.5f);
        Respawn(location);
        rb2d.gravityScale = g;
    }
}
