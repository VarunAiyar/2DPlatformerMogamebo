using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; // Adding Scene Management Libraries

public class sceneControllerScript : MonoBehaviour
{
    public static sceneControllerScript instance { get; private set; } // Get an instance of this Script to be accessible in any other script

    private void Awake() // Awake is built-in and is called when the script instance is being loaded
    {
        //print("Goober");
        if (instance == null) // If there is no Scene Controller present in the Current Scene
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else // If a Scene Controller Object is already present in the Current Scene
        {
            Destroy(gameObject);
        }
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            print("Next Level Loaded. Current Level: " + SceneManager.GetActiveScene().buildIndex.ToString());
        }
        else
        {
            print("Next Level not present in Build Settings");
        }

    }

    public void LoadSpecificLevel(string sceneName)
    {
        Debug.Log("Attempting to load scene: " + sceneName);

        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadSceneAsync(sceneName);
            Debug.Log("Loading scene: " + sceneName);
        }
        else
        {
            Debug.Log("Scene " + sceneName + " cannot be loaded. Please check if it is added to the build settings.");
        }
    }
}
