using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Obstacle : MonoBehaviour
{
    public string obstacleType;  // Obstacle types "Fire", "Water", "Electric"
    private NavMeshObstacle navMeshObstacle;

    void Start()
    {
        navMeshObstacle = GetComponent<NavMeshObstacle>();
        navMeshObstacle.carving = true; 
    }

   
    public void SetObstacleActive(bool isActive)
    {
        navMeshObstacle.enabled = isActive;
    }
}
