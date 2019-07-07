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
    private Vector3 distanceToTarget;
    public float cameraMoveAmount;
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

        this.transform.position = Vector3.MoveTowards(this.transform.position, this.targetToFollow.transform.position + distanceToTarget, cameraMoveAmount);
    }
}
