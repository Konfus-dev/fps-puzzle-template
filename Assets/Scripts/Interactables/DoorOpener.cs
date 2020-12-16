using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public List<Doors> Doors;
    public Transform KeyPos;
    public string Tag;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag(Tag))
        {
            col.transform.position = KeyPos.position;
            col.transform.rotation = KeyPos.rotation;
            col.rigidbody.useGravity = false;
            col.rigidbody.isKinematic = true;
            foreach (Doors d in Doors)
            {
                d.OpenDoor();
            }
        }
        
    }
}
