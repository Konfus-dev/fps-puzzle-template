using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    #region globals
    public List<Doors> doors;
    public Transform keyPos;
    public string correctKeyTag;
    #endregion

    //NOTE: this is quick and dirty code, 
    //in the future I would likely re do this with tweening
    //as well as make it more re-usable. 

    #region monobehavior
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag(correctKeyTag))
        {
            col.transform.position = keyPos.position;
            col.transform.rotation = keyPos.rotation;
            col.rigidbody.useGravity = false;
            col.rigidbody.isKinematic = true;
            foreach (Doors d in doors)
            {
                d.OpenDoor();
            }
        }
        
    }
    #endregion
}
