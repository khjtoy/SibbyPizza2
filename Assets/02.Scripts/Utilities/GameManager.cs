using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public List<string> IngredientName;
    public List<string> shopName;
    public List<Sprite> Icons;

    public void DecreaseLife()
    {
        if (DataManager.Instance.CurrentUser.life <= 0) return;
        UIManager.Instance.ChangeHeart(DataManager.Instance.CurrentUser.life);
        DataManager.Instance.CurrentUser.life--;
        if (DataManager.Instance.CurrentUser.life <= 0)
            SceneManager.LoadScene((int)Define.Scenes.Failure - 1);
    }

    public void DecreaseLife2()
    {
        if (DataManager.Instance.CurrentUser.life2 <= 0) return;
        UIManager.Instance.ChangeHeart2(DataManager.Instance.CurrentUser.life2);
        DataManager.Instance.CurrentUser.life2--;
    }
}
