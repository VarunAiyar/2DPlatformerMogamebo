using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    private Vector3 targetPosition; // Target position for the missile
    private float speed; // Speed of the missile
    private ParticleSystem explosionEffect; // Particle effect for missile explosion

    public float explosionRadius = 2f; // Radius for area of effect
    private GameObject playerRef; // Reference to the Player

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    public void Initialize(Vector3 targetPosition, float speed, ParticleSystem explosionEffect)
    {
        this.targetPosition = targetPosition; // Set the target position
        this.speed = speed; // Set the speed
        this.explosionEffect = explosionEffect; // Set the explosion effect

        RotateTowards(targetPosition); // Set initial rotation
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime); // Move the missile
        if (transform.position == targetPosition)
        {
            Explode(); // Explode if the missile reaches the target            
        }
    }

    void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Tag_Ground"))
        {            
            Explode(); // Explode on collision with player or ground
        }
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity); // Instantiate the explosion effect
        Destroy(gameObject); // Destroy the missile
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
