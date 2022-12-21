using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaShopSound : MonoBehaviour
{
    private void Awake()
    {
    }

    private void Start()
    {
        SoundManager.Instance.AllStop();
        SoundManager.Instance.Play("PizzaShop");
    }
}
