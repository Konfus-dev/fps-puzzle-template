using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravGlove : MonoBehaviour
{
    public float grabDist = 5;
    public float grabbedMoveSpeed = 10;
    public float smoothTime = 0.3F;

    public Transform cam;
    public GameObject grabbedObj;

    private RaycastHit hit;

    private void FixedUpdate()
    {
        bool grab = Input.GetMouseButton(0);

        if (grab)
        {
            Vector3 velocity = Vector3.zero;

            Vector3 castPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.2f);

            //if holding left click down and have hit and hit has rigidbody
            if (grab && Physics.Raycast(castPos, transform.forward, out hit, grabDist) && hit.transform.GetComponent<Rigidbody>())
            {
                if (grabbedObj == null) grabbedObj = hit.transform.gameObject;
            }
            else if (!grab)
            {
                grabbedObj = null;
            }

            //manipulate grabbed thing
            if (grabbedObj)
            {
                grabbedObj.GetComponent<Rigidbody>().velocity =
                    Vector3.SmoothDamp(grabbedObj.GetComponent<Rigidbody>().velocity, ((cam.position + cam.up * .1f + cam.forward * 1.8f) - grabbedObj.transform.position) * grabbedMoveSpeed, ref velocity, smoothTime);
            }
        }
        else grabbedObj = null;
    }
}
