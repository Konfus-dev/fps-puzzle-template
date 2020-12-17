using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region globals
    [Tooltip("To adjust player speed")]
    public float baseSpeed;
    [Tooltip("To adjust player sprint speed")]
    public float sprintSpeedMod;
    [Tooltip("To adjust player fov when sprinting")]
    public float sprintFOVMod = 1.25f;
    [Tooltip("To adjust player crouch speed")]
    public float crouchSpeedMod;
    [Tooltip("To adjust player fov when sprinting")]
    public float crouchFOVMod = 0.9f;
    [Tooltip("To adjust how fast player reaches sprint")]
    public float getToSpeed;
    [Tooltip("To adjust how fast player fov changes")]
    public float getToFOV;

    private Player player;

    private float adjustSpeed;
    private float baseFOV;
    #endregion

    #region monobehavior
    private void Start()
    {
        player = this.transform.GetComponent<Player>();
        baseFOV = player.cam.fieldOfView;
        adjustSpeed = baseSpeed;
    }

    void FixedUpdate()
    {
        //get input
        Vector2 moveInput = player.input.GetPlayerMovement();

        //get move direction from input
        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y);
        direction.Normalize();

        //get player state and adjust speed appropriately
        if (player.state == Player.State.Sprinting)
        {
            Sprint();
        }
        else if (player.state == Player.State.Crouching)
        {
            Crouch();
        }
        else
        {
            NormalMovement();
        }

        //apply movement to player rb
        Vector3 targetVelocity = transform.TransformDirection(direction) * adjustSpeed * Time.fixedDeltaTime;
        targetVelocity.y = player.rb.velocity.y;
        player.rb.velocity = Vector3.Lerp(player.rb.velocity, targetVelocity, Time.fixedDeltaTime * 10);
    }
    #endregion

    #region private methods
    private void Sprint()
    {
        player.cam.fieldOfView = Mathf.Lerp(player.cam.fieldOfView, baseFOV * sprintFOVMod, Time.deltaTime * getToFOV);
        adjustSpeed = Mathf.Lerp(adjustSpeed, baseSpeed * sprintSpeedMod, Time.fixedDeltaTime * getToSpeed);
    }

    private void Crouch()
    {
        player.cam.fieldOfView = Mathf.Lerp(player.cam.fieldOfView, baseFOV * crouchFOVMod, Time.deltaTime * getToFOV);
        adjustSpeed = Mathf.Lerp(adjustSpeed, baseSpeed * crouchSpeedMod, Time.fixedDeltaTime * getToSpeed);
    }

    private void NormalMovement()
    {
        player.cam.fieldOfView = Mathf.Lerp(player.cam.fieldOfView, baseFOV, Time.deltaTime * getToFOV);
        adjustSpeed = Mathf.Lerp(adjustSpeed, baseSpeed, Time.fixedDeltaTime * getToSpeed);
    }
    #endregion
}
