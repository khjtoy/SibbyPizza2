using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Calculate : MonoBehaviour
{
    [SerializeField]
    private GameObject[] costObj;
    [SerializeField]
    private GameObject okButton;
    [SerializeField]
    private TextMeshProUGUI day;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        day.text = $"{DataManager.Instance.CurrentUser.level}일차";
        Destroy(DayManager.Instance.DirectionLight.gameObject);
        Destroy(DayManager.Instance.gameObject);
        Destroy(UIManager.Instance.DontDestroyedCanvas.gameObject);
        if (DataManager.Instance.CurrentUser.scapeGoal == 0)
        {
            costObj[2].transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.red;
            costObj[2].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "실패";
            GameManager.Instance.DecreaseLife2();
        }
        else
        {
            costObj[2].transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.blue;
            costObj[2].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "성공";
        }

        Destroy(UIManager.Instance.gameObject);
        StartCoroutine("ShowCost");
        costObj[0].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{DataManager.Instance.CurrentUser.currentGoal}";
        costObj[1].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{DataManager.Instance.CurrentUser.failureGoal}";


        DataManager.Instance.CurrentUser.level++;
        DataManager.Instance.CurrentUser.currentGoal = 0;
        DataManager.Instance.CurrentUser.failureGoal = 0;
        DataManager.Instance.CurrentUser.scapeGoal = 0;
        DataManager.Instance.CurrentUser.currentPizza = "";
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
        if (DataManager.Instance.CurrentUser.life2 <= 0)
        {
            DataManager.Instance.CurrentUser.level = 1;
            SceneManager.LoadScene((int)Define.Scenes.Failure - 1);
            return;
        }
        if (DataManager.Instance.CurrentUser.level >= 8)
        {
            SceneManager.LoadScene((int)Define.Scenes.Clear - 1);
            return;
        }
        TransitionManager.Instance.LoadScene((int)Define.Scenes.Day);
    }
}
