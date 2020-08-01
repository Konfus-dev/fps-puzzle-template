using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggers : MonoBehaviour
{
    public bool isTriggered;

    private void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggered = false;
    }
}
