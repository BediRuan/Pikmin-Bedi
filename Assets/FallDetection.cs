using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FallDetection : MonoBehaviour
{
    public GameObject lowerPlane; // Reference to the lower plane GameObject
    public float fallSpeed = 5f;  // Speed at which the character "falls"
    private NavMeshAgent navMeshAgent;
    private bool isFalling = false;
    private bool reachedLowerPlane = false;
    private Vector3 fallTargetPosition;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // If the character is falling, smoothly move downwards
        if (isFalling)
        {
            // Lerp or move down towards the fall target position
            transform.position = Vector3.MoveTowards(transform.position, fallTargetPosition, fallSpeed * Time.deltaTime);

            // Check if character reached the lower plane
            if (Vector3.Distance(transform.position, fallTargetPosition) < 0.1f)
            {
                CompleteFall();
            }
        }
        else if (!reachedLowerPlane && IsNearEdge())
        {
            StartFalling();
        }
    }

    //check if closse to edge
    bool IsNearEdge()
    {
        RaycastHit hit;
        // shoot a ray downward
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
        {
            return false;  // character still on ground
        }
        return true;  
    }

    
    void StartFalling()
    {
        isFalling = true;
        navMeshAgent.enabled = false;  //disable NavMeshAgent 

       
        fallTargetPosition = new Vector3(transform.position.x, lowerPlane.transform.position.y + 1f, transform.position.z);  // +1 to adjust for height

        
        GetComponent<Rigidbody>().isKinematic = true;  
    }

    
    void CompleteFall()
    {
        isFalling = false;
        reachedLowerPlane = true;

        // reactivate NavMeshAgent
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            navMeshAgent.enabled = true;
            navMeshAgent.Warp(hit.position);  
        }
    }
}
