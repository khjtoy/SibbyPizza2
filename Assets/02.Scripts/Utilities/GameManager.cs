using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public List<string> IngredientName;
    public List<string> shopName;
    public List<Sprite> Icons;
    private int life = 5;

    public void DecreaseLife()
    {
        if (life <= 0) return;
        UIManager.Instance.ChangeHeart(life);
        life--;
        //if (life <= 0)
            //SceneManager.LoadScene((int)Define.Scenes.Failure - 1);
    }
}
