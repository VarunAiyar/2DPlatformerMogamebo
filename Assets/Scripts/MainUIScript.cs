using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Adding Scene Management libraries to get current Scene Number

public class MainUIScript : MonoBehaviour
{
    public static MainUIScript instance { get; private set; } 

    [SerializeField]
    [Header("Death Counter")]
    private TextMeshProUGUI deathCounterText;
    private int deathCount = 0;

    [SerializeField]
    [Header("Floor Counter")]
    private TextMeshProUGUI floorCounterText;

    [SerializeField]
    [Header("Give Up Button")]
    private Button giveUpButton;

    [SerializeField]
    [Header("Restart Button")]
    private Button restartButton;

    [Header("Progress Bar")]
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TextMeshProUGUI progressText;
    private float lerpFactor = 0.5f;
    private float lerpDuration = 2f;
    public float previousProgress = 0;
    public float currentProgress = 0;
    private bool canLerpNow = false; // Boolean to enable Lerping

    [Header("Mogamebo Text")]
    [SerializeField] private TextMeshProUGUI mogameboText;





    private void Awake()
    {
        // If the old Script instance already exists and it's not this new instance, destroy the new instance
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
            instance = this; // Set the instance variable to this Script instance
            giveUpButton.enabled = true;
            //restartButton.enabled = true;

        // Make this Game Object persistent across Scenes
        DontDestroyOnLoad(gameObject); // DontDestroyOnLoad() prevents the GameObject from being Destroyed when a new Scene is loaded
    }

    // Start is called before the first frame update
    void Start()
    {
        MogameboSpeak();
    }

    // Update is called once per frame
    void Update()
    {
        deathCounterText.text = string.Format("{0} Deaths", deathCount);
        floorCounterText.text = string.Format("Floor {0}", GetCurrentLevel());

        //progressSlider.value = 1;
        previousProgress = (float)((GetCurrentLevel() - 1) * 1) / (SceneManager.sceneCountInBuildSettings - 1);
        //currentProgress = (float)(GetCurrentLevel() / (SceneManager.sceneCountInBuildSettings - 1));

        // Update lerpFactor over time
        if (lerpFactor >= 0 && canLerpNow)
        {
            //lerpFactor += Time.deltaTime / lerpDuration;
            lerpFactor += Time.deltaTime; //0.1f;
            lerpFactor = Mathf.Clamp01(lerpFactor);
        }

        // Update the Slider Progress value and Completion Text       
        //progressSlider.value = Mathf.Lerp(previousProgress, currentProgress, lerpFactor);
        progressSlider.value = previousProgress;
        progressText.text = string.Format("{0}% DONE", Mathf.FloorToInt(progressSlider.value * 100));

        // Debug logs to check values
        //Debug.Log($"Previous Progress: {previousProgress}");
        //Debug.Log($"Current Progress: {currentProgress}");
        //Debug.Log($"Lerp Factor: {lerpFactor}");
        //Debug.Log($"Slider Value: {progressSlider.value}");

        // Optional: Reset lerpFactor if necessary
        if (lerpFactor >= 1.0f)
        {
            lerpFactor = 0f;
            canLerpNow = false;
        }

        // Remove Main UI when Give Up Level or Game Complete Level is reached
        //if (SceneManager.GetActiveScene().buildIndex == 2)
        //{
        //    Destroy(gameObject); // Or make it inactive instead of destroying
        //}

        // Update Mogamebo Text
    }

    public int AddDeath()
    {
        deathCount++;
        //MogameboCommentOnDeath();
        return deathCount;
    }

    public int GetCurrentLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        return currentLevel;
    }

    public void GiveUpButtonClicked()
    {
        SceneManager.LoadSceneAsync("Main Menu"); // Insert Give Up Level Build Index        

    }

    public void RestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart Level
    }

    public void EnableLerp()
    {
       canLerpNow = true;
    }

    // Mogamebo Dialogues
    public void MogameboSpeak() // Dialogues when certain Floors/Scenes begin
    {
        //if (SceneManager.GetActiveScene().buildIndex == 2)
        //{
        //    mogameboText.text = string.Format("I am Mo-Game-Bo. Welcome to secret lair.");

        //}

        int level = SceneManager.GetActiveScene().buildIndex;

        switch (level)
        {
            case 1:
                mogameboText.text = string.Format("Ah, you came back. Are you sure you want to go through all that again?");
                return;
            case 2:
                mogameboText.text = string.Format("I am Mo-Game-Bo. Welcome to my secret lair.");
                return;
            case 3:
                mogameboText.text = "Pressing Space quicker makes you jump higher. Try it.";
                return;
            case 4:
                mogameboText.text = "You're just a pawn in my grand scheme.";
                return;
            case 5:
                mogameboText.text = "Every step you take, my traps are waiting.";
                return;
            case 6:
                mogameboText.text = "You won't get past this without a scratch.";
                return;
            case 7:
                mogameboText.text = "The deeper you go, the darker it gets.";
                return;
            case 8:
                mogameboText.text = "I relish your struggle. Can you feel it?";
                return;
            case 9:
                mogameboText.text = "Each level is a piece of my masterpiece of torment.";
                return;
            case 10:
                mogameboText.text = "Don't worry, it only gets harder from here.";
                return;
            case 11:
                mogameboText.text = "You've made it this far, but it's far from over.";
                return;
            case 12:
                mogameboText.text = "Despair, mortal! Your journey is futile.";
                return;
            case 13:
                mogameboText.text = "Can you feel the walls closing in? Good.";
                return;
            case 14:
                mogameboText.text = "Every failure makes me stronger. Every pain, my pleasure.";
                return;
            case 15:
                mogameboText.text = "How long until you give up?";
                return;
            case 16:
                mogameboText.text = "Your persistence is pathetic. Yet, amusing.";
                return;
            case 17:
                mogameboText.text = "The end is near, but so is your doom.";
                return;
            case 18:
                mogameboText.text = "Each level was designed for your ultimate suffering.";
                return;
            case 19:
                mogameboText.text = "I have all the time in the world. You do not.";
                return;
            case 20:
                mogameboText.text = "Welcome to the final trial. Here, you will meet your end.";
                return;
            default:
                mogameboText.text = "Ah, a new test subject. This should be fun.";
                return;
        }
    }

    public void MogameboCommentOnDeath()
    {
        int randomInt = Random.Range(0, 10); // Assuming 10 different comments
        switch (randomInt)
        {
            case 0:
                mogameboText.text = "Haha, you call that an effort?";
                break;
            case 1:
                mogameboText.text = "Pathetic! Try harder next time.";
                break;
            case 2:
                mogameboText.text = "Is that all you've got?";
                break;
            case 3:
                mogameboText.text = "Splat!";
                break;
            case 4:
                mogameboText.text = "Ouch!";
                break;
            case 5:
                mogameboText.text = "You fell for that? How amusing!";
                break;
            case 6:
                mogameboText.text = "Another one bites the dust!";
                break;
            case 7:
                mogameboText.text = "You're making this too easy!";
                break;
            case 8:
                mogameboText.text = "Haha! Try harder.";
                break;
            case 9:
                mogameboText.text = "Keep trying, it’s entertaining watching you fail.";
                break;
            default:
                mogameboText.text = "Press that button down there. It hands out free cookies.";
                break;
        }
    }


    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
