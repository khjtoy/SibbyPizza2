using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private RectTransform paritcle;
    [SerializeField]
    private RectTransform pos;
    // Start is called before the first frame update
    void Start()
    {
        paritcle.anchoredPosition = pos.anchoredPosition;
    }
}
