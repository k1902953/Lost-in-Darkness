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
    public bool gameover = false;      // a boolean to indicate the game over state

    public int totalbatts;       // Total number of batteries to collect
    public int battscollected = 0;   // Number of batteries collected so far

    public int totalnotes;       // Total number of papers to collect
    public int papercollected = 0;   // Number of paper collected so far
    public ProgressBar health;
    public int healthValue = 100;

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
        //FindObjectOfType<DialogueTrigger>().TriggerDialogue();
        StartCoroutine(addHealth());
        gameovertxt.text = "";
        health.BarValue = healthValue;
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

    public void EnemyAttack()
    {
        Debug.Log("ouch - player is attacked");
        healthValue = healthValue - 6;
        if (healthValue < 0) healthValue = 0;
        health.BarValue = healthValue;
        if (healthValue <= 0)
        {
            setGameOver();
        }
    }

    IEnumerator addHealth()
    {
        while (true)
        {
            if (healthValue < 100)
            {
                healthValue += 1;
                health.BarValue = healthValue;
                yield return new WaitForSeconds(1);
            }
            else
            { // if health >= 100, just yield 
                yield return null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }
}
