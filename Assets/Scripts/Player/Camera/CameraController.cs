using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    #region Globals
    [Tooltip("To adjust cam x sensitivity")]
    public float xSensitivity;
    [Tooltip("To adjust cam y sensitivity")]
    public float ySensitivity;
    [Tooltip("Max look anlge of camera")]
    public float maxAngle;

    private Player player;
    private Quaternion camCenter;
    #endregion

    #region monobehavior
    private void Start()
    {
        player = this.transform.GetComponent<Player>();
        camCenter = player.cam.transform.localRotation;
    }

    private void FixedUpdate()
    {
        SetY();
        SetX();
    }
    #endregion

    #region private methods
    private void SetY()
    {
        float inputY = player.input.GetPlayerMouseDelta().y * ySensitivity * Time.fixedDeltaTime;
        Quaternion adj = Quaternion.AngleAxis(inputY, -Vector3.right);
        Quaternion delta = player.cam.transform.localRotation * adj;

        if (Quaternion.Angle(camCenter, delta) <= maxAngle)
        {
            player.cam.transform.localRotation = delta;
        }
        else return;
    }

    private void SetX()
    {
        float inputX = player.input.GetPlayerMouseDelta().x * xSensitivity * Time.fixedDeltaTime;
        Quaternion adj = Quaternion.AngleAxis(inputX, Vector3.up);
        Quaternion delta = player.rb.rotation * adj;
        player.rb.rotation = delta;
    }
    #endregion
}
