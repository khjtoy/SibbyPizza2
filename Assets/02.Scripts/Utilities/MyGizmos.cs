using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmos : MonoBehaviour
{
    public bool visable = true;
    public Color gizmoColor = Color.yellow;
    public float radius = 0.1f;


    private void OnDrawGizmos()
    {
        if (visable)
        {
            //���� ����
            Gizmos.color = gizmoColor;

            //��ü ����� ����
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}
