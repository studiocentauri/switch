using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    float horizontal = 0.0f;

    [SerializeField] Controller[] playerControllers;

    [SerializeField] Transform[] playerTransforms;
    [SerializeField] float GROUND_POUND_NORMALIZATION_CONSTANT = 0.3f;

    public int currentPlayer; // active player identifier 0 for top and 1 for bottom.

    public CameraManagement cammale;
    public CameraManagement camfemale;

    [SerializeField] bool isInSync; // other player is in sync with the player

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerControllers[currentPlayer].ProcessPower();
            playerControllers[currentPlayer].ProcessJump(1f);

        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {  
            Vector2 temp = playerControllers[currentPlayer].rb.velocity;
            temp.x = 0;
            playerControllers[currentPlayer].rb.velocity = temp; // stops the x velocity of the current player
            currentPlayer = 1 - currentPlayer; // switches the player

            if(currentPlayer == 0)
            {
                cammale.cinemachineVirtualCamera.Priority = 1;
                camfemale.cinemachineVirtualCamera.Priority = 0;
            }
            else
            {
                cammale.cinemachineVirtualCamera.Priority=0;
                camfemale.cinemachineVirtualCamera.Priority = 1;
            }

        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isInSync = !isInSync;
        }
    }

    void FixedUpdate()
    {
        playerControllers[currentPlayer].Move(horizontal);

        if (isInSync) // follow the player
        {
            var targetPos = new Vector2(playerTransforms[currentPlayer].position.x, playerTransforms[1 - currentPlayer].position.y);
            var currentPos = new Vector2(playerTransforms[1 - currentPlayer].position.x, playerTransforms[1 - currentPlayer].position.y);
            if (Vector2.Distance(targetPos, currentPos) > 0.1f)
            {
                playerTransforms[1 - currentPlayer].position = Vector2.MoveTowards(playerTransforms[1 - currentPlayer].position, targetPos, playerControllers[currentPlayer].maxSpeed * Time.deltaTime);
            }
        }
        else
        {
            Vector2 vel = playerControllers[1 - currentPlayer].rb.velocity;
            vel.x = 0; ; // stops the other player
            playerControllers[1 - currentPlayer].rb.velocity = vel;
        }
        horizontal = 0.0f;
    }

    public void ApplyGroundPound(float gp_x) {
        float jumpFactor = 1.2f/Mathf.Pow(Mathf.Abs(playerControllers[1 - currentPlayer].transform.position.x - gp_x), GROUND_POUND_NORMALIZATION_CONSTANT);
        if(jumpFactor < 0.5f) { jumpFactor = 0f; } 
        if(playerControllers[currentPlayer].isMale) {
            playerControllers[1 - currentPlayer].ProcessJump(Mathf.Min(1.2f, jumpFactor));
        }
        else {
            playerControllers[currentPlayer].ProcessJump(Mathf.Min(1.2f, jumpFactor));
        }
        
    }
}
