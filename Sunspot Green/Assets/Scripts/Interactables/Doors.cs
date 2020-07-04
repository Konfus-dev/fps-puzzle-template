using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{

    public Transform MoveTo;

    private bool IsOpening = false;
    private void FixedUpdate()
    {
        if (IsOpening) LerpDoor();
    }

    public void OpenDoor()
    {
        IsOpening = true;
    }

    private void LerpDoor()
    {
        transform.position = Vector3.Lerp(transform.position, MoveTo.position, Time.fixedDeltaTime * 2f);
        if (transform.position == MoveTo.position) IsOpening = false;
    }
}
