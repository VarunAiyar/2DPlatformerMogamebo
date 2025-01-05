using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuzzsawScript : MonoBehaviour
{
    public float sawSpeed = 5f;
    private Vector2 nextPointPos;
    private int pointIndex;

    public float rotationSpeed = 500.0f; // Speed of rotation around the pivot
    public float rotationDirection = -1f;


    public GameObject[] points; // Array of Points
    private int totalPoints;

    // Initial Transforms
    private Vector2 initialPos;
    private Quaternion initialRot;
    private int direction;


    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;

        pointIndex = 1;
        nextPointPos = points[pointIndex].transform.position;
        totalPoints = points.Length;
    }

    // Update is called once per frame
    void Update()
    {
        var step = sawSpeed * Time.deltaTime;
        // Move Buzzsaw
        transform.position = Vector2.MoveTowards(transform.position, nextPointPos, step);

        // Spin Buzzsaw
        // Rotate around its own axis
        transform.Rotate(0, 0, rotationDirection * rotationSpeed * Time.deltaTime);

        // If Buzzsaw reaches Next Point
        if (transform.position == points[pointIndex].transform.position)
        {
            ChangeDirection();

        }
    }

    private void ChangeDirection()
    {
        // Last Point reached, reverse direction
        if (pointIndex == totalPoints - 1) 
        {
            direction = -1;        
        }

        // First Point reached, reverse direction
        if (pointIndex == 0)
        {
            direction = 1;
        }

        pointIndex += direction; // This determines the next point position
        // Update nextPointPos
        nextPointPos = points[pointIndex].transform.position;
    }

    public void ResetBuzzsaw()
    {
        transform.position = initialPos;
        transform.rotation = initialRot;
    }
}
