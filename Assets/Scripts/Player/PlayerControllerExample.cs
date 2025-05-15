using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerExample : MonoBehaviour
{
    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void Disable()
    {
        playerControls.Disable();
        playerControls.Land.Explosion.started -= Explosion;
    }

    private void Start()
    {
        playerControls.Land.Explosion.started += Explosion;
        playerControls.Land.Explosion.performed += Explosion;
        playerControls.Land.Explosion.canceled += Explosion;

        //playerControls.Land.Explosion.started -= Explosion;
    }
    private void Explosion(InputAction.CallbackContext context)
    {
        Debug.Log("Explosion");
        context.ReadValue<float>();
        context.ReadValueAsButton();
    }
    private void Update()
    {
        Vector2 move = playerControls.Land.Move.ReadValue<Vector2>();
        Debug.Log(move);
        // playerControls.Land.Jump.ReadValue<float>();
        // if (playerControls.Land.Jump.ReadValue<float>() == 1)
        if (playerControls.Land.Jump.triggered)
            Debug.Log("Jump");
    }
}
