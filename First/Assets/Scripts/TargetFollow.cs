using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 * Moves the game object this script is attached
 * to follow a target
 * 
 **/

public class TargetFollow : MonoBehaviour
{
    public bool isDebug;
    public Camera camera;
    public Color rayColor;
    public GameObject targetToFollow;
    public Vector3 center;
    public GameObject playerGO;
    private Vector3 distanceToTarget;
    public float cameraMoveAmount;
    [Range(1, 100)]
    public float camBoundSizeHorizontal = 100f;
    [Range(1, 100)]
    public float camBoundSizeVertical = 100f;
    public float frustumHeight;
    public float frustumWidth;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (targetToFollow != null)
        {
            center = playerGO.transform.position;
            distanceToTarget = transform.position - targetToFollow.transform.position;
            frustumHeight = 2 * distanceToTarget.magnitude * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
            frustumWidth = frustumHeight * camera.aspect;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDebug)
            Debug.DrawRay(this.transform.position, -distanceToTarget, rayColor);
        MoveCamera();
    }

    private void LateUpdate()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, center + distanceToTarget, cameraMoveAmount);
    }

    void MoveCamera()
    {
        if (playerGO.transform.position.x > (center.x + FrustumWidth / 2))
        {
            center += new Vector3(playerGO.transform.position.x - (center.x + FrustumWidth / 2), 0, 0);
        }

        if (playerGO.transform.position.x < (center.x - FrustumWidth / 2))
        {
            center += new Vector3(playerGO.transform.position.x - (center.x - FrustumWidth / 2), 0, 0);
        }

        if (playerGO.transform.position.y > (center.y + FrustumHeight / 2))
        {
            center += new Vector3(0, playerGO.transform.position.y - (center.y + FrustumHeight / 2), 0);
        }

        if (playerGO.transform.position.y < (center.y - FrustumHeight / 2))
        {
            center += new Vector3(0, playerGO.transform.position.y - (center.y - FrustumHeight / 2), 0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(center, new Vector3(FrustumWidth, FrustumHeight, 5));
    }

    public float FrustumWidth {get { return frustumWidth * (camBoundSizeHorizontal / 100f); }}
    public float FrustumHeight { get { return frustumHeight * (camBoundSizeVertical / 100f); } }
}
