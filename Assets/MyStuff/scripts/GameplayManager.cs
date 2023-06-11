using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;

    private float endTime;
    public bool end;
    public Button continueToTown;
    public Text continueToTownText;

    //Fail
    public bool failed;
    public Text failText;


    public int goingToIndex, fromIndex; // Scene indexes

    private float TimeInGame = 300;  //Max amount 5 - SpeedOfTruck ( -amount of vagons(how many and how much is in them) + power of motor + skill of truck driver )

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        endTime = Time.time + TimeInGame;
    }

    void Update()
    {
        if(endTime < Time.time && !end)
        {
            end = true;
            continueToTown.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        if (end && !failed)
        {
            failText.text = "Won, Go to town!";
        }
    }

    public void ContinueToTown()
    {
        SceneManager.LoadScene(goingToIndex);
    }

    public void Lost()
    {
        failed = true;
        end = true;
        continueToTownText.text = "Back to Town";
        failText.text = "Mission failed!";
    }
}
