using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void NewGoToGame()
    {
        DataManager.Instance.CurrentUser.level = 1;
        GotoGame2();
    }
    public void GotoGame()
    {
        if (DataManager.Instance.CurrentUser.level == 1) return;
        TransitionManager.Instance.LoadScene((int)Define.Scenes.Day);
        SoundManager.Instance.Stop("StartLobby");
    }

    public void GotoGame2()
    {
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
        Application.Quit(); // ���ø����̼� ����
#endif
    }
}
