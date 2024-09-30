using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    private GameObject selectedCharacter;
    public GameObject conePrefab;  // Assign the cone prefab in the Inspector
    private GameObject activeCone; // Keeps track of the active cone
    public LayerMask selectableLayer; // Set this to detect only characters
    public LayerMask groundLayer; 
    public LayerMask obstacleLayer;
    void Update()
    {
        HandleCharacterSelection();

        // 如果有选中的角色，处理点击地面移动
        if (selectedCharacter != null && Input.GetMouseButtonDown(0))
        {
            MoveSelectedCharacter();
        }
    }

    void HandleCharacterSelection()
    {
        // Check for left mouse click to select character
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectableLayer))
            {
                GameObject clickedObject = hit.collider.gameObject;

                // Reset obstacles when selecting a new character
                ResetObstacles();

                // Deselect the previous character (if any)
                if (selectedCharacter != null)
                {
                    DeselectCharacter();
                }

                // Select the new character
                SelectCharacter(clickedObject);
            }
        }

        // Right-click to deselect the current character
        if (Input.GetMouseButtonDown(1) && selectedCharacter != null)
        {
            DeselectCharacter();
        }
    }

    void SelectCharacter(GameObject character)
    {
        selectedCharacter = character;

        // Instantiate the cone above the character
        Vector3 conePosition = selectedCharacter.transform.position + new Vector3(0, 2, 0); // 2 units above
        activeCone = Instantiate(conePrefab, conePosition, Quaternion.identity);
        activeCone.transform.SetParent(selectedCharacter.transform);  // Make sure the cone follows the character
    }

    void DeselectCharacter()
    {
        // Destroy the active cone when deselecting
        if (activeCone != null)
        {
            Destroy(activeCone);
        }
        selectedCharacter = null;
    }

    // Move the selected character to the clicked ground position
    void MoveSelectedCharacter()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
           
            CharacterMovement characterMovement = selectedCharacter.GetComponent<CharacterMovement>();
            if (characterMovement != null)
            {
                characterMovement.SetTargetPosition(hit.point);  
            }
        }
    }

    // Reset obstacles when selecting a new character
    void ResetObstacles()
    {
        Collider[] obstacles = Physics.OverlapSphere(Vector3.zero, 100f, obstacleLayer); 
        foreach (Collider collider in obstacles)
        {
            Obstacle obstacle = collider.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                obstacle.SetObstacleActive(true);  
            }
        }
    }
}
