using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Plunger : MonoBehaviour
{
    public PizzaManager instance;
    public bool upAndDonw;

    private void Awake()
    {
        instance = PizzaManager.GetInstance();
    }

    private void Start()
    {
        upAndDonw = true;
    }

    private void Update()
    {
        if(upAndDonw)
        {
            if(this.transform.position.y < -2.0f)
            {
                upAndDonw = false;
                PizzaManager.GetInstance().StretchPizzaDough();
            }
        }
        else
        {
            if(this.transform.position.y > .5f)
            {
                upAndDonw = true;
                PizzaManager.GetInstance().StretchPizzaDough();
            }
        }
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        //this.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        this.transform.position = new Vector3(-2.68f, Camera.main.ScreenToWorldPoint(mousePosition).y, -3);
    }

    
}

