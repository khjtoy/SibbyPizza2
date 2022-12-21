using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.VFX;

using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;


public class NewspaperControlller : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> scapeSprites;
    [SerializeField]
    private List<Sprite> prologueSprites;
    [SerializeField]
    private List<GameObject> newsPaperSprites;
    [SerializeField]
    private TextMeshProUGUI dayText;
    [SerializeField]
    private CanvasGroup news;
    [SerializeField]
    private VisualEffect warpVFX;
    [SerializeField]
    private Image scapeImage;
    [SerializeField]
    private Image prologueImage;
    [SerializeField]
    private RectTransform[] moveButtons;

    private bool warpActive = true;
    private Text text;
    public float rate = 0.02f;

    public VolumeProfile mVolumeProfile;
    private Bloom mBloom;

    int idx;
    int day;
    public int current = 0;
    private bool showPlologue = false;


    private void Awake()
    {
        SoundManager.Instance.AllStop();
        for (int i = 0; i < mVolumeProfile.components.Count; i++)
        {
            if (mVolumeProfile.components[i].name == "Bloom")
            {
                mBloom = (Bloom)mVolumeProfile.components[i];
            }
        }

        MinFloatParameter intensity = mBloom.intensity;
        intensity.value = 15f;
    }
     private void Start()
    {
        
        SoundManager.Instance.Play("DayScene");
        day = DataManager.Instance.CurrentUser.level;
        idx = prologueSprites.Count;
        warpVFX.Stop();
        warpVFX.SetFloat("WarpAmount", 0);
    }

    private void Update()
    {
        if(!showPlologue)
            Prologue();
    }

    public void ScapeMode()
    {

        news.DOFade(0, 0.7f).OnComplete(() =>
        {
            StartCoroutine(ActivateParticles());
        });
    }

    IEnumerator ActivateParticles()
    {
        if(warpActive)
        {
            warpVFX.Play();

            float amount = warpVFX.GetFloat("WarpAmount");
            while(amount < 1 && warpActive)
            {
                amount += rate;
                warpVFX.SetFloat("WarpAmount", amount);
                MinFloatParameter intensity = mBloom.intensity;
                intensity.value += 3f;
                yield return new WaitForSeconds(0.05f);
            }
            ScapeItem();
        }
        else
        {
            float amount = warpVFX.GetFloat("WarpAmount");
            scapeImage.DOFade(0, 1f);
            while (amount > 0 & !warpActive)
            {
                amount -= rate;
                warpVFX.SetFloat("WarpAmount", amount);
                yield return new WaitForSeconds(0.05f);

                if(amount <= 0 + rate)
                {
                    amount = 0;
                    warpVFX.SetFloat("WarpAmount", amount);
                    warpVFX.Stop();
                }
            }
            yield return new WaitForSeconds(2f);
            TransitionManager.Instance.LoadScene((int)Define.Scenes.Shop);
        }
    }

    IEnumerator DecreaseIntensity()
    {
        MinFloatParameter intensity = mBloom.intensity;
        while (intensity.value > 15)
        {
            intensity.value -= 3f;
            yield return 0.05f;
        }
    }

    private void ScapeItem()
    {
        StartCoroutine(DecreaseIntensity());

        scapeImage.gameObject.SetActive(true);
        scapeImage.GetComponent<RectTransform>().DOScale(4f, 2).OnComplete(() =>
        {
            ShowButton(0, 0.65f);
        });
    }

    private void ShowDay()
    {
        if (DataManager.Instance != null)
        {
            scapeImage.sprite = scapeSprites[day - 1];
            dayText.DOText($"DAY {day}", 2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                dayText.DOFade(0, 1f).OnComplete(() =>
                {
                    newsPaperSprites[day - 1].SetActive(true);
                    news.DOFade(1, 1f);
                });
            });
        }
    }

    private void ShowButton(float posX, float speed, bool flip = false)
    {
        int flipNum = 1;
        for(int i = 0; i < moveButtons.Length; i++)
        {
            moveButtons[i].DOAnchorPosX(posX * flipNum, speed).SetEase(Ease.Linear);

            if (flip) flipNum *= -1;
        }
    }

    private void Prologue()
    {
        if (current < idx - 1 && DataManager.Instance.CurrentUser.level == 1)
        {
            if (Input.GetMouseButtonDown(0) && prologueImage.color.a == 1)
            {
                prologueImage.DOFade(0, 1f).OnComplete(() =>
                {
                    prologueImage.sprite = prologueSprites[++current];
                });
            }

            if (prologueImage.color.a == 0)
            {
                prologueImage.DOFade(1, 1f);
            }
        }
        else
        {
            ShowDay();
            prologueImage.gameObject.SetActive(false);
            showPlologue = true;
        }
    }


    // Button
    public void MoveGameScene()
    {
        warpActive = false;
        ShowButton(-250, 0.45f, true);
        StartCoroutine(ActivateParticles());
    }

    public void MoveNeswpaper()
    {
        ShowButton(-250, 0.45f, true);
        warpVFX.Stop();
        scapeImage.gameObject.SetActive(false);
        //newsPaperSprites[DataManager].SetActive(false);
        news.DOFade(1, 0f);
    }
}
