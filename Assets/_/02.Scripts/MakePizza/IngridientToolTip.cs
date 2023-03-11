using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngridientToolTip : MonoBehaviour
{
    public TextMeshProUGUI nameTxt;
    private float halfWidth;
    private RectTransform rectTransform;

    private void Start()
    {
        halfWidth = GetComponentInParent<CanvasScaler>().referenceResolution.x * 0.5f;
        rectTransform = GetComponent<RectTransform>();
    }

    public void Update()
    {
        transform.position = Input.mousePosition;

        if (rectTransform.anchoredPosition.x + rectTransform.sizeDelta.x > halfWidth)
            rectTransform.pivot = new Vector2(1, 1);
        else
            rectTransform.pivot = new Vector2(0, 1);
    }
}
