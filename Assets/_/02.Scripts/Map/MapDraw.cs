using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDraw : MonoBehaviour
{

    [Header("MiniMap")]
    public RectTransform playerInMap;
    // 장소를 생성할 부모 오브젝트
    public GameObject place;
    public RectTransform map2dEnd;
    public GameObject placeMarkObject;
    public RectTransform shopInMap;
    public GameObject bigMap;
    public Color orginColor;
    public Color changeColor;
    [Header("BigMap")]
    public RectTransform playerInMapB;
    // 장소를 생성할 부모 오브젝트
    public GameObject placeB;
    public RectTransform shopInMapB;
    public RectTransform map2dEndB;
    public GameObject iconMarkObject;
    [Header("Map")]
    // 실제 place를 저장하고 있는 부모 오브젝트
    public GameObject realPlaceObj;
    public Transform map3dParent;
    public GameObject shopObject;
    public Transform map3dEnd;
    public float xOffset;
    public float yOffset;

    private List<GameObject> placeObjs;

    private Transform temp = null;
    private Transform tempB = null;
    [Header("ETC")]
    [SerializeField]
    private ToolTip toolTip;
    private void Start()
    {
        placeObjs = realPlaceObj.transform.AllChildrenObjList();

        foreach(GameObject placeObj in placeObjs)
        {
            RectTransform mark = Instantiate(placeMarkObject).GetComponent<RectTransform>();
            mark.SetParent(place.transform);
            ShowMarkInMap(placeObj, mark, map2dEnd);

            mark = Instantiate(iconMarkObject).GetComponent<RectTransform>();
            mark.GetComponent<Image>().sprite = GameManager.Instance.Icons[placeObj.GetComponent<RangeCheck>().ShopName - 1];
            mark.gameObject.AddComponent<Mark>();
            mark.GetComponent<Mark>().name = GameManager.Instance.shopName[placeObj.GetComponent<RangeCheck>().ShopName - 1];
            mark.GetComponent<Mark>().toolTip = toolTip;
            mark.SetParent(placeB.transform);
            ShowMarkInMap(placeObj, mark, map2dEndB);
        }
        ShowMarkInMap(shopObject, shopInMap, map2dEnd);
        ShowMarkInMap(shopObject, shopInMapB, map2dEndB);
    }

    private void Update()
    {
        ShowMarkInMap(this.gameObject, playerInMap, map2dEnd);
        ShowMarkInMap(this.gameObject, playerInMapB, map2dEndB);
        if(Input.GetKeyDown(KeyCode.M))
        {
            bigMap.SetActive(!bigMap.activeSelf);

            if(bigMap.activeSelf)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                toolTip.gameObject.SetActive(false);
            }
        }
    }

    private void ShowMarkInMap(GameObject thisObject, RectTransform objectInMap, RectTransform _map2dEnd)
    {
        Vector3 normalized, mapped;

        normalized = map3dParent.InverseTransformPoint(thisObject.transform.position).DivVec(map3dEnd.position - map3dParent.position);
        normalized.y = normalized.z;
        mapped = normalized.MulVec(_map2dEnd.localPosition);
        mapped.z = 0;
        mapped.x = mapped.x + xOffset;
        mapped.y = mapped.y + yOffset;
        objectInMap.localPosition = mapped;
    }

    public void ChangeColor(int idx)
    {
        if (temp != null)
            temp.GetComponent<Image>().color = orginColor;
        if (tempB != null)
            tempB.GetComponent<Outline>().enabled = false;

        temp = place.transform.GetChild(idx);
        tempB = placeB.transform.GetChild(idx);

        temp.GetComponent<Image>().color = changeColor;
        tempB.GetComponent<Outline>().enabled = true;
    }
}
