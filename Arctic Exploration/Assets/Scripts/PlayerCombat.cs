using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    // Gebruik gemaakt van https://www.youtube.com/watch?v=sPiVz1k-fEs
    public Transform attackPoint;
    public float attackRange;
    public int attackDamage;
    public LayerMask enemyLayers;

    // Levens en score
    public int maxHealth = 500;
    private int currentHealth;
    public int score;

    // Variabele zodat de speler niet space kan spammen.
    // Ik gebruik hetzelfde als in EnemyCombat.cs
    // Credits naar de website voor de code staan daar
    private bool allowedToAttack = true;
    private float nextActionTime = 0.0f;
    public float attackPeriod = 5f;

    // Health bar
    public HealthBarScript healthBar;
 

    // Start is called before the first frame update
    void Start()
    {
        // In het begin is de HP hetzelfde als de maximale
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > nextActionTime)
        {
            allowedToAttack = true;
        }
        
        if (allowedToAttack && Input.GetKeyDown(KeyCode.Space))
        {
            // Laat niet meer toe om te attacken en zet een cooldown op de attack.
            allowedToAttack = false;
            nextActionTime += Time.time + attackPeriod;
            // Val aan
            Attack();
        }
    }

    void Attack()
    {
        // Ik kan hier een animation gebruiken
        // Vijanden zoeken
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Hit alle vijanden
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<EnemyCombat>().TakeDamage(attackDamage);
        }
    }

    // Deze functie wordt geroepen vanaf een andere plek, wanneer de speler geslagen wordt
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        // Animatie
        //animator.SetTrigger("Hurt");

        // Als de enemy geen hp meer over heeft, dan gaat hij dood.
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Voor als de speler dood gaat
    void Die()
    {
        // Debuggen
        Debug.Log("Player died!");

        //Uitzetten
        GetComponent<Collider2D>().enabled = false;
        gameObject.SetActive(false);
        this.enabled = false;

        // Laat tekst in scherm zien en begin opnieuw
    }
    // Handige functie om te debuggen
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}

