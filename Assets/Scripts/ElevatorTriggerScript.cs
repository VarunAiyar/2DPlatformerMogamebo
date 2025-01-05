using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTriggerScript : MonoBehaviour
{
    
    private Animator elevatorAnim;

    // Start is called before the first frame update
    void Start()
    {
        elevatorAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.gameObject.CompareTag("Player"))
        {
            elevatorAnim.SetBool("playerInsideElevatorTrigger", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            elevatorAnim.SetBool("playerInsideElevatorTrigger", false);
        }
    }
}
