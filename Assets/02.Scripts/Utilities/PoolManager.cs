using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool = 2;
    public bool shouldExpand = true;
}
public class PoolManager : MonoSingleton<PoolManager>
{
    public List<ObjectPoolItem> itemsToPool;
    private List<List<GameObject>> pooledObjectsList;
    public List<GameObject> changeParent;
    public List<GameObject> pooledObjects;
    private List<int> positions;
    void Awake()
    {
        pooledObjectsList = new List<List<GameObject>>();
        pooledObjects = new List<GameObject>();
        changeParent = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            pooledObjects = new List<GameObject>();
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                obj.transform.SetParent(this.transform);
                pooledObjects.Add(obj);
            }
            pooledObjectsList.Add(pooledObjects);
        }

        positions = new List<int>();
        for (int i = 0; i < pooledObjectsList.Count; i++)
        {
            positions.Add(-1);
        }

        transform.rotation = Quaternion.Euler(45, 0, 0);
    }

    public GameObject GetPooledObject(int index)
    {
        int curSize = pooledObjectsList[index].Count;

        if (curSize == 1)
        {
            if (!pooledObjectsList[index][0].activeInHierarchy)
            {
                return pooledObjectsList[index][0];
            }
        }

        for (int i = positions[index] + 1; i < positions[index] + pooledObjectsList[index].Count; i++)
        {

            if (!pooledObjectsList[index][i % curSize].activeInHierarchy)
            {
                positions[index] = i % curSize;
                return pooledObjectsList[index][i % curSize];
            }
        }

        if (itemsToPool[index].shouldExpand)
        {

            GameObject obj = (GameObject)Instantiate(itemsToPool[index].objectToPool);
            obj.SetActive(false);
            obj.transform.parent = this.transform;
            pooledObjectsList[index].Add(obj);
            return obj;

        }
        return null;
    }

    public void ChangeParent(GameObject target)
    {
        changeParent.Add(target);
    }

    public List<GameObject> GetAllPooledObjects(int index)
    {
        return pooledObjectsList[index];
    }

    public void Despawn(GameObject target)
    {
        target.SetActive(false);
        if (target.transform.parent != this.transform)
        {
            target.transform.SetParent(this.transform);
            changeParent.RemoveAt(changeParent.FindIndex(x => x == target));
        }
    }

    public void AllDespawn()
    {
        if (pooledObjectsList.Count == 0) return;

        foreach(GameObject obj in changeParent)
        {
            obj.transform.SetParent(this.transform);
        }
        changeParent.Clear();

        for(int i = 0; i < pooledObjectsList.Count; i++)
        {
            for(int j = 0; j < pooledObjectsList[i].Count; j++)
            {
                pooledObjectsList[i][j].SetActive(false);
            }
        }
    }
}

