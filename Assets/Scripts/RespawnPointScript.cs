using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointScript : MonoBehaviour
{
    public static RespawnPointScript instance { get; private set; }
    public Transform respawnPointTransform;

    /// <summary>
    /// TRAPS
    /// </summary>
    [SerializeField] private List<GameObject> bombs; // BOMBS
    [SerializeField] private List<GameObject> fallingPlatforms; // FALLING PLATFORMS
    [SerializeField] private List<GameObject> rhinos; // RHINOS
    [SerializeField] private List<GameObject> buzzsaws; // BUZZSAWS


    public void RespawnPlayer(GameObject Player)
    {
        Player.transform.position = respawnPointTransform.position;

        // Reset the Bombs
        foreach (GameObject bomb in bombs)
        {
            if(bomb.GetComponent<BombScript>() != null)
            {
                bomb.GetComponent<BombScript>().RespawnBomb();
                print("All Bombs have been reset");
            }
        }

        // Reset the Falling Platforms
        foreach (GameObject fpf in fallingPlatforms)
        {
            if (fpf.GetComponent<FallingPlatformScript>() != null)
            {
                fpf.GetComponent<FallingPlatformScript>().ResetPlatform();
                print("All Falling Platforms have been reset");
            }
        }

        // Reset the Rhinos
        foreach (GameObject rhino in rhinos)
        {
            if (rhino.GetComponent<RhinoTriggerScript>() != null)
            {
                rhino.GetComponent<RhinoTriggerScript>().ResetRhino();
                print("All Rhinos have been reset");
            }
        }

        // Reset the Buzzsaws
        foreach (GameObject buzzsaw in buzzsaws)
        {
            if (buzzsaw.GetComponent<BuzzsawScript>() != null)
            {
                buzzsaw.GetComponent<BuzzsawScript>().ResetBuzzsaw();
                print("All Buzzsaws have been reset");
            }
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

}
