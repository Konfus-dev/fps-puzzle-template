using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    #region Globals
    [Header("Transforms")]
    [Tooltip("Put the player cam here")]
    public Transform Cam;

    [Header("Player Look Sensitivity")]
    [Tooltip("To adjust cam x sensitivity")]
    public float xSensitivity;
    [Tooltip("To adjust cam y sensitivity")]
    public float ySensitivity;

    [Header("Cam Clamp (Max look angles)")]
    [Tooltip("Max look anlge of camera")]
    public float MaxAngle;

    private Transform Player;
    private bool CursorLocked = true;
    private Quaternion CamCenter;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        CamCenter = Cam.localRotation;
        Player = transform;
    }

    // Update is called once per frame
    void Update()
    {
        SetY();
        SetX();
        UpdateCursorLock();
    }

    #region customFuncs
    void SetY()
    {
        float input = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
        Quaternion adj = Quaternion.AngleAxis(input, -Vector3.right);
        Quaternion delta = Cam.localRotation * adj;

        if (Quaternion.Angle(CamCenter, delta) < MaxAngle)
        {
            Cam.localRotation = Quaternion.Lerp(Cam.localRotation, delta, Time.deltaTime * 30);

        }

    }

    void SetX()
    {
        float input = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        Quaternion adj = Quaternion.AngleAxis(input, Vector3.up);
        Quaternion delta = Player.localRotation * adj;
        Player.localRotation = Quaternion.Lerp(Player.localRotation, delta, Time.deltaTime * 30); ;
    }

    void UpdateCursorLock()
    {
        if (CursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CursorLocked = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CursorLocked = true;
            }
        }
    }
    #endregion
}
