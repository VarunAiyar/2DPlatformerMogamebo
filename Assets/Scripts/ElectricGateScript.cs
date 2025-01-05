using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricGateScript : MonoBehaviour
{
    private bool gateActive;
    public float activeTime = 3.0f;
    public float inactiveTime = 3.0f;

    public int gateOrFireChildNumber = 2;

    [SerializeField] private BoxCollider2D sparkCollider;


    // Start is called before the first frame update
    void Start()
    {
        gateActive = true;
        StartCoroutine(ActivateGate());
    }
    
    public IEnumerator ActivateGate()
    {
        gateActive = true;
        if (gateActive)
        {
            // Turn on the Collider Collision
            sparkCollider.enabled = true;
            SpriteRenderer sparks = transform.GetChild(gateOrFireChildNumber).GetComponent<SpriteRenderer>();
            sparks.enabled = true;
            //sparks.color = Color.red;
            yield return new WaitForSeconds(activeTime);
            StartCoroutine(DisableGate());
        }
    }

    public IEnumerator DisableGate()
    {
        gateActive = false;
        // Turn off the Collider Collision
        sparkCollider.enabled = false;
        SpriteRenderer sparks = transform.GetChild(gateOrFireChildNumber).GetComponent<SpriteRenderer>();
        sparks.enabled = false;
        //sparks.color = Color.white; 
        yield return new WaitForSeconds(inactiveTime);
        StartCoroutine(ActivateGate());
    }
}
