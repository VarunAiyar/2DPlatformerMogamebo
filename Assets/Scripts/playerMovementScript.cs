using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public class playerMovementScript : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private float Move;

    public float playerMovementSpeed;
    public float jumpSpeed;
    public float airControlFactor = 0.5f;
    private bool doubleJump = true;

    // private bool isGrounded; // For Method 1

    // Method 2: Raycast Ground Check
    public Vector2 boxSize;
    public float castDistance; // Length of the Raycast Ray
    public LayerMask groundLayer; // Layer Mask to refer to the Ground
    private bool isGrounded;

    // Animation
    private Animator anim;

    // Flip Sprite
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();

        isFacingRight = true;

    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxisRaw("Horizontal"); // Whether Player wants to move to the left (-) or right (+)
        Vector2 moveVelocity = playerRigidBody.velocity;

        if (CheckIfGrounded()) // If Player is on Ground
        {
            //playerRigidBody.velocity = new Vector2(Move * playerMovementSpeed, playerRigidBody.velocity.y); // Setting Player's RigidBody2D's velocity

            moveVelocity.x = Move * playerMovementSpeed; // Horizontal Velocity = Horizontal Input * Movement Speed
            
        }
        else // If Player is not on Ground
        {
            moveVelocity.x += Move * playerMovementSpeed * airControlFactor * Time.deltaTime; // Apply Air Control Factor to Horizontal Input when Player is in the Air
            moveVelocity.x = Mathf.Clamp(moveVelocity.x, -playerMovementSpeed, playerMovementSpeed); // Clamp Mid Air Horizontal Speed to max Movement Speed
        }

        playerRigidBody.velocity = moveVelocity; // Setting Player's RigidBody2D's velocity to moveVelocity

        // Jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (CheckIfGrounded())
            {
                // playerRigidBody.AddForce(new Vector2(playerRigidBody.velocity.x, jumpSpeed * 10));

                // New
                playerRigidBody.AddForce(new Vector2(playerRigidBody.velocity.x * Move * 5f, jumpSpeed * 10));
            }
            else if (doubleJump)
            {
                playerRigidBody.AddForce(new Vector2(playerRigidBody.velocity.x * Move * 5f, jumpSpeed * 10));
                doubleJump = false;
            }
            
        }

        // Animation Transitions
        if (Move != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else 
        {
            anim.SetBool("isRunning", false);
        }

        anim.SetBool("isJumping", !CheckIfGrounded());

        // Flip Sprite
        if (!isFacingRight && Move > 0)
        {
            Flip();
        }
        else if (isFacingRight && Move < 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        // Flip Sprite
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    // Method 1: Checking Up Normal for Collision (doesn't work for sloped surfaces)
    //private void OnCollisionEnter2D(Collision2D collidingObject)
    //{
    //    if (collidingObject.gameObject.CompareTag("Tag_Ground"))
    //    {
    //        Vector3 normal = collidingObject.GetContact(0).normal;
    //        if (normal == Vector3.up)
    //        { 
    //            isGrounded = true;
    //        }
    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collidingObject)
    //{
    //    if (collidingObject.gameObject.CompareTag("Tag_Ground"))
    //    {
    //        isGrounded = false;
    //    }
    //}

    public bool CheckIfGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer)) // Raycasting to check whether the ray from the Player hits an Object with the groundLayer Layer Mask
        {
            doubleJump = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    // Collision with Trap
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Tag_Trap"))
        {
            // Call the Spawn Blood Effect function
            BloodEffectController.instance.SpawnBloodEffect();

            // Add Death to UI Counter
            MainUIScript.instance.AddDeath();
           // MainUIScript.instance.MogameboCommentOnDeath();

            // Respawn the Player
            RespawnPointScript.instance.RespawnPlayer(gameObject);
           
            
     
        }
    }

}
