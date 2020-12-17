using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    #region globals
    [Tooltip("To adjust cam x sensitivity")]
    public int jumpForce;

    private Player player;
    #endregion

    #region monobehavior
    private void Start()
    {
        player = this.transform.GetComponent<Player>();
    }

    private void Update()
    {
        if (player.state == Player.State.Jumping)
        {
            player.rb.velocity = Vector3.up * jumpForce + player.rb.velocity;
        }

    }
    #endregion
}
