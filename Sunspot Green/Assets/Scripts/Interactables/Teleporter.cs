using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform TeleportTo;
    public Teleporter LinkedTeleporter;
    public bool CanTeleport = true;

    private GravGlove Grav;
    private void Awake()
    {
        Grav = GameObject.Find("PlayerCam").GetComponent<GravGlove>();
    }

    private void OnTriggerEnter(Collider col)
    {

        //LinkedTeleporter.CanTeleport = false;
        CanTeleport = true;
        if(CanTeleport)
        {
            if(col.CompareTag("Player"))
            {
                Vector3 enterVel = col.GetComponentInParent<Rigidbody>().velocity;
                col.transform.parent.position = new Vector3(TeleportTo.position.x, col.transform.parent.position.y, TeleportTo.position.z);
                col.transform.parent.rotation = TeleportTo.rotation;
                col.GetComponentInParent<Rigidbody>().velocity = enterVel;
            }
            else
            {
                if (col.gameObject == Grav.GrabbedObj) Grav.NoGrabby();
                Vector3 enterVel = col.GetComponent<Rigidbody>().velocity;
                col.transform.position = new Vector3(TeleportTo.position.x, col.transform.position.y, TeleportTo.position.z);
                col.transform.rotation = TeleportTo.rotation;
                col.GetComponent<Rigidbody>().velocity = enterVel;
            }
        }
    }
    /*private void OnTriggerEnter(Collision col)
    {
        LinkedTeleporter.CanTeleport = false;
        Vector3 enterVel = col.rigidbody.velocity;
        col.transform.position = TeleportTo.position;
        col.transform.rotation = TeleportTo.rotation;
        col.rigidbody.velocity = enterVel;
    }*/

   /* private void OnTriggerExit(Collider other)
    {
        CanTeleport = true;
    }*/
    /*private void OnCollisionExit(Collision collision)
    {
        
    }*/
}
