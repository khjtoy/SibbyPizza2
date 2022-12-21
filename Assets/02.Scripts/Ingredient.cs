using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ingredient : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string name;
    public ToopTip toolTip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTip.nameTxt.text = name;
        toolTip.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.gameObject.SetActive(false);
    }
}
