using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformScript : MonoBehaviour
{
    // Wiggle and Fall
    public float fallDelay = 2.0f;
    //private bool isWiggling = false;
    //public float wiggleSpeed = 5f; // The speed of wiggling
    //public float wiggleAmount = 10f; // The amount of rotation


    [SerializeField] private Rigidbody2D platformRB;

    // Original Transforms
    private Vector2 originalPos;
    private Quaternion originalRotation;

    private BoxCollider2D bottomCollider;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        originalRotation = transform.rotation;

        bottomCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private IEnumerator WiggleAndFall()
    {
        // Wiggle
        //while (isWiggling)
        //{
        //    float angle = Mathf.Sin(Time.time * wiggleSpeed) * wiggleAmount;
        //    transform.rotation = Quaternion.Euler(0, 0, angle);
        //    // Wait
        //    yield return new WaitForSeconds(fallDelay);            
        //}

        // isWiggling = false;

        // Fall
        yield return new WaitForSeconds(fallDelay);
        platformRB.bodyType = RigidbodyType2D.Dynamic;
        bottomCollider.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // isWiggling = true;
            platformRB.WakeUp();
            StartCoroutine(WiggleAndFall());
        }
        else
        {
            // If it is colliding with anything other than the Player, slowly fade and Hide
        }
    }

    public void ResetPlatform()
    {
        platformRB.bodyType = RigidbodyType2D.Static;
        platformRB.Sleep();
        transform.position = originalPos;
        transform.rotation = originalRotation;
        bottomCollider.enabled = true;

        //isWiggling = false;
    }
}
