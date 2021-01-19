using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text batterytxt;        // Text field for the batteries collected
    public TMP_Text papertxt;          // Text field for the papers collected
    public TMP_Text gameovertxt;       // Text field for game over text
    public TMP_Text healthtxt;         // Text field for the health text

    public int health = 100;           // the player health
    public bool gameover = false;     // a boolean to indicate the game over state

    public int totalbatts;       // Total number of batteries to collect
    public int battscollected = 0;   // Number of batteries collected so far

    public int totalnotes;       // Total number of papers to collect
    public int papercollected = 0;   // Number of paper collected so far

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    // GameManager instance
    private static GameManager instance = null;

    void Awake()
    {
        if (instance)
        {
            Debug.Log("already an instance so destroying new one");
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        gameovertxt.text = "";
        setHealthText(health);
        totalnotes = GameObject.FindObjectsOfType<PickupNotesTrigger>().Length;
        totalbatts = GameObject.FindObjectsOfType<PickupBattery>().Length;
        papertxt.text = "Notes: " + papercollected + "/" + totalnotes;
        batterytxt.text = "Battries: " + battscollected + "/" + totalbatts;
    }

    public void pickupnote()
    {
        // Check that not game over
        if (gameover) return;

        // Add one to total number of keys collected
        papercollected++;

        // Update the text field 
        papertxt.text = "Notes: " + papercollected + "/" + totalnotes;
    }

    public void pickupbatt()
    {
        // Check that not game over
        if (gameover) return;

        // Add one to total number of keys collected
        battscollected++;

        // Update the text field 
        batterytxt.text = "Battries: " + battscollected + "/" + totalbatts;
    }

    // goalreached() should set the gameover to true, and set the game over
    // text to "GAME OVER!"
    public void goalreached()
    {
        setGameOver();
    }

    // Is it game over
    public bool isGameOver()
    {
        return gameover;
    }

    // Set the game over state to true
    public void setGameOver()
    {
        if (gameover == false)
        {
            gameover = true;
            gameovertxt.text = "GAME OVER!";
        }
    }

    // Set the health text
    public void setHealthText(int health)
    {
        healthtxt.text = "Health: " + health;
    }


    // Update is called once per frame
    void Update()
    {
        
       
    }
}
