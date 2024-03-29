using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    float horizontal = 0.0f;

    [SerializeField] Controller[] playerControllers;

    [SerializeField] Transform[] playerTransforms;
    [SerializeField] float GROUND_POUND_NORMALIZATION_CONSTANT = 0.3f;
    [SerializeField] float MAP_COOLDOWN = 0.1f;
    [SerializeField] GameObject pauseMenu;
    private float map_timer = 0;

    public int currentPlayer; // active player identifier 0 for top and 1 for bottom.

    public CameraManagement cammale;
    public CameraManagement camfemale;
    public TileFlip tileflip;

    public delegate void SmashAction(float gp_x);
    public static event SmashAction onSmash;

    [SerializeField] bool isInSync; // other player is in sync with the player

    public bool canSwitch = true;

    void Start() {
        AudioManager.instance.PlayBackground();
    }
    public void OnPause()
    {
        if (pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void OnMove(InputValue value)
    {
        var dir = value.Get<Vector2>();
        horizontal = dir.x;
    }

    public void OnJump()
    {
        playerControllers[currentPlayer].ProcessPower();
        playerControllers[currentPlayer].ProcessJump(1f);
    }

    public void OnSwitch()
    {
        if (canSwitch)
        {
            playerControllers[currentPlayer].isActive = false;
            playerControllers[currentPlayer].rb.velocity = new Vector2(0, playerControllers[currentPlayer].rb.velocity.y); // stops the current player
            playerControllers[currentPlayer].rb.gameObject.GetComponentInChildren<Animator>().SetFloat("isRunning", 0);
            currentPlayer = 1 - currentPlayer; // switches the player
            playerControllers[currentPlayer].isActive = true;

            if (currentPlayer == 0)
            {
                cammale.cinemachineVirtualCamera.Priority = 1;
                camfemale.cinemachineVirtualCamera.Priority = 0;
            }
            else
            {
                cammale.cinemachineVirtualCamera.Priority = 0;
                camfemale.cinemachineVirtualCamera.Priority = 1;
            }
        }
    }

    public void OnSync()
    {
        //isInSync = !isInSync;
    }

    public void OnMapSwitch()
    {
        if (canSwitch)
        {
            RaycastHit2D hit1 = Physics2D.Raycast(new Vector2(playerTransforms[0].position.x, -2.5f - playerTransforms[0].position.y), Vector2.down);
            RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(playerTransforms[1].position.x, -2.5f - playerTransforms[1].position.y), Vector2.up);
            Debug.DrawRay(new Vector2(playerTransforms[0].position.x, -2.5f - playerTransforms[0].position.y), Vector2.down);
            Debug.DrawRay(new Vector2(playerTransforms[1].position.x, -2.5f - playerTransforms[1].position.y), Vector2.up);
            if (map_timer >= MAP_COOLDOWN && (hit1.collider == null || hit1.collider.name == "ScreenBoundry" || hit1.collider.tag == "Player" || hit1.collider.name == "Game End Trigger") && (hit2.collider == null || hit2.collider.name == "ScreenBoundry" || hit2.collider.tag == "Player" || hit2.collider.name == "Game End Trigger"))
            {
                if(tileflip.transform.Find("Obstacles"))
                {
                    tileflip.transform.Find("Obstacles").GetComponent<FixRigidBodies>().SwapGravity();
                }
                map_timer = 0; // reset cooldown timer
                AudioManager.instance.PlaySound("Map Switch");
                if (tileflip.transform.rotation.eulerAngles.y == 0)
                {
                    tileflip.gameObject.GetComponent<Animator>().Play("TileFlipTo");
                }
                else
                {
                    tileflip.gameObject.GetComponent<Animator>().Play("TileFlipFrom");
                }

            }
            else
            {
                Debug.Log(hit1.collider.name);
                Debug.Log(hit2.collider.name);
                if(cammale.cinemachineVirtualCamera.Priority == 1)
                    cammale.ShakeCamera(4f, .2f);
                else
                    camfemale.ShakeCamera(4f, .2f);
                Debug.Log("Cant flip now");
            }
        }
    }

    void Update()
    {
        map_timer += Time.deltaTime; // implementing map cooldown
        map_timer = Mathf.Min(map_timer, MAP_COOLDOWN);
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
        // horizontal = 0.0f;
    }

    public void ApplyGroundPound(float gp_x)
    {

        if (onSmash != null)
        {
            onSmash(gp_x);
        }

        float jumpFactor = 1.8f / Mathf.Pow(Mathf.Abs(playerControllers[1 - currentPlayer].transform.position.x - gp_x), GROUND_POUND_NORMALIZATION_CONSTANT);
        if (jumpFactor < 0.5f) { jumpFactor = 0f; }
        if (playerControllers[currentPlayer].isMale)
        {
            playerControllers[1 - currentPlayer].ProcessJump(Mathf.Min(1.2f, jumpFactor));
        }
        else
        {
            playerControllers[currentPlayer].ProcessJump(Mathf.Min(1.2f, jumpFactor));
        }

    }
}
