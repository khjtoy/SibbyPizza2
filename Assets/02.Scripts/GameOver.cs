using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup panel;

    private void Start()
    {
        if (DayManager.IsInstantiated)
        {
            Destroy(DayManager.Instance.DirectionLight.gameObject);
            Destroy(DayManager.Instance.gameObject);
        }
        if (UIManager.IsInstantiated)
        {
            Destroy(UIManager.Instance.DontDestroyedCanvas.gameObject);
            Destroy(UIManager.Instance.gameObject);
        }
        StartCoroutine(GameOverAction());
    }

    private IEnumerator GameOverAction()
    {
        yield return new WaitForSeconds(2.5f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        panel.DOFade(1, 1);
    }
}
