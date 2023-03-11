using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaPan : MonoBehaviour
{
    public PizzaManager instance;
    public string pizzaName;
    public BoxCollider2D collider2D;

    private void Start()
    {
        instance = PizzaManager.GetInstance();
        collider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(this.transform.position.y < -3f)
        {
            //pizzaName = instance.PizzaNameExport();
            Debug.Log(pizzaName);
        }
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        //this.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        
        this.transform.position = new Vector3(-2.68f, Camera.main.ScreenToWorldPoint(mousePosition).y - (collider2D.size.y), 0);
    }
}
