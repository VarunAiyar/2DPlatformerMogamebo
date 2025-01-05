using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleFadeInWallScript : MonoBehaviour
{
    [SerializeField] private float maxDistance = 5f; // The distance the Player should be at for the wall to be fully visible
    
    private SpriteRenderer wallSprite; // The Wall Sprite
    private Coroutine fadeCoroutine; // Reference to the active fade Coroutine
    [SerializeField] private float fadeInSpeed = 1f;
    [SerializeField] private float fadeOutSpeed = 1.25f;

    private bool playerInTrigger = false;


    // Start is called before the first frame update
    void Start()
    {
        // Get the Wall Sprite
        wallSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        // Init the Wall to be fully transparent
        wallSprite.color = new Color(wallSprite.color.r, wallSprite.color.g, wallSprite.color.b, 0f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the Player enters the Trigger
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInTrigger = true;

            // Stop any ongoing fade Coroutines
            if (fadeCoroutine != null) 
                StopCoroutine(fadeCoroutine);
            // Start fading in the Wall
            wallSprite.color = new Color(wallSprite.color.r, wallSprite.color.g, wallSprite.color.b, 0f);
            fadeCoroutine = StartCoroutine(FadeIn());
        }
    }

    // OnTriggerStay2D is called every frame as long as the Player is in the Trigger, so it kinda works like Update()
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (!playerInTrigger) { return; }

    //    // If the Player stays in the Trigger
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        // Calculate the distance between the Player and the Wall
    //        float distance = Vector2.Distance(collision.transform.position, transform.GetChild(0).position);
    //        // Calculate the alpha of the Sprite based on distance and maxDistance
    //        float alpha = Mathf.Clamp01(1 - distance / maxDistance);
    //        // Update the wall's color with new alpha value
    //        //  wallSprite.color = new Color(wallSprite.color.r, wallSprite.color.g, wallSprite.color.b, Mathf.Lerp(wallSprite.color.a, alpha, Time.deltaTime * 5f));
    //        wallSprite.color = new Color(wallSprite.color.r, wallSprite.color.g, wallSprite.color.b, alpha);

    //    }
    //}

    //private void Update()
    //{
    //    if (!playerInTrigger) { return; }

    //    if (playerInTrigger)
    //    {
    //        print("googoogeegee");
    //        // Calculate the distance between the Player and the Wall
    //        float distance = Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.GetChild(0).position);
    //        // Calculate the alpha of the Sprite based on distance and maxDistance
    //        float alpha = Mathf.Clamp01(1 - distance / maxDistance);
    //        // Update the wall's color with new alpha value
    //        //  wallSprite.color = new Color(wallSprite.color.r, wallSprite.color.g, wallSprite.color.b, Mathf.Lerp(wallSprite.color.a, alpha, Time.deltaTime * 5f));
    //        wallSprite.color = new Color(wallSprite.color.r, wallSprite.color.g, wallSprite.color.b, alpha);
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If the Player exits the Trigger
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInTrigger = false;

            // Stop any ongoing fade coroutine
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);
            // Start fading out the wall
            wallSprite.color = new Color(wallSprite.color.r, wallSprite.color.g, wallSprite.color.b, 1f);
            fadeCoroutine = StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeIn()
    {
        // Gradually increase the alpha value to fade in the wall
        while (wallSprite.color.a < 1f || (playerInTrigger == true))
        {            
            //wallSprite.color = new Color(wallSprite.color.r, wallSprite.color.g, wallSprite.color.b, Mathf.MoveTowards(wallSprite.color.a, 1f, Time.deltaTime * fadeInSpeed));
            wallSprite.color = new Color(wallSprite.color.r, wallSprite.color.g, wallSprite.color.b, wallSprite.color.a + Time.deltaTime * fadeInSpeed);
            yield return null; // Wait for the next frame
        }
    }

    private IEnumerator FadeOut()
    {
        // Gradually decrease the alpha value to fade out the wall
        while (wallSprite.color.a > 0f || (playerInTrigger == false))
        {
            // wallSprite.color = new Color(wallSprite.color.r, wallSprite.color.g, wallSprite.color.b, Mathf.MoveTowards(wallSprite.color.a, 0f, Time.deltaTime * fadeOutSpeed)); // Faster fade out
            wallSprite.color = new Color(wallSprite.color.r, wallSprite.color.g, wallSprite.color.b, wallSprite.color.a - Time.deltaTime * fadeOutSpeed); // Faster fade out
            yield return null; // Wait for the next frame
        }
    }

}
