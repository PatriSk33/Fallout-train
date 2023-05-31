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


    public int goingToIndex, fromIndex; // Scene indexes

    private float TimeInGame = 300;  //Max amount 5 - SpeedOfTrain ( -amount of vagons(how many and how much is in them) + power of motor + skill of train driver )

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
        if(endTime < Time.time)
        {
            end = true;
        }
        if (end)
        {
            continueToTown.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ContinueToTown()
    {
        SceneManager.LoadScene(goingToIndex);
    }
}
