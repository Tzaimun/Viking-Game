using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    // Gebruik gemaakt van https://www.codegrepper.com/code-examples/csharp/change+textmesh+pro+text+unity

    // Hierin kan ik het text element aanroepen
    /*
    public void SetScore(int score)
    {    
        // Dit is het text element
        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
        // Hierin zet ik de score
        textmeshPro.SetText("{0}", score);
    }
    */

    public TextMeshProUGUI textDisplay;
    public int score = 0;

    public void SetScore(int newScore)
    {
        score = newScore;
        Debug.Log($"Score is now {score}");
        textDisplay.text = $"{score}";
    }

    public void AddScore()
    {
        score += 1;
        Debug.Log($"Score is now {score}");
        textDisplay.text = $"{score}";
    }
}
