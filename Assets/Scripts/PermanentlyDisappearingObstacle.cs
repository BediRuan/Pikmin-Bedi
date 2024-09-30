using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentlyDisappearingObstacle : Obstacle
{
   
    private void OnTriggerEnter(Collider other)
    {
        CharacterMovement character = other.GetComponent<CharacterMovement>();

        
        if (character != null && character.characterType == obstacleType)
        {
            
            DestroyObstacle();
        }
    }

    
    private void DestroyObstacle()
    {
        
        Debug.Log("obstacle destroied£¡");
        Destroy(gameObject); 
    }
}
