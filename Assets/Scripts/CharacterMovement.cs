using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public GameObject moveIndicatorPrefab;
    private GameObject activeMoveIndicator;

    public string characterType;  // character types: "Fire", "Water", "Electric"
    public LayerMask obstacleLayer; // check layer
    private List<Obstacle> disabledObstacles = new List<Obstacle>(); 

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true; 
    }

    void Update()
    {
       
        if (navMeshAgent.hasPath && !navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    // reactivate
                    EnableDisabledObstacles();

                    // destroy indicator
                    if (activeMoveIndicator != null)
                    {
                        Destroy(activeMoveIndicator);
                    }
                }
            }
        }
    }

    
    public void SetTargetPosition(Vector3 position)
    {
        navMeshAgent.SetDestination(position);

        
        DisableMatchingObstacles();

        
        if (moveIndicatorPrefab != null)
        {
            if (activeMoveIndicator != null)
            {
                Destroy(activeMoveIndicator);
            }
            activeMoveIndicator = Instantiate(moveIndicatorPrefab, position, Quaternion.identity);
        }
    }

    
    private void DisableMatchingObstacles()
    {
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, 50f, obstacleLayer); 
        foreach (Collider collider in colliders)
        {
            Obstacle obstacle = collider.GetComponent<Obstacle>();
            if (obstacle != null && obstacle.obstacleType == characterType)
            {
                // disable obstacle
                obstacle.SetObstacleActive(false);
                disabledObstacles.Add(obstacle); 
            }
        }
    }

    
    private void EnableDisabledObstacles()
    {
        foreach (Obstacle obstacle in disabledObstacles)
        {
            if (obstacle != null)
            {
                obstacle.SetObstacleActive(true);
            }
        }
        disabledObstacles.Clear(); 
    }
}

