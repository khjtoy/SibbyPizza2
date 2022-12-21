using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class DeliveryManager : MonoSingleton<DeliveryManager>
{
    [SerializeField]
    private Transform DeliveryPos;
    [SerializeField]
    private AnswerSO[] answers;

    [SerializeField]
    private Transform content;
    public int worldCount;


    private List<GameObject> pos = new List<GameObject>();
    
    private Dictionary<int, GameObject> currentDeliveyPos = new Dictionary<int, GameObject>();

    GameObject reticle = null;

    private int currentOrder;

    public int[] count;

    private bool isCheck = false;

    public AnswerSO currentAnswer;
    public bool answerCheck = false;

    public Transform GetDeliveryPos
    {
        get
        {
            return DeliveryPos;
        }
    }
    protected override void Init()
    {
        base.Init();
        if(currentAnswer == null)
        {
            currentAnswer = answers[DataManager.Instance.CurrentUser.level - 1];
        }
        currentDeliveyPos[(int)currentAnswer.place] = new GameObject();
    }


    private void Start()
    {
        DontDestroyOnLoad(DeliveryPos);

        pos = DeliveryPos.AllChildrenObjList();

        /*        Debug.Log(pos.Count);
                for (int placeIdx = 0; placeIdx < pos.Count; placeIdx++)
                {
                    placeDic[pos[placeIdx].GetComponent<RangeCheck>().ShopName] = pos[placeIdx];
                }*/


        //PredictPos();
        //PredictPos(true);
    }

    public void RandomPos()
    {
        Transform reticlePos = pos.GetRandomItem().transform;
        reticlePos.GetComponent<RangeCheck>().IsGoal = true;

        reticle = PoolManager.Instance.GetPooledObject((int)Define.PoolObject.Reticle);
        reticle.transform.position = reticlePos.position;
        reticle.transform.rotation = Quaternion.Euler(0, 0, 0);
        reticle.SetActive(true);
    }

    public void PredictPos(bool must = false)
    {
        if (currentOrder <= 0) return;
        if (currentOrder >= 2 && !must) return;
        if (SceneManager.GetActiveScene().buildIndex == (int)Define.Scenes.Shop - 1 && !must) return;

        foreach(GameObject deliveyPos in pos)
        {
            reticle = PoolManager.Instance.GetPooledObject((int)Define.PoolObject.PredictReticle);
            if (reticle == null || deliveyPos == null) break;
            reticle.transform.position = deliveyPos.transform.position;
            reticle.transform.rotation = Quaternion.Euler(0, 0, 0);
            reticle.SetActive(true);
        }
    }

    public void DisablePredictReticle()
    {
        if (currentOrder > 0) return;
        List<GameObject> predictReticleList = PoolManager.Instance.GetAllPooledObjects((int)Define.PoolObject.PredictReticle);

        foreach(GameObject disableReticle in predictReticleList)
        {
            PoolManager.Instance.Despawn(disableReticle);
        }
    }

    public void AddContent(string time)
    {
        if (isCheck) return;

        if(currentOrder >= 4)
        {
            currentDeliveyPos.Remove(content.GetChild(0).GetComponent<Quest>().value);
            content.GetChild(0).GetComponent<Image>().color = new Color(1, 0, 0, 0.9f);
            StartCoroutine(DisableContent(time));
            return;
        }

        currentOrder++;

        count.Shuffle();

        int random = UnityEngine.Random.Range(1, worldCount + 1);

        int[] temp = new int[random];

        for(int i = 0; i < random; i++)
        {
            temp[i] = count[i];
        }

        // 내림차순으로 정렬
        Array.Sort(temp);
        Array.Reverse(temp);

        // 이름 짓기
        string pizzaName = string.Empty;
        for(int i = 0; i < temp.Length; i++)
        {
            pizzaName += GameManager.Instance.IngredientName[temp[i]];
        }
        pizzaName += "피자";

        int place = UnityEngine.Random.Range(1, (int)Define.Shop.COUNT);

        while(currentDeliveyPos.ContainsKey(place))
        {
            place = UnityEngine.Random.Range(1, (int)Define.Shop.COUNT);
        }

        GameObject order = PoolManager.Instance.GetPooledObject((int)Define.PoolObject.OrderImage);
        if (order == null) return;
        order.GetComponent<RectTransform>().SetParent(content);
        PoolManager.Instance.ChangeParent(order);
        order.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
        order.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"주문시간:{time}";
        order.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"주문내용:{pizzaName}";
        order.transform.GetChild(2).GetComponent<Image>().sprite = GameManager.Instance.Icons[place - 1];
        order.GetComponent<Quest>().value = place;
        order.GetComponent<Quest>().orderPizza = pizzaName;
        order.SetActive(true);
        PredictPos();
        currentDeliveyPos[place] = order; 
    }

    public void AnswerContent()
    {
        if (currentOrder >= 4)
        {
            currentDeliveyPos.Remove(content.GetChild(0).GetComponent<Quest>().value);
            content.GetChild(0).GetComponent<Image>().color = new Color(1, 0, 0, 0.9f);
            StartCoroutine(DisableContent("",false));
            return;
        }

        GameObject order = PoolManager.Instance.GetPooledObject((int)Define.PoolObject.OrderImage);
        if (order == null) return;
        order.GetComponent<RectTransform>().SetParent(content);
        PoolManager.Instance.ChangeParent(order);
        order.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);

        string time = $"{currentAnswer.time}:00";
        time = time.PadLeft(5, '0');

        order.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"주문시간:{time}";
        order.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"주문내용:{currentAnswer.pizza}";
        order.transform.GetChild(2).GetComponent<Image>().sprite = GameManager.Instance.Icons[(int)currentAnswer.place - 1];
        order.GetComponent<Quest>().value = (int)currentAnswer.place;
        order.GetComponent<Quest>().orderPizza = currentAnswer.pizza;
        order.SetActive(true);
        PredictPos();
        currentDeliveyPos[(int)currentAnswer.place] = order;
    }

    public bool HasGoal(int deliveryIdx)
    {

        if (!DayManager.Instance.isCheck)
        {
            if (deliveryIdx == (int)currentAnswer.place)
            {
                if (currentAnswer.time - DayManager.Instance.Time == 1 && currentAnswer.pizza == DataManager.Instance.CurrentUser.currentPizza)
                {
                    answerCheck = true;
                    return true;
                }
                else
                    return false;
            }
        } 

        if(currentDeliveyPos.ContainsKey(deliveryIdx) && currentDeliveyPos[deliveryIdx].GetComponent<Quest>().orderPizza == DataManager.Instance.CurrentUser.currentPizza)
        {
            currentDeliveyPos[deliveryIdx].GetComponent<Image>().color = new Color(0.5f, 1, 0, 0.9f);
            StartCoroutine(SucessContent(deliveryIdx));
            return true;
        }
        return false;
/*        if(currentDeliveyPos.ContainsKey(deliveryIdx) && currentDeliveyPos[deliveryIdx] == 1)
        {
            currentDeliveyPos[deliveryIdx] = 0;
            return true;
        }*/
    }

    public void DisableReticle()
    {
        PoolManager.Instance.Despawn(reticle);
    }

    public IEnumerator DisableContent(string time, bool isSpawn = true)
    {
        DataManager.Instance.CurrentUser.failureGoal++;
        yield return new WaitForSeconds(0.3f);
        content.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0.9f);
        GameManager.Instance.DecreaseLife();
        PoolManager.Instance.Despawn(content.GetChild(0).gameObject);
        currentOrder--;
        if (isSpawn)
            AddContent(time);
        else
            AnswerContent();
    }

    public IEnumerator SucessContent(int deliveryIdx)
    {
        DataManager.Instance.CurrentUser.currentGoal++;
        isCheck = true;
        currentOrder--;
        yield return new WaitForSeconds(0.3f);
        currentDeliveyPos[deliveryIdx].GetComponent<Image>().color = new Color(1, 1, 1, 0.9f);
        PoolManager.Instance.Despawn(currentDeliveyPos[deliveryIdx]);
        currentDeliveyPos.Remove(deliveryIdx);
        isCheck = false;
    }
}
