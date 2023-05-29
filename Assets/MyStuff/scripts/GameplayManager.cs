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

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        endTime = Time.time + 180;
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
        SceneManager.LoadScene("Town");
    }
}
