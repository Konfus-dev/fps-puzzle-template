using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravGlove : MonoBehaviour
{
    public float GrabDist = 5;
    public float GrabbedMoveSpeed = 10;
    public float smoothTime = 0.3F;
    public Transform GrabPos;
    public GameObject GrabbedObj;

    private RaycastHit Hit;
    private bool NoGrab = false;
    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        bool grab = Input.GetMouseButton(0);
        if(!NoGrab)
        {
            Vector3 castPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f);

            //if holding left click down and have hit and hit has rigidbody
            if (grab && Physics.Raycast(castPos, transform.forward, out Hit, GrabDist) && Hit.transform.GetComponent<Rigidbody>())
            {
                if(GrabbedObj == null) GrabbedObj = Hit.transform.gameObject;
            }
            else if (!grab)
            {
                GrabbedObj = null;
            }

            //manipulate grabbed thing
            if (GrabbedObj)
            {
                GrabbedObj.GetComponent<Rigidbody>().velocity = 
                    Vector3.SmoothDamp(GrabbedObj.GetComponent<Rigidbody>().velocity, (GrabPos.position - GrabbedObj.transform.position) * GrabbedMoveSpeed, ref velocity, smoothTime);
            }
        }
        if (!grab) NoGrab = false;
    }

    public void NoGrabby()
    {
        GrabbedObj = null;
        NoGrab = true;
    }
}
