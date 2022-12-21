using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void GotoGame()
    {
        TransitionManager.Instance.LoadScene((int)Define.Scenes.Day);
        SoundManager.Instance.Stop("StartLobby");
    }

    public void GotoStart()
    {
        SceneManager.LoadScene((int)Define.Scenes.Start - 1);
    }
}
