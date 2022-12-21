using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculate : MonoBehaviour
{
    [SerializeField]
    private GameObject[] costObj;
    [SerializeField]
    private GameObject okButton;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        StartCoroutine("ShowCost");
    }

    private IEnumerator ShowCost()
    {
        for(int i = 0; i < costObj.Length; i++)
        {
            costObj[i].SetActive(true);
            yield return new WaitForSeconds(0.7f);
        }
        okButton.SetActive(true);
    }

    public void OkButton()
    {
        PoolManager.Instance.AllDespawn();
        TransitionManager.Instance.LoadScene((int)Define.Scenes.Day);
    }
}
