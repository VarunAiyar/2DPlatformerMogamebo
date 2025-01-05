using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBombTrapScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bombRB;


    // Start is called before the first frame update
    void Start()
    {
        // Ensure the Rigidbody starts asleep
        bombRB.Sleep();
    }

    private void OnTriggerEnter2D(Collider2D collidingObject)
    {
        print("This function is being called");
        if (collidingObject.gameObject.CompareTag("Player"))
        {
            // Release the Bomb
            print("Player triggered Bomb");            
            bombRB.WakeUp();
            bombRB.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
