using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    // Gebruik gemaakt van https://www.youtube.com/watch?v=sPiVz1k-fEs
    public Transform attackPoint;
    public LayerMask playerLayer;

    // Levens
    public int maxHealth = 100;
    int currentHealth;

    // Attack damage
    public float attackRange;
    public int attackDamage;

    // Dit gebruik ik om elke period seconden de Attack functie te roepen
    // https://www.codegrepper.com/code-examples/csharp/set+a+update+for+every+5+seconds+unity
    private float nextActionTime = 0.0f;
    public float period = 0.5f;

    // Health bar en scoreUI
    public HealthBarScript healthBar;
    public ScoreScript scoreUI;

    // Spawner voor als de enemy dood gaat
    public EnemySpawner enemySpawner;

    void Start()
    {
        // In het begin is de HP hetzelfde als de maximale
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    // Update is called once per frame
    void Update()
    {
        // Als de huidige tijd groter is dan de volgende periode tijd (nextActionTime)
        // Dan wordt Attack geroepen en de nextActionTime aangepast.
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            Attack();
        }

        //InvokeRepeating("Attack", 3f, 3f);
    }

    void Attack()
    {
        // Ik kan hier evt een animation gebruiken

        // Player zoeken
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        // Hit de Player
        // Er is maar 1 player, dus eigenlijk is er geen array nodig. Toch maar gebruikt omdat het makkelijk is.
        //  Als ik meer tijd had dan zou ik dit netter opgelost hebben.
        foreach (Collider2D player in hitPlayer)
        {
            if (player.name == "Player")
            {
                Debug.Log("We hit " + player.name);
                player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
            }
               
        }
    }


    // Deze functie wordt geroepen vanaf een andere plek, wanneer de speler de enemy slaat
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        // Als de enemy geen hp meer over heeft, dan gaat hij dood.
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Debuggen
        Debug.Log("Enemy died!");

        // Update score
        scoreUI.AddScore();

        // Spawn een nieuwe enemy
        enemySpawner.newEnemy();

        // uitzetten
        GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject);
        Debug.Log(GameObject.Find("Enemy(Clone)"));
        Destroy(GameObject.Find("Enemy(Clone)"));
        this.enabled = false;
    }

    // Handige functie om te debuggen
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}

