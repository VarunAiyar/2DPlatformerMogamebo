using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatTrapScript : MonoBehaviour
{
    public float amplitude = 1.0f;   // Amplitude of the sine wave
    public float frequency = 1.0f;   // Frequency of the sine wave
    public float speed = 2.0f;       // Speed of the bat's horizontal movement
    public float pingPongDistance = 5.0f; // Distance travelled before ping-ponging back

    private Vector3 startPosition; // Reference to the Spawning position of the Bat
    public bool movingRight = false;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // Store the starting position
        startPosition = transform.position;

        anim = GetComponent<Animator>();

        if (movingRight)
        {
            // Flip Sprite to face the Right           
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new horizontal position
        float horizontalPosition = transform.position.x + (movingRight ? speed : -speed) * Time.deltaTime;

        // Calculate the new vertical position using sine wave
        float verticalPosition = startPosition.y + Mathf.Sin(horizontalPosition * frequency) * amplitude;

        // Update the position of the bat
        transform.position = new Vector2(horizontalPosition, verticalPosition);

        // Check if the bat should ping-pong back
        if (movingRight && horizontalPosition > startPosition.x + pingPongDistance)
        {
            movingRight = false;

            // Flip Sprite            
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        else if (!movingRight && horizontalPosition < startPosition.x - pingPongDistance)
        {
            movingRight = true;

            // Flip Sprite            
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
