using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCheck : MonoBehaviour
{
    private bool isGoal = false;
    public bool IsGoal
    {
        get
        {
            return isGoal;
        }
        set
        {
            isGoal = value;
        }
    }

    [SerializeField]
    private bool changeScene = false;
    public bool ChangeScene
    {
        get
        {
            return changeScene;
        }
    }

    [SerializeField]
    private Define.Scenes sceneName = Define.Scenes.None;
    [SerializeField]
    private Define.Shop shopName = Define.Shop.None;

    public int SceneName
    {
        get
        {
            return (int)sceneName;
        }
    }

    public int ShopName
    {
        get
        {
            return (int)shopName;
        }
    }
}
