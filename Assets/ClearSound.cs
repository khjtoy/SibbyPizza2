using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClearSound : MonoBehaviour
{
    public Text canvas;
    public GameObject button;

    void Start()
    {
        //SoundManager.Instance.AllStop();
        //SoundManager.Instance.Play("GameClear");
        StartCoroutine(panelOn());

    }

    public IEnumerator panelOn()
    {
        yield return new WaitForSeconds(1f);
        canvas.DOText("thanks for playing", 5).OnComplete(() => button.SetActive(true)); ;
    }

    public void GoMain()
    {
        DataManager.Instance.CurrentUser.level = 1;
        SceneManager.LoadScene("Start");
        
    }
}
