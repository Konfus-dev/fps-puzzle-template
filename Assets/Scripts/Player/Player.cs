using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region globals
    public enum State
    {
        Sprinting,
        Walking,
        Crouching,
        Jumping,
        Idle
    }

    [HideInInspector]
    public State state;
    [HideInInspector]
    public InputManager input;
    [HideInInspector]
    public Camera cam;
    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public bool grounded;
    [HideInInspector]
    public GameObject grabbedObj;

    private Trigger groundTrigger;
    #endregion

    #region monobehavior
    private void Awake()
    {
        input = InputManager.Instance;
        rb = this.GetComponent<Rigidbody>();
        cam = this.GetComponentInChildren<Camera>();
        groundTrigger = this.GetComponentInChildren<Trigger>();

        state = new State();
        state = State.Idle;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        CheckIsGrounded();
        UpdatePlayerState();
    }
    #endregion

    #region private methods
    private void UpdatePlayerState()
    {
        Vector2 moveInput = input.GetPlayerMovement();
        bool moving = grounded && (Mathf.Abs(moveInput.x) > 0.1f || Mathf.Abs(moveInput.y) > 0.1f);
        bool sprinting = moving && input.PlayerPressSprint();
        bool crouching = ToggleCrouchState();
        
        bool jumping = grounded && input.PlayerPressJump();

        Debug.Log(state.ToString());

        if (jumping) //Jumping
            state = State.Jumping;
        else if (crouching) //Crouching
            state = State.Crouching;
        else if (sprinting) //Sprinting
            state = State.Sprinting;
        else if (moving) //Walking
            state = State.Walking;
        else state = State.Idle; //Idle
    }

    private bool ToggleCrouchState()
    {
        if (state == State.Crouching && input.PlayerPressCrouch())
            return false;
        else if (input.PlayerPressCrouch() || state == State.Crouching)
            return true;
        else return false;
    }

    private void CheckIsGrounded()
    {
        if (groundTrigger.isTriggered) grounded = true;
    }
    #endregion
}
