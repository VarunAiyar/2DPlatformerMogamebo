using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
    Transform targetTransform;
    Vector3 cameraVelocity = Vector3.zero;

    [Range(0f, 1f)]
    public float smoothTime;

    public Vector3 positionOffset;

    [Header("Axis Limitations")]
    public float xMin; // X Axis Vector Position Clamp for the Camera
    public float xMax;
    public float yMin; // Y Axis "
    public float yMax;

    private void Awake()
    {
        // Get the Player's current Transform
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // LateUpdate is also called every frame like Update, but it is called after all Update calls have been made
    // Therefore, it is used for calculations that are dependent on values derived from an Update call
    private void LateUpdate()
    {
        Vector3 cameraPosition = targetTransform.position + positionOffset;
        // transform here refers to the transform of the Game Object this Script is attached to (The Camera)
        cameraPosition = new Vector3(Mathf.Clamp(cameraPosition.x, xMin, xMax), Mathf.Clamp(cameraPosition.y, yMin, yMax), -10);
        transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref cameraVelocity, smoothTime); // Smooths the Vector value from current to target based on ref and smoothTime
    }
}
