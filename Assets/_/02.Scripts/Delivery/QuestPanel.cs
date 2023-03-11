using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class QuestPanel : MonoBehaviour
{
    private bool isDown = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            isDown = !isDown;
            transform.DOMoveY(isDown ? -666f : -20, 1f);
        }
    }
}
