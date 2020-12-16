using UnityEngine;
using System.Collections;

public class Look : MonoBehaviour
{
    #region Globals
    [Header("Transforms")]
    [Tooltip("Put the player cam here")]
    public Transform cam;
    public Transform playerMesh;

    [Header("Player Look Sensitivity")]
    [Tooltip("To adjust cam x sensitivity")]
    public float xSensitivity;
    [Tooltip("To adjust cam y sensitivity")]
    public float ySensitivity;

    [Header("Player Look Smoothing")]
    [Tooltip("To adjust smoothing")]
    public float smoothTime = 0.3f;

    [Header("Cam Clamp (Max look angles)")]
    [Tooltip("Max look anlge of camera")]
    public float maxAngle;

    private Rigidbody rig;
    private Quaternion camCenter;
    private bool cursorLocked = true;
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        camCenter = cam.localRotation;
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Cursor.lockState == CursorLockMode.Locked) SetY();
        if (Cursor.lockState == CursorLockMode.Locked) SetX();
        UpdateCursorLock();
    }

    #region customFuncs
    private void SetY()
    {
        float input = Input.GetAxisRaw("Mouse Y") * ySensitivity * Time.fixedDeltaTime;
        Quaternion adj = Quaternion.AngleAxis(input, -Vector3.right);
        Quaternion delta = cam.localRotation * adj;

        if (Quaternion.Angle(camCenter, delta) <= maxAngle)
        {
            cam.localRotation = delta;
        }
        else return;
    }

    private void SetX()
    {
        float input = Input.GetAxisRaw("Mouse X") * xSensitivity * Time.fixedDeltaTime;
        Quaternion adj = Quaternion.AngleAxis(input, Vector3.up);
        Quaternion delta = rig.rotation * adj;
        rig.rotation = delta;
    }

    private void UpdateCursorLock()
    {
        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLocked = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLocked = true;
            }
        }
    }
    #endregion
}
