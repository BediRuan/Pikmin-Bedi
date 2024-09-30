using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinZone : MonoBehaviour
{
    public int totalCharactersNeeded = 6;  // Total number of characters required to win
    private int currentCharactersInZone = 0;

    public TMP_Text overallCounterText; // Reference to the overall counter TextMeshPro Text
    public TMP_Text remainingCounterText; // Reference to the remaining counter TextMeshPro Text
    public TMP_Text winMessageText; // Reference to the Win message TextMeshPro Text

    void Start()
    {
        // Hide the win message at the start
        winMessageText.gameObject.SetActive(false);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        CharacterMovement character = other.GetComponent<CharacterMovement>();

        if (character != null)
        {
            currentCharactersInZone++;
            UpdateUI();
            CheckForWin();
        }
    }

   
    private void OnTriggerExit(Collider other)
    {
        CharacterMovement character = other.GetComponent<CharacterMovement>();

        if (character != null)
        {
            currentCharactersInZone--;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        overallCounterText.text = $"{currentCharactersInZone} of {totalCharactersNeeded} present";
        int remainingCharacters = totalCharactersNeeded - currentCharactersInZone;
        remainingCounterText.text = $"{remainingCharacters} more needed";
    }

   
    private void CheckForWin()
    {
        if (currentCharactersInZone >= totalCharactersNeeded)
        {
            Debug.Log("Level Won!");
            // win message
            winMessageText.gameObject.SetActive(true);
            winMessageText.text = "Level Complete!";  
        }
    }
}
