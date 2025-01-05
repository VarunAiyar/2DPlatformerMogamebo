using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Adding Scene Management Libraries

public class MainMenuScript : MonoBehaviour
{
    public void PlayGameClicked()
    {
        SceneManager.LoadSceneAsync(1);
        if (MainUIScript.instance != null)
        {
            MainUIScript.instance.DestroySelf();
        }
    }
}
