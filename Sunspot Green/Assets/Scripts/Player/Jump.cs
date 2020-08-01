using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public OnTriggers Trigger;
    public int JumpForce;
    public bool isGrounded;

    private Rigidbody PlayerRb;

    private void Start()
    {
        PlayerRb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {

        bool tryJump = Input.GetButtonDown("Jump");
        isGrounded = Trigger.isTriggered;
        if (isGrounded && tryJump)
        {
            PlayerRb.velocity = Vector3.up * JumpForce + PlayerRb.velocity;
        }

    }
}
