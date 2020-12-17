using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    #region globals
    [Tooltip("How far player can grab an obj from")]
    public float grabDist = 5;
    [Tooltip("How fast obj adjusts to camera movement")]
    public float grabbedMoveSpeed = 10;
    [Tooltip("Obj grabbed move speed smoothing")]
    public float smoothTime = 0.3F;

    private Player player;
    #endregion

    #region monobehavior
    private void Start()
    {
        player = this.transform.parent.GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        bool grab = InputManager.Instance.PlayerPressGrab();

        if (grab && player.grabbedObj != null) //hold object while pressing grab
            HoldObj();
        else if (grab) //pick up obj
            GrabObj();
        else DropObj(); //drop obj when stop pressing grab
    }
    #endregion

    #region private methods
    private void GrabObj()
    {
        Vector3 castPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.2f);
        
        RaycastHit hit;
        if (Physics.Raycast(castPos, transform.forward, out hit, grabDist) && hit.transform.GetComponent<Rigidbody>() && player.grabbedObj == null)
        {
            player.grabbedObj = hit.transform.gameObject;
        }
    }

    private void HoldObj()
    {
        Vector3 velocity = Vector3.zero;
        player.grabbedObj.GetComponent<Rigidbody>().velocity =
                    Vector3.SmoothDamp(player.grabbedObj.GetComponent<Rigidbody>().velocity, 
                    ((player.cam.transform.position + player.cam.transform.up * .1f + 
                    player.cam.transform.forward * 1.8f) - player.grabbedObj.transform.position) * grabbedMoveSpeed, 
                    ref velocity, smoothTime);
    }

    private void DropObj()
    {
        player.grabbedObj = null;
    }
    #endregion
}


