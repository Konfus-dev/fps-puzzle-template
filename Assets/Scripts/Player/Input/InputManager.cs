using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region globals
    private InputActions playerInput;
    private static InputManager instance;
    public static InputManager Instance
    {
        get
        {
            return instance;
        }
    }

    private bool holdingSprint = false;
    private bool holdingGrab = false;
    #endregion

    #region monobehavior
    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else instance = this;

        playerInput = new InputActions();

        playerInput.Player.Grab.performed += _ => holdingGrab = true;
        playerInput.Player.Grab.canceled += _ => holdingGrab = false;

        playerInput.Player.Sprint.performed += _ => holdingSprint = true;
        playerInput.Player.Sprint.canceled += _ => holdingSprint = false;
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
    #endregion

    #region public methods
    public Vector2 GetPlayerMovement()
    {
        return playerInput.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetPlayerMouseDelta()
    {
        return playerInput.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerPressJump()
    {
        return playerInput.Player.Jump.triggered;
    }

    public bool PlayerPressCrouch()
    {
        return playerInput.Player.Crouch.triggered;
    }

    public bool PlayerPressSprint()
    {
        return holdingSprint;
    }

    public bool PlayerPressGrab()
    {
        return holdingGrab;
    }
    #endregion
}
