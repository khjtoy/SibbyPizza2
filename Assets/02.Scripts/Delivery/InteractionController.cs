using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class InteractionController : MonoBehaviour
{
    [SerializeField]
    private GameObject pizzaBox;
    [SerializeField]
    private GameObject goal = null;
    [SerializeField]
    private TextMeshProUGUI warningText;

    [SerializeField]
    private Animator transitionAnimator;

    [SerializeField]
    private ParticleSystem uiParticle;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && goal != null)
        {
            RangeCheck goalCheck = goal.GetComponent<RangeCheck>();
            if (goalCheck.ChangeScene)
            {
                //transitionAnimator.Play("TransitionAnimation");
                
                if(goalCheck.SceneName == (int)Define.Scenes.Shop)
                {
                   List<GameObject> reticles = PoolManager.Instance.GetAllPooledObjects((int)Define.PoolObject.PredictReticle);
                    foreach(GameObject reticle in reticles)
                    {
                        PoolManager.Instance.Despawn(reticle);
                    }
                }
                if(goalCheck.SceneName == (int)Define.Scenes.Street)
                {
                    DeliveryManager.Instance.PredictPos(true);
                }
                
                TransitionManager.Instance.LoadScene(goalCheck.SceneName);
            }
            else
            {
                if (DataManager.Instance.CurrentUser.currentPizza != "")
                {
                    Instantiate(pizzaBox, goal.transform.position, Quaternion.identity);
                }
                if (DeliveryManager.Instance.HasGoal(goalCheck.ShopName))
                {
                    if ((int)DeliveryManager.Instance.currentAnswer.place != goalCheck.ShopName)
                    {
                        Debug.Log("목표 장소!");
                        uiParticle.Emit(10);
                    }
                }
                else
                {
                    Debug.Log("목표 장소가 아님");
                    warningText.DOFade(1, 1f).OnComplete(() =>
                    {
                        warningText.DOFade(0f, 1.5f);
                    });
                }
                DataManager.Instance.CurrentUser.currentPizza = "";
                UIManager.Instance.SetPizzaText();
                DeliveryManager.Instance.DisablePredictReticle();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Range"))
        {
            Debug.Log("Enter");
            goal = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Range") && goal != null)
        {
            Debug.Log("Exit");
            goal = null;
        }
    }
}
