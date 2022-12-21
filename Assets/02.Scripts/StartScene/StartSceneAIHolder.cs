using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneAIHolder : MonoBehaviour
{
    public int playerDir = 1;

    [SerializeField]
    private GameObject[] AIPrefab;

    private GameObject clone;


    private void Update()
    {
        if (transform.position.x < 33f && playerDir == 1)
        {
            PoolManager.Instance.Despawn(this.gameObject);
        }
        else if (transform.position.x > 55f && playerDir == -1)
        {
            PoolManager.Instance.Despawn(this.gameObject);
        }

        transform.AddPositionX(-1f * Time.deltaTime * playerDir);
    }

    void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0f, -90f * playerDir, 0f);

        for (int i = 0; i < AIPrefab.Length; i++)
        {
            AIPrefab[i].gameObject.SetActive(false);
        }
        clone = AIPrefab[Random.Range(0, AIPrefab.Length)];
        clone.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        clone.gameObject.SetActive(false);
    }
}
