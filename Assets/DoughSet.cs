using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoughSet : MonoBehaviour
{
    private Vector3 defaultPos;

    public void Start()
    {
        defaultPos = transform.position;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        this.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        this.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(mousePosition).x, Camera.main.ScreenToWorldPoint(mousePosition).y, -2);
    }

    private void OnMouseUp()
    {
        if ((transform.position.x < -1f && -5f < transform.position.x) && (transform.position.y < 3f && -1f < transform.position.y))
        {
            PizzaManager.GetInstance().PizzaDoughSet();
            transform.position = defaultPos;
        }
        else
        {
            transform.position = defaultPos;
        }
    }
}
