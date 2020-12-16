using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleportTo;
    public Teleporter linkedTeleporter;
    public bool canTeleport = true;

    private void OnTriggerEnter(Collider col)
    {

    }
}
