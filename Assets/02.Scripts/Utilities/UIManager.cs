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
    private Sprite brokenHeart;

    private GameObject[] hearts;

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
        SetPizzaText();
    }

    public void SetPizzaText()
    {
        curPizzaText.text = DataManager.Instance.CurrentUser.currentPizza;
    }

    public void ChangeHeart(int life)
    {
        hearts[5 - life].GetComponent<Image>().sprite = brokenHeart;
    }
}
