using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using DG.Tweening;

public class DayManager : MonoSingleton<DayManager>
{
    [SerializeField]
    private Light directionalLight;
    [SerializeField]
    private LightingPreset Preset;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI markText;
    [SerializeField]
    [Range(8, 24)] private float timeOfDay;
    public bool isPlay = true;

    [SerializeField]
    private GameObject clouds;
    [SerializeField]
    private float minClodePos = -200f;
    [SerializeField]
    private float maxClodePos = 200f;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private TextMeshProUGUI goalText;

    public int h = 8;
    public int minute = 0;
    
    [SerializeField]
    private float timer;
    [SerializeField]
    private float tenMinute;

    public int startTotalMinutes;
    public int afterSecond;
    public int endTotalMinutes;

    public int beforeTime;

    public Light DirectionLight
    {
        get
        {
            return directionalLight;
        }
    }

    public int Time
    {
        get
        {
            return h;
        }
    }

    public bool isCheck = false;

    protected override void Init()
    {
        base.Init();
        CheckObject[] CheckObj = FindObjectsOfType<CheckObject>();
        var allLight = from light in CheckObj
                        where light.gameObject.HasComponent<Light>()
                        select light;

        if (allLight.Count() > 1)
        {
            foreach (CheckObject light in allLight)
            {
                if (!light.isCheck)
                    Destroy(light.gameObject);
            }
        }
        /*        CheckObject[] lights = FindObjectsOfType<CheckObject>();
                if (lights.Length > 1)
                {
                    foreach (CheckObject light in lights)
                    {
                        if (!light.isCheck && light.gameObject.HasComponent<Light>()) Destroy(light.gameObject);
                    }
                }*/
    }


    private void Start()
    {
        DontDestroyOnLoad(directionalLight);
        directionalLight.GetComponent<CheckObject>().isCheck = true;
        tenMinute = (float)1 / 6;
        DeliveryManager.Instance.AddContent("08:00");
        startTotalMinutes = (int)timeOfDay * 60 + (minute * 10);
        afterSecond = (Random.Range(6, 9) * 10) - ((DataManager.Instance.CurrentUser.level / 2)* 10);
    }

    private void Update()
    {
        DayLighting();
        MoveClouds();
        
        if (timer >= tenMinute)
        {
            timer = 0;
            minute = (minute + 1) % 6;
            if (minute == 0)
                h++;
        }

        if (DeliveryManager.Instance.currentAnswer.time == Time && !isCheck)
        {
            DeliveryManager.Instance.AnswerContent();
            if (DeliveryManager.Instance.answerCheck)
            {
                DataManager.Instance.CurrentUser.scapeGoal = 1;
                StartCoroutine(DeliveryManager.Instance.SucessContent((int)DeliveryManager.Instance.currentAnswer.place));
                goalText.DOFade(1, 1f).OnComplete(() =>
                {
                    goalText.DOFade(0f, 1.5f);
                });
            }
            else
            {
                Debug.Log("배달 실패");
            }
            isCheck = true;
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if(directionalLight != null)
        {
            directionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

           directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }

    private void DayLighting()
    {
        if (Preset == null)
            return;


        if (isPlay)
        {
            if (timeOfDay >= 24)
            {
                PoolManager.Instance.AllDespawn();
                SceneManager.LoadScene((int)Define.Scenes.Cost - 1);
                //TransitionManager.Instance.LoadScene((int)Define.Scenes.Cost);
                return;
            }
            //(Replace with a reference to the game time)
            timeOfDay += UnityEngine.Time.deltaTime / 40;
            timer += UnityEngine.Time.deltaTime / 40;
            SetTimeText();
            CheckQuest();
            //timeOfDay %= 24; //Modulus to ensure always between 0-24
            UpdateLighting(timeOfDay / 24);
        }
        else
        {
            UpdateLighting(timeOfDay / 24);
        }
    }

    private void MoveClouds()
    {
        if (clouds != null)
        {
            if (clouds.activeSelf && timeOfDay >= 18)
                clouds.SetActive(false);
            if (clouds.transform.position.x >= maxClodePos)
                clouds.transform.SetPositionX(minClodePos);
            clouds.transform.Translate(Vector3.right * speed * UnityEngine.Time.deltaTime);
        }
    }

    public void FinddirectionalLight()
    {
        if (directionalLight != null) return;

        directionalLight = GameObject.FindGameObjectWithTag("Light").GetComponent<Light>();
    }

    private void SetTimeText()
    {
        string temp = $"{(int)h} {minute}0";
        temp = temp.PadLeft(5,'0');
        timeText.text = temp;
    }

    private void CheckQuest()
    {
        endTotalMinutes = (int)h * 60 + (minute * 10);
        
        if(endTotalMinutes - startTotalMinutes == afterSecond)
        {
            string temp = $"{(int)h}:{minute}0";
            temp = temp.PadLeft(5, '0');
            DeliveryManager.Instance.AddContent(temp);
            startTotalMinutes = endTotalMinutes;
            afterSecond = (Random.Range(6, 9) * 10) - ((DataManager.Instance.CurrentUser.level / 2) * 10);
        }
    }
}
