using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoSingleton<TutorialManager>
{
    public bool firstVisit = false;

    public int daySceneVisit = 0;

    void OnEnable()
    {
        if (DataManager.Instance.CurrentUser.level == 1 && firstVisit == false)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Start":
                //Debug.Log("123");
                break;
            case "DayScene":
                daySceneVisit++;
                break;
            default:
                break;
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
