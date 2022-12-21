using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Icon : MonoBehaviour, IPointerClickHandler
{
    private MapDraw mapDraw;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        mapDraw = GameObject.FindGameObjectWithTag("Player").GetComponent<MapDraw>();
        Size();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        mapDraw.ChangeColor(transform.GetSiblingIndex());
    }

    private void Size()
    {
        rectTransform.DOScale(1.3f, 1f).OnComplete(() =>
        {
            rectTransform.DOScale(1, 1f).OnComplete(() =>
            {
                Size();
            });
        });
    }
}
