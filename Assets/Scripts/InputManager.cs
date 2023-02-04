using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControls controls;
    
    float horizontal = 0.0f;

    [SerializeField] Controller[] playerControllers;

    [SerializeField] Transform[] playerTransforms;

    [SerializeField] int currentPlayer; // active player identifier

    [SerializeField] bool isInSync; // other player is in sync with the player

    private void Awake()
    {
        controls = new PlayerControls();
        controls.InGame.Jump.performed += _ => Jump();
        controls.InGame.Switch.performed += _ => Switch();
        controls.InGame.Sync.performed += _ => Sync();
    }

    private void Jump()
    {
        playerControllers[currentPlayer].ProcessPower();
        playerControllers[currentPlayer].ProcessJump();
    }

    private void Switch()
    {
        playerControllers[currentPlayer].rb.velocity = Vector2.zero; // stops the current player
        currentPlayer = 1 - currentPlayer; // switches the player
    }

    private void Sync()
    {
        isInSync = !isInSync;
    }

    void Update()
    {
        // horizontal = Input.GetAxisRaw("Horizontal");
        horizontal = controls.InGame.Movement.ReadValue<float>();
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
}
