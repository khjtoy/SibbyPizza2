using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        Destroy(DayManager.Instance.DirectionLight.gameObject);
        Destroy(DayManager.Instance.gameObject);
        Destroy(UIManager.Instance.DontDestroyedCanvas.gameObject);
        Destroy(UIManager.Instance.gameObject);
        StartCoroutine("ShowCost");
        costObj[0].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{DataManager.Instance.CurrentUser.currentGoal}";
        costObj[1].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{DataManager.Instance.CurrentUser.failureGoal}";

        if (DataManager.Instance.CurrentUser.scapeGoal == 0)
        {
            costObj[2].transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.red;
            costObj[2].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "실패";
        }
        else
        {
            costObj[2].transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.blue;
            costObj[2].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "성공";
        }

        DataManager.Instance.CurrentUser.level++;
        DataManager.Instance.CurrentUser.currentGoal = 0;
        DataManager.Instance.CurrentUser.failureGoal = 0;
        DataManager.Instance.CurrentUser.scapeGoal = 0;
    }

    private IEnumerator ShowCost()
    {
        for(int i = 0; i < costObj.Length; i++)
        {
            costObj[i].SetActive(true);
            yield return new WaitForSeconds(0.7f);
        }
        //okButton.SetActive(true);
    }

    public void OkButton()
    {
        if(PoolManager.IsInstantiated)
            PoolManager.Instance.AllDespawn();
        TransitionManager.Instance.LoadScene((int)Define.Scenes.Day);
    }
}
