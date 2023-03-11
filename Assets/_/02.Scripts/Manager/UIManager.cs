using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    private GameObject dontDestroyedCanvas;
    public GameObject DontDestroyedCanvas
    {
        get
        {
            return dontDestroyedCanvas;
        }
    }
    [SerializeField]
    private TextMeshProUGUI curPizzaText;
    [SerializeField]
    private Transform heartParent;
    [SerializeField]
    private Transform heartParent2;
    [SerializeField]
    private Sprite brokenHeart;
    [SerializeField]
    private Sprite brokenHeart2;

    private GameObject[] hearts;
    private GameObject[] hearts2;

    protected override void Init()
    {
        base.Init();
        CheckObject[] CheckObj = FindObjectsOfType<CheckObject>();
        var allCanvas = from canvas in CheckObj
                        where canvas.gameObject.HasComponent<Canvas>()
                        select canvas;

        if (allCanvas.Count() > 1)
        {
            foreach (CheckObject canvas in allCanvas)
            {
                if (!canvas.isCheck)
                    Destroy(canvas.gameObject);
            }
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(dontDestroyedCanvas);
        dontDestroyedCanvas.GetComponent<CheckObject>().isCheck = true;
        hearts = heartParent.AllChildrenObjArray();
        hearts2 = heartParent2.AllChildrenObjArray();
        SetPizzaText();
        SetHeart();
    }

    public void SetPizzaText()
    {
        curPizzaText.text = DataManager.Instance.CurrentUser.currentPizza;
    }

    public void ChangeHeart(int life)
    {
        hearts[5 - life].GetComponent<Image>().sprite = brokenHeart;
    }

    public void SetHeart()
    {
        for(int i = 0; i < 5 - DataManager.Instance.CurrentUser.life; i++)
        {
            hearts[i].GetComponent<Image>().sprite = brokenHeart;
        }
        for (int i = 0; i < 2 - DataManager.Instance.CurrentUser.life2; i++)
        {
            hearts2[i].GetComponent<Image>().sprite = brokenHeart2;
        }
    }

    public void ChangeHeart2(int life)
    {
        hearts2[2 - life].GetComponent<Image>().sprite = brokenHeart2;
    }
}
