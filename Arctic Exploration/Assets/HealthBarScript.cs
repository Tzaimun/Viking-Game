using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{

    // Gebruik gemaakt van https://www.youtube.com/watch?v=BLfNP4Sc_iA

    public Slider slider;

    // Deze functie wordt aangeroepen door een andere script. Dit script bepaalt hoe groot de health bar is.
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    // Met deze functie kan je de max health aanpassen vanuit een ander script. Dit is handig omdat de player en enemy andere health hebben.
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
}
