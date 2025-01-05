using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    // Explosion Particle Effect
    [SerializeField] private GameObject bombEffectPrefab;

    // Original Transforms
    private Vector2 initialPos;
    private Quaternion initialRot;

    private Rigidbody2D bombRB;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;

        bombRB = GetComponent<Rigidbody2D>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Play Exploding Effect
        GameObject explosion = Instantiate(bombEffectPrefab, transform.position, Quaternion.identity);

        // Destroy the Bomb

        // Playerasangati collide kellari original positionari vachka
        // Ani khanche Objectasangati collide kellari destroy zaunka
        // Hmm, zalari problem ha ki zari Playeran bomb avoid karnapade vingad Objectan mellari, Bomb reset karuk padtale, mhalari destroy kornaye

        if (collision.gameObject.CompareTag("Player"))
        {
            ResetBomb();
        }
        else
        {
            gameObject.SetActive(false);            
        }




        //Destroy(gameObject);
    }

    public void ResetBomb()
    {
        transform.position = initialPos;
        transform.rotation = initialRot;
        bombRB.bodyType = RigidbodyType2D.Static;
        bombRB.Sleep();
    }

    public void RespawnBomb()
    {
        print("Active Gooberrrrr");
        gameObject.SetActive(true);
        transform.position = initialPos;
        transform.rotation = initialRot;
        bombRB.bodyType = RigidbodyType2D.Static;
        bombRB.Sleep();
    }
}
