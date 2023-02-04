using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Controller player;

    float horizontal = 0.0f;

    [SerializeField] Controller[] playerControllers;

    [SerializeField] Transform[] playerTransforms;

    [SerializeField] int currentPlayer; // active player identifier

    [SerializeField] bool isInSync; // other player is in sync with the player

    float timer = 0f; // to lerp other player's position

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            playerControllers[currentPlayer].rb.velocity = Vector2.zero; // stops the current player
            currentPlayer = 1 - currentPlayer; // switches the player
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
            isInSync = !isInSync;
    }

    void FixedUpdate()
    {
        playerControllers[currentPlayer].Move(horizontal);
        playerControllers[currentPlayer].processJump();
        if (isInSync) // follow the player
        {
            var targetPos = new Vector2(playerTransforms[currentPlayer].position.x, playerTransforms[1 - currentPlayer].position.y);
            var currentPos = new Vector2(playerTransforms[1 - currentPlayer].position.x, playerTransforms[1 - currentPlayer].position.y);
            if (Vector2.Distance(targetPos,currentPos) > 0.1f)
            {
                playerTransforms[1 - currentPlayer].position = Vector2.MoveTowards(playerTransforms[1 - currentPlayer].position, targetPos, 15f * Time.deltaTime);
            }
        }
        else
        {
            playerControllers[1 - currentPlayer].rb.velocity = Vector2.zero; // stops the other player
        }
        horizontal = 0.0f;
    }
}
