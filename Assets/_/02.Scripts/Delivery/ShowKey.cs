using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowKey : MonoBehaviour
{
    [SerializeField]
    private GameObject key;


    private void OnTriggerEnter(Collider other)
    {
        ShowKeyAction();
    }

    private void OnTriggerExit(Collider other)
    {
        DisableKey();
    }

    public void ShowKeyAction()
    {
        key.SetActive(true);
    }

    public void DisableKey()
    {
        key.SetActive(false);
    }
}
