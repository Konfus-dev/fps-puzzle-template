using UnityEngine;
using System.Collections;

public class Look : MonoBehaviour
{
    #region Globals
    [Header("Transforms")]
    [Tooltip("Put the player cam here")]
    public Transform Cam;
    public Transform CamBody;
    public Transform PlayerMesh;

    [Header("Player Look Sensitivity")]
    [Tooltip("To adjust cam x sensitivity")]
    public float xSensitivity;
    [Tooltip("To adjust cam y sensitivity")]
    public float ySensitivity;

    [Header("Player Look Smoothing")]
    [Tooltip("To adjust smoothing")]
    public float SmoothTime = 0.3f;

    [Header("Cam Clamp (Max look angles)")]
    [Tooltip("Max look anlge of camera")]
    public float MaxAngle;

    private Transform Player;
    private Rigidbody Rig;
    private bool CursorLocked = true;
    private Quaternion CamCenter;
    private Vector3 velocity = Vector3.zero;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        CamCenter = Cam.localRotation;
        Player = transform;
        Rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Quaternion startRotPlayer = Player.localRotation;
        Quaternion startRotCam = Cam.localRotation;
        Vector3 startPosCam = CamBody.position;
        if (Cursor.lockState == CursorLockMode.Locked) SetY(startRotCam);
        if (Cursor.lockState == CursorLockMode.Locked) SetX(startRotPlayer);
        FollowPlayer(startPosCam);
        UpdateCursorLock();
    }

    #region customFuncs
    void SetY(Quaternion startRot)
    {
        float input = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
        Quaternion adj = Quaternion.AngleAxis(input, -Vector3.right);
        Quaternion delta = Cam.localRotation * adj;

        if (Quaternion.Angle(CamCenter, delta) < MaxAngle)
        {
            Cam.localRotation = Quaternion.Slerp(startRot, delta, Time.deltaTime * 30);
        }

    }

    void SetX(Quaternion startRot)
    {
        float input = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        Quaternion adj = Quaternion.AngleAxis(input, Vector3.up);
        Quaternion delta = Player.localRotation * adj;
        Player.localRotation = Quaternion.Lerp(startRot, delta, Time.deltaTime * 30);
        CamBody.localRotation = Quaternion.Lerp(startRot, delta, Time.deltaTime * 30);
        //Rig.rotation = Quaternion.Slerp(startRot, delta, Time.deltaTime * 30);
    }

    void FollowPlayer(Vector3 startPos)
    {
        // update position
        Vector3 targetPosition = Player.position;
        CamBody.position = Vector3.SmoothDamp(startPos, targetPosition, ref velocity, SmoothTime);
        //CamBody.position = Vector3.Lerp(startPos, targetPosition, Time.deltaTime * 30);
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
