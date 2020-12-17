using UnityEngine;

public class HeadBob : MonoBehaviour
{
    #region globals
    [Tooltip("How fast camera bobs during basic movement")]
    public float baseBobSpeed = 14f;
    [Tooltip("How fast camera bobs during sprinting")]
    public float sprintBobSpeed = 14f;
    [Tooltip("How much camera bobs during sprinting")]
    public float sprintBobAmount = 0.05f;
    [Tooltip("How much camera bobs during walking")]
    public float walkBobAmount = 0.05f;

    private Player player;
    private float defaultPosY = 0;
    private float timer = 0;
    #endregion

    #region monobehavior
    void Start()
    {
        player = this.transform.parent.GetComponent<Player>();
        defaultPosY = transform.localPosition.y;
    }

    private void Update()
    {
        BobCamera();
    }
    #endregion

    #region private methods
    private void BobCamera()
    {
        if (player.state == Player.State.Walking)
        {
            timer += Time.deltaTime * baseBobSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * walkBobAmount, transform.localPosition.z);
        }
        else if (player.state == Player.State.Sprinting)
        {
            timer += Time.deltaTime * sprintBobSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * sprintBobAmount, transform.localPosition.z);
        }
        else
        {
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * baseBobSpeed), transform.localPosition.z);
        }
    }
    #endregion
}

