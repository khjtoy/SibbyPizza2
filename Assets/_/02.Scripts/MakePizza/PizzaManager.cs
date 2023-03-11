using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class PizzaManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] pizzaDough = new GameObject[7];
    [SerializeField]
    private GameObject pizza;
    [SerializeField]
    private GameObject pizzaPan;
    [SerializeField]
    private GameObject brushManagerObject;
    [SerializeField]
    private TextMeshProUGUI currentText;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private RectTransform panelText;
    [SerializeField]
    private TextMeshProUGUI errorMessage;

    [SerializeField]
    private int pizzaLevelIndex;

    public static PizzaManager instance = null;

    private GameObject clone;

    public Define.PizzaTopping pizzaTopping = Define.PizzaTopping.Cheese;
    public Define.PizzaTopping currentTopping = Define.PizzaTopping.Count;
    public Color nowBrushColor = Color.red;

    public BrushManager brushManager = null;

    public List<string> pizzaName = new List<string>();
    public List<Texture2D> doughTexture = new List<Texture2D>();
    Color[] colors;

    public int equal;
    public int noEqual = 0;
    public int result;

    public int num = 0;

    public int[] pizzaToppingList;
    public string[] pizzaToopingString;
    
    private void Awake()
    {
        doughTexture.Clear();

        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        pizzaLevelIndex = 0;
        pizzaTopping = Define.PizzaTopping.Count;
        currentTopping = Define.PizzaTopping.Count -1;

        pizza = pizzaDough[pizzaLevelIndex];
        pizzaToppingList = new int[9];
        pizzaToopingString = new string[9];

        brushManager = BrushManager.GetInstance();

        brushManager.DeleteBrush();

        //Instantiate(pizza);
    }

    public static PizzaManager GetInstance()
    {
        return instance;
    }

    public void PizzaDoughSet()
    {
        if (pizzaLevelIndex != 0)
            return;

        clone = pizzaDough[0];
        clone = Instantiate(clone, pizzaPan.transform.position + new Vector3(0f, 1f, -1f), Quaternion.Euler(-180, 0, 0));

        pizzaLevelIndex++;
    }

    public void StretchPizzaDough()
    {
        if(pizzaLevelIndex >= pizzaDough.Length || pizzaLevelIndex == 0)
        {
            return;
        }

        Transform instantiatePos = pizza.transform;

        Destroy(clone);

        clone = pizzaDough[pizzaLevelIndex];

        if(pizzaLevelIndex >= pizzaDough.Length - 1)
        {
            clone = Instantiate(clone, pizzaPan.transform.position + new Vector3(0f, 1f), Quaternion.Euler(-90, 0, 0));

            pizza = clone;

            //ReadPixels();

            StartCoroutine(Delay());
        }
        else
        {
            clone = Instantiate(clone, pizzaPan.transform.position + new Vector3(0f, 0.75f, -1f), Quaternion.Euler(-180, 0, 0));

            pizza = clone;
        }

        pizzaLevelIndex++;
    }

    public void PizzaToppingAdd(string topping)
    {
        if (pizzaLevelIndex < pizzaDough.Length)
            return;

        brushManager.DeleteBrush();

        switch ((Define.PizzaTopping)System.Enum.Parse(typeof(Define.PizzaTopping), topping))
        {
            case Define.PizzaTopping.Cheese:
                ChangeTopping(Define.PizzaTopping.Cheese);
                StopCoroutine("CheckPixel");
                StartCoroutine(CheckPixel());
                brushManager.CreateBrush();
                brushManager.cheeseBrushSet();
                break;

            case Define.PizzaTopping.Tomato:
                ChangeTopping(Define.PizzaTopping.Tomato);
                StopCoroutine("CheckPixel");
                StartCoroutine(CheckPixel());
                brushManager.CreateBrush();
                brushManager.sourceBrushSet(new Color32(255, 0, 0, 255));
                break;

            case Define.PizzaTopping.Pepperoni:
                ChangeTopping(Define.PizzaTopping.Pepperoni);
                StopCoroutine("CheckPixel");
                StartCoroutine(CheckPixel(10f));
                brushManager.CreateBrush();
                brushManager.PepperoniBrushSet(new Color32(255, 0, 0, 255));
                break;

            case Define.PizzaTopping.Olive:
                ChangeTopping(Define.PizzaTopping.Olive);
                StopCoroutine("CheckPixel");
                StartCoroutine(CheckPixel(10f));
                brushManager.CreateBrush();
                brushManager.OliveBrushSet(new Color32(0, 0, 0, 255));
                break;

            case Define.PizzaTopping.Potato:
                ChangeTopping(Define.PizzaTopping.Potato);
                StopCoroutine("CheckPixel");
                StartCoroutine(CheckPixel(10f));
                brushManager.CreateBrush();
                brushManager.PotatoBrushSet(new Color32(222, 184, 135, 255));
                break;

            case Define.PizzaTopping.Shrimp:
                ChangeTopping(Define.PizzaTopping.Shrimp);
                StopCoroutine("CheckPixel");
                StartCoroutine(CheckPixel(10f));
                brushManager.CreateBrush();
                brushManager.ShrimpBrushSet(new Color32(193, 178, 157, 255));
                break;

            case Define.PizzaTopping.Pineapple:
                ChangeTopping(Define.PizzaTopping.Pineapple);
                StopCoroutine("CheckPixel");
                StartCoroutine(CheckPixel(10f));
                brushManager.CreateBrush();
                brushManager.PineappleBrushSet(new Color32(239, 204, 68, 255));
                break;

            case Define.PizzaTopping.SweetPotato:
                ChangeTopping(Define.PizzaTopping.SweetPotato);
                StopCoroutine("CheckPixel");
                StartCoroutine(CheckPixel());
                brushManager.CreateBrush();
                brushManager.SweetPotatoBrushSet(new Color32(114, 89, 111, 255));
                break;

            default:
                break;
        }
    }

    public void CheckTopping()
    {
        switch (pizzaTopping)
        {
            case Define.PizzaTopping.Cheese:

                    ChangeTopping(pizzaTopping);
                    pizzaName.Add("치즈");

                break;

            case Define.PizzaTopping.Tomato:

                    ChangeTopping(pizzaTopping);
                    pizzaName.Add("토마토");

                break;

            case Define.PizzaTopping.Pepperoni:

                    ChangeTopping(pizzaTopping);
                    pizzaName.Add("페퍼로니");

                break;

            case Define.PizzaTopping.Olive:

                    ChangeTopping(pizzaTopping);
                    pizzaName.Add("올리브");

                break;

            case Define.PizzaTopping.Potato:

                    ChangeTopping(pizzaTopping);
                    pizzaName.Add("감자");

                break;

            case Define.PizzaTopping.Shrimp:

                    ChangeTopping(pizzaTopping);
                    pizzaName.Add("새우");

                break;

            case Define.PizzaTopping.Pineapple:

                    ChangeTopping(pizzaTopping);
                    pizzaName.Add("파인애플");

                break;

            case Define.PizzaTopping.SweetPotato:

                    ChangeTopping(pizzaTopping);
                    pizzaName.Add("고구마");

                break;
        }
        
    }

    
    public void ReadPixels()
    {
        //for(int i = 0; i < doughTexture.Count; i++)
        //{
        //    colors = doughTexture[i].GetPixels(1);

        //    for(int j = 0; j < colors.Length; j++)
        //    {
        //            noEqual++;
        //    }
        //}

        //result = noEqual;
    }

    public IEnumerator CheckPixel(float percent = 400f)
    {
        if (pizzaToppingList[(int)pizzaTopping] > percent)
        {
            yield break;
        }
            int debug;
        debug = num++;   
        equal = 0;
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.2f);

        while(true)
        {
            Debug.Log(debug);
            if (pizzaToppingList[(int)pizzaTopping] > percent)
            {
                pizzaToopingString[(int)pizzaTopping] = GameManager.Instance.IngredientName[(int)pizzaTopping];
                currentText.text = SaveName();
                CheckTopping();
                break;
            }
            yield return waitForSeconds;
        }
    }

    public void ToppingUp()
    {
        pizzaToppingList[((int)pizzaTopping)]++;
    }

    public void ChangeTopping(Define.PizzaTopping ChangePizzaTopping)
    {
        currentTopping = pizzaTopping;
        pizzaTopping = ChangePizzaTopping;

        return;
    }

    public void PizzaNameExport()
    {
        string resultPizzaName = null;

        for(int i = 8; i >= 0; i--)
        {
            if (!string.IsNullOrEmpty(pizzaToopingString[i]))
            {
                resultPizzaName += pizzaToopingString[i];
            }
        }
        resultPizzaName += "피자";

        if(resultPizzaName == "피자")
        {
            errorMessage.DOFade(1, 1f).OnComplete(() =>
            {
                errorMessage.DOFade(0f, 1.5f);
            });
            return;
        }

        Debug.Log(resultPizzaName);
        DataManager.Instance.CurrentUser.currentPizza = resultPizzaName;
        UIManager.Instance.SetPizzaText();

        panel.SetActive(true);
        panelText.DOAnchorPosY(10, 1f).OnComplete(() =>
        {
            TransitionManager.Instance.LoadScene((int)Define.Scenes.Shop);
        });
    }

    public string SaveName()
    {
        string resultPizzaName = null;

        for (int i = 8; i >= 0; i--)
        {
            if (!string.IsNullOrEmpty(pizzaToopingString[i]))
            {
                resultPizzaName += pizzaToopingString[i];
            }
        }
        resultPizzaName += "피자";
        return resultPizzaName;
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < pizza.transform.childCount; i++)
        {
            if (pizza.transform.GetChild(i).gameObject.transform.GetComponent<TexturePaintTarget>() != null)
            {
                pizza.transform.GetChild(i).gameObject.transform.GetComponent<TexturePaintTarget>();
                doughTexture.Add((Texture2D)pizza.transform.GetChild(i).GetComponent<MeshRenderer>().material.GetTexture("_PaintTex"));
            }
        }
    }
}
