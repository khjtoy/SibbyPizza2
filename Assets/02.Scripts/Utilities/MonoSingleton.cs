using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    protected bool hasInstace = false;
    private static T _instance = null;
    public static bool IsInstantiated
    {
        get
        {
            return _instance != null;
        }
    }
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                T instance = GameObject.FindObjectOfType<T>() as T;
                if (instance == null)
                {
                    instance = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
                    instance = instance.CreateObject() as T;
                }
                else
                {
                    create_instance(instance);
                    
                }
                Debug.Assert(_instance != null, "failed create instance of " + typeof(T).ToString());
            }
            return _instance;
        }
    }

    private static void create_instance(Object instance)
    {
        _instance = instance as T;
        _instance.Init();
    }

    void Awake()
    {
        create_instance(this);
    }

    protected virtual void Init()
    {
        DontDestroyOnLoad(_instance);
        if (FindObjectsOfType<T>().Length > 1 && !hasInstace)
        {
            Destroy(gameObject);
        }
        hasInstace = true;
    }

    protected virtual Object CreateObject()
    {
        return new Object();
    }

    protected virtual void OnDestroy()
    {
        _instance = null;
    }

    private void OnApplicationQuit()
    {
        _instance = null;
    }
}