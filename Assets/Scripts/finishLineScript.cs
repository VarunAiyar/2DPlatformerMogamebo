using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishLineScript : MonoBehaviour
{
    public string whichLevelToLoad = "Floor1";

    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collidingObject)
    {       
        if (collidingObject.gameObject.CompareTag("Player"))
        {
            // Load next Level            
            //sceneControllerScript.instance.LoadNextLevel();
            sceneControllerScript.instance.LoadSpecificLevel(whichLevelToLoad);
            MainUIScript.instance.EnableLerp();
            MainUIScript.instance.MogameboSpeak();
        }
    }
}
