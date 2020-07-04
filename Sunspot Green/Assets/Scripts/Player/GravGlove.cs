using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravGlove : MonoBehaviour
{
    public float GrabDist = 5;
    public float GrabbedMoveSpeed = 10;
    public Transform GrabPos;
    public GameObject GrabbedObj;

    private RaycastHit Hit;
    private bool NoGrab = false;
    void FixedUpdate()
    {
        bool grab = Input.GetMouseButton(0);
        if(!NoGrab)
        {
            //if holding left click down and have hit and hit has rigidbody
            if (grab && Physics.Raycast(transform.position, transform.forward, out Hit, GrabDist) && Hit.transform.GetComponent<Rigidbody>())
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
                GrabbedObj.GetComponent<Rigidbody>().velocity = Vector3.Lerp(GrabbedObj.GetComponent<Rigidbody>().velocity, (GrabPos.position - GrabbedObj.transform.position) * GrabbedMoveSpeed, Time.fixedDeltaTime * GrabbedMoveSpeed);
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
