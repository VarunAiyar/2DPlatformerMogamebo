using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Adding the TextMeshPro Library

public class TimerScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;
    float elapsedTime;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        int hours = Mathf.FloorToInt(elapsedTime / 3600);
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60); // Modulo division (remainder)

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}
