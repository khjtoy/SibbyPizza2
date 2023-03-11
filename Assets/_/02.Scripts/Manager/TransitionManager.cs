using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoSingleton<TransitionManager>
{
    [SerializeField]
    private Animator transitionAnimator;

    private bool isClose = true;

    private bool ChangeObject = false;

    protected override void Init()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    protected override Object CreateObject()
    {
        //GameObject temp = instance as GameObject;
        Object Instance = Instantiate(Resources.Load<TransitionManager>("TransitionUI"));
        Destroy(this.gameObject);
        return Instance;
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        GetComponent<Animator>().Play("OpenAnimation");
        isClose = !isClose;
    }

    public void LoadScene(int sceneID)
    {
        //Debug.Log("z");
        if (sceneID <= 0) return;
        GetComponent<Animator>().SetInteger("SceneID", sceneID - 1);
        GetComponent<Animator>().Play("TransitionAnimation");
        /*        if (isClose)
                    GetComponent<Animator>().Play("TransitionAnimation");
                else
                    GetComponent<Animator>().Play("OpenAnimation");

                isClose = !isClose;*/
    }
}
