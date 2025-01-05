using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RhinoTriggerScript : MonoBehaviour
{
    public float rhinoSpeed = 5f;
    private Vector2 nextPointPos;
    private int pointIndex;

    public GameObject[] points; // Array of Points
    private int totalPoints;

    // Initial Transforms
    private Vector2 initialPos;
    private Quaternion initialRot;
    private int direction;

    // Reference to Rhino
    private GameObject Rhino;

    // Activation
    private bool rhinoActive;

    // Animation
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Rhino = transform.GetChild(0).gameObject;
        initialPos = Rhino.transform.position;
        initialRot = Rhino.transform.rotation;

        pointIndex = 0; // Go to Point 1 first
        nextPointPos = points[pointIndex].transform.position;
        totalPoints = points.Length;

        rhinoActive = false;

        anim = Rhino.GetComponent<Animator>(); // Get the Animator of the Rhino child Object

        direction = 1; // Start by moving towards the first point
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rhinoActive = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rhinoActive)
        {
            MoveRhino();
        }
    }

    private void MoveRhino()
    {
        // Set Animation variable to true
        anim.SetBool("isChasing", true); // Play the Bear_Run Animation

        var step = rhinoSpeed * Time.deltaTime;
        Rhino.transform.position = Vector2.MoveTowards(Rhino.transform.position, nextPointPos, step);
        //Debug.Log(string.Format("MoveRhino() called, the nextPointPos is {0}", nextPointPos));

        // If Rhino reaches the next point
        if (Vector2.Distance(Rhino.transform.position, nextPointPos) < 0.1f)
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        // Reverse direction if we reached the last or first point
        if (pointIndex >= totalPoints - 1)
        {
            direction = -1;
            //pointIndex = totalPoints - 2; // Move to the second last point
        }
        else if (pointIndex <= 0)
        {
            direction = 1;
           // pointIndex = 1; // Move to the second point
        }

        // Update the next point position
        pointIndex += direction;
        nextPointPos = points[pointIndex].transform.position;
        //Debug.Log(string.Format("PointIndex Updated. The current Point Index is {0}", pointIndex));

        // Optionally, flip the Rhino sprite based on direction
        if (direction == 1)
        {
            // Flip sprite to face right
            // Flip Sprite            
            Vector3 localScale = Rhino.transform.localScale;
            localScale.x *= -1f;
            Rhino.transform.localScale = localScale;
        }
        else
        {
            // Flip sprite to face left
            // Flip Sprite            
            Vector3 localScale = Rhino.transform.localScale;
            localScale.x *= -1f;
            Rhino.transform.localScale = localScale;
        }
    }

    public void ResetRhino()
    {
        rhinoActive = false;
        anim.SetBool("isChasing", false); // Play the Bear_Idle Animation
        Rhino.transform.position = initialPos;
        Rhino.transform.rotation = initialRot;

        // Play Idle Animation
    }
}
