using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class NewsPaperToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public NewsPaperSO paperSO;

    public string story;

    public TextMeshProUGUI text;

    void Start()
    {
        story = paperSO.story;
        Debug.Log("저 있어요.");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("in");
        text.text = story;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("out");
        text.text = null;
    }

}
