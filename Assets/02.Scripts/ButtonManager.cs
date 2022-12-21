using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void NewGoToGame()
    {
        DataManager.Instance.CurrentUser.level = 1;
        DataManager.Instance.CurrentUser.life = 5;
        DataManager.Instance.CurrentUser.life2 = 2;
        GotoGame2();
    }
    public void GotoGame()
    {
        if (DataManager.Instance.CurrentUser.level == 1) return;
        DataManager.Instance.CurrentUser.currentGoal = 0;
        DataManager.Instance.CurrentUser.failureGoal = 0;
        DataManager.Instance.CurrentUser.scapeGoal = 0;
        DataManager.Instance.CurrentUser.currentPizza = "";
        TransitionManager.Instance.LoadScene((int)Define.Scenes.Day);
        SoundManager.Instance.Stop("StartLobby");
    }

    public void GotoGame2()
    {
        DataManager.Instance.CurrentUser.currentGoal = 0;
        DataManager.Instance.CurrentUser.failureGoal = 0;
        DataManager.Instance.CurrentUser.scapeGoal = 0;
        DataManager.Instance.CurrentUser.currentPizza = "";
        TransitionManager.Instance.LoadScene((int)Define.Scenes.Day);
        SoundManager.Instance.Stop("StartLobby");
    }

    public void GotoStart()
    {
        SceneManager.LoadScene((int)Define.Scenes.Start - 1);
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
