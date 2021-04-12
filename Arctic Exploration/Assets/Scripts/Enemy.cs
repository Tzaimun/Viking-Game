using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Gebruik gemaakt van https://www.youtube.com/watch?v=sPiVz1k-fEs
    public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        // In het begin is de HP hetzelfde als de maximale
        currentHealth = maxHealth;
    }

    // Deze functie wordt geroepen vanaf een andere plek, wanneer de speler de enemy slaat
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Animatie

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

        // Doodgaan animatie
        
        // Daadwerkelijk weghalen van vijand
    }


}
