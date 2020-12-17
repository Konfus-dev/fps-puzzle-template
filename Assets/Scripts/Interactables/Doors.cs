using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    #region globals
    public Transform openPos;
    private bool isOpening = false;
    #endregion

    //NOTE: this is quick and dirty code, 
    //in the future I would likely re do this with tweening
    //as well as make it more re-usable. 

    #region monobehavior
    private void FixedUpdate()
    {
        if (isOpening) LerpDoor();
    }
    #endregion

    #region public methods
    public void OpenDoor()
    {
        isOpening = true;
    }
    #endregion

    #region private methods
    private void LerpDoor()
    {
        transform.position = Vector3.Lerp(transform.position, openPos.position, Time.fixedDeltaTime * 2f);
        if (Vector3.Distance(transform.position, openPos.position) < 0.1f) isOpening = false;
    }
    #endregion
}
