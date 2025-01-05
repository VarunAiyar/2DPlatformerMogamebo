using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MissileLauncherScript : MonoBehaviour
{
    public float raycastInterval = 0.1f; // Interval between raycasts
    public float lockOnTime = 2f; // Time required to lock onto the player
    public float fireRate = 1f; // Rate of missile firing
    public GameObject missilePrefab; // Prefab of the missile
    public Transform spawnPoint; // Spawn point of the missile
    public ParticleSystem explosionEffect; // Particle effect for missile explosion
    public string groundTag = "Tag_Ground"; // Tag for ground objects
    public float missileSpeed = 10f; // Speed of the missile
    public float backToIdleTime = 0.5f; // Time to rotate back to Idle Position
    public float backToLockedInTime = 0.5f; // Time to rotate back to LockedIn Position
    public GameObject idleTargetPosition; // Because nig transform right isn't working

    private Transform player; // Reference to the player
    private bool isLockedOn = false; // Flag to check if the launcher is locked onto the player
    private float lockOnTimer = 0f; // Timer to track lock-on time
    private float fireTimer = 0f; // Timer to track missile firing rate
    private Vector3 initialPosition; // Initial position of the launcher
    private Quaternion initialRotation; // Initial rotation of the launcher

    private bool reached = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player
        initialPosition = transform.position; // Store the initial position
        initialRotation = transform.rotation; // Store the initial rotation
        StartCoroutine(RaycastToPlayer()); // Start the raycasting coroutine
    }

    // Update is called once per frame
    void Update()
    {
        if (isLockedOn)
        {
            fireTimer += Time.deltaTime; // Increment the fire timer
            if (fireTimer >= fireRate && reached == true)
            {
                FireMissile(player.position); // Fire a missile at the player's position
                fireTimer = 0f; // Reset the fire timer
            }
            // RotateTowards(player.position); // Rotate the launcher towards the player
            StopCoroutine(RotateTowardsCoroutine(player.position));
            StartCoroutine(RotateTowardsCoroutine(player.position));
        }
        else
        {
            lockOnTimer += Time.deltaTime; // Increment the lock-on timer
            if (lockOnTimer >= lockOnTime)
            {
                // ReturnToIdle(); // Return to the idle state
                StartCoroutine(CoroutineReturnToIdle());
            }
        }

    }

    IEnumerator RaycastToPlayer()
    {
        while (true)
        {
            // RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, Mathf.Infinity, LayerMask.GetMask(groundTag)); // We're not using Layer Masks

            RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, Mathf.Infinity);
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                lockOnTimer = 0f; // Reset the lock-on timer
                isLockedOn = true; // Set the locked-on flag
            }
            else
            {
                isLockedOn = false; // Clear the locked-on flag
            }
            yield return new WaitForSeconds(raycastInterval); // Wait for the next raycast
        }
    }

    //void FireMissile(Vector3 targetPosition)
    //{
    //    GameObject missile = Instantiate(missilePrefab, spawnPoint.position, Quaternion.identity); // Instantiate the missile
    //    missile.GetComponent<MissileScript>().Initialize(targetPosition, missileSpeed, explosionEffect); // Initialize the missile
    //}

    void FireMissile(Vector3 targetPosition)
    {
        // Calculate the direction to the target
        Vector3 fireDirection = (targetPosition - spawnPoint.position).normalized;
        Quaternion missileRotation = Quaternion.LookRotation(Vector3.forward, fireDirection);

        // Instantiate missile with the correct rotation
        GameObject missile = Instantiate(missilePrefab, spawnPoint.position, missileRotation);
        missile.GetComponent<MissileScript>().Initialize(targetPosition, missileSpeed, explosionEffect);
    }

    //void RotateTowards(Vector3 targetPosition)
    //{
    //    Vector3 direction = targetPosition - transform.position; // Calculate the direction to the target
    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Calculate the angle
    //    transform.rotation = Quaternion.Euler(0, 0, angle); // Rotate the launcher
    //}

    IEnumerator RotateTowardsCoroutine(Vector3 targetPosition)
    {
        Quaternion initialRotation = transform.rotation;
        Vector3 direction = targetPosition - transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        float elapsedTime = 0f;
        float totalRotationTime = backToLockedInTime; // Time to complete the rotation

        while (elapsedTime < totalRotationTime)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, (elapsedTime / totalRotationTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation; // Ensure exact rotation at the end

        if (transform.rotation == targetRotation)
        {
            reached = true;
        }
    }

    //void ReturnToIdle()
    //{
    //    transform.position = initialPosition; // Reset the position
    //    transform.rotation = initialRotation; // Reset the rotation
    //    fireTimer += Time.deltaTime; // Increment the fire timer
    //    if (fireTimer >= fireRate)
    //    {
    //        FireMissile(transform.right * 10); // Fire a missile straight ahead
    //        fireTimer = 0f; // Reset the fire timer
    //    }
    //}

    IEnumerator CoroutineReturnToIdle()
    {
        float elapsedTime = 0f;
        float totalRotationTime = backToIdleTime; // Time it takes to return to idle

        Vector3 startingPosition = transform.position; // Store the starting position
        Quaternion startingRotation = transform.rotation; // Store the starting rotation

        while (elapsedTime < totalRotationTime)
        {
            transform.position = Vector3.Lerp(startingPosition, initialPosition, (elapsedTime / totalRotationTime)); // Smoothly move to initial position
            transform.rotation = Quaternion.Slerp(startingRotation, initialRotation, (elapsedTime / totalRotationTime)); // Smoothly rotate to initial rotation

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = initialPosition; // Ensure exact position
        transform.rotation = initialRotation; // Ensure exact rotation

        fireTimer += Time.deltaTime; // Increment the fire timer
        if (fireTimer >= fireRate)
        {
            FireMissile(idleTargetPosition.transform.position); // Fire a missile straight ahead
            fireTimer = 0f; // Reset the fire timer
        }
    }
}
