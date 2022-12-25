using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

 // 에디터 모드 또는 플레이 모드에서 실행 가능
public class StartSceneInGameManager : MonoBehaviour
{
    [SerializeField]
    private Light directionalLight;
    [SerializeField]
    private LightingPreset Preset;
    [SerializeField]
    [Range(8, 24)] private float timeOfDay = 8f;
    public bool isPlay = true;

    [SerializeField]
    private GameObject clouds;
    [SerializeField]
    private float minClodePos = -200f;
    [SerializeField]
    private float maxClodePos = 200f;
    [SerializeField]
    private float speed = 5f;

    private int minute = 0;

    [SerializeField]
    private float timer;
    [SerializeField]
    private float tenMinute;

    [SerializeField]
    private GameObject[] AIPrefab;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private GameObject ScreenButton;

    [SerializeField]
    private GameObject[] Button;

    [SerializeField]
    private GameObject howToPlayPanel;

    [SerializeField]
    private GameObject[] daySceneTuto;
    [SerializeField]
    private TextMeshProUGUI dayIndex;

    private int daySceneTutoIDX;
    

    [SerializeField]
    private GameObject[] pizzaMakeTuto;
    [SerializeField]
    private TextMeshProUGUI pizzaMakeIndex;

    private int pizzaMakeTutoIDX;

    [SerializeField]
    private GameObject[] pizzaMoveTuto;
    [SerializeField]
    private TextMeshProUGUI pizzaMoveIndex;

    [SerializeField]
    private Image continueButton;

    private int pizzaMoveTutoIDX;


    [SerializeField]
    private GameObject moveKey;


    private void Awake()
    {
        SoundManager.Instance.AllStop();
    }

    private void Start()
    {
        SoundManager.Instance.Play("StartLobby");
        daySceneTutoIDX = 0;
        pizzaMakeTutoIDX = 0;
        pizzaMoveTutoIDX = 0;
        timeOfDay = 9f;

        mainCamera = Camera.main;
        mainCamera.transform.SetPosition(42.5f, 3.3f, 22f);

        ScreenButton.SetActive(true);

        if (Application.isPlaying)
        {
            tenMinute = (float)1 / 6;
        }

        if(DataManager.Instance.CurrentUser.level == 1)
        {
            Debug.Log("check");
            continueButton.color = new Color(1, 1, 1, 0.04f);
        }

        StartCoroutine(ClickInput());
        StartCoroutine(AISpawnDelay());
        StartCoroutine(AIRSpawnDelay());
    }

    private void Update()
    {
        DayLighting();
        MoveClouds();
        if (Application.isPlaying)
        {
            if (timer >= tenMinute)
            {
                timer = 0;
                minute = (minute + 1) % 6;
            }
        }
    }

    IEnumerator AISpawnDelay()
    {
        while(true)
        {
            GameObject clone = PoolManager.Instance.GetPooledObject((int)Define.PoolObject.StartSceneAI);
            clone.transform.position = new Vector3(50, 0, Random.Range(15.7f, 18f));
            clone.transform.rotation = Quaternion.identity;
            clone.SetActive(true);
            yield return new WaitForSeconds(Random.Range(2f, 10f));

        }
    }

    IEnumerator AIRSpawnDelay()
    {
        while (true)
        {
            GameObject clone = PoolManager.Instance.GetPooledObject((int)Define.PoolObject.StartSceneAI);
            clone.transform.position = new Vector3(33f, 0, Random.Range(15.7f, 18f));
            clone.transform.rotation = Quaternion.identity;
            clone.GetComponent<StartSceneAIHolder>().playerDir = -1;
            clone.SetActive(true);
            yield return new WaitForSeconds(Random.Range(2f, 10f));
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if (directionalLight != null)
        {
            directionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }

    private void DayLighting()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying && isPlay)
        {
            if (timeOfDay >= 24)
                timeOfDay = 0;
            if (clouds.activeSelf && timeOfDay >= 18)
                clouds.SetActive(false);
            
            timer += Time.deltaTime * 3;
            UpdateLighting(timeOfDay / 24);
        }
        else
        {
            UpdateLighting(timeOfDay / 24);
        }
    }

    private void MoveClouds()
    {
        if (Application.isPlaying)
        {
            if (clouds.transform.position.x >= maxClodePos)
                clouds.transform.SetPositionX(minClodePos);
            clouds.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    public IEnumerator ClickInput()
    {
        while(true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                
                ClickScreen();
                break;
            }
            yield return null;
        }
    }

    public void ClickScreen()
    {
        ScreenButton.SetActive(false);
        mainCamera.transform.DOMove(new Vector3(42.5f, 1.97f, 18.45f), 1.5f).OnComplete(() => {
            for (int i = 0; i < 4; i++)
            {
                Button[i].SetActive(true);
            }
        });
        
    }

    public void GameStart()
    {
        SceneManager.LoadScene("DayScene");
    }

    public void Continue()
    {

    }

    public void HowtoPlay()
    {
        howToPlayPanel.SetActive(true);
    }
   
    public void howToPlayQuit()
    {
        howToPlayPanel.SetActive(false);
    }

    public void DaySceneTutoOn()
    {
        dayIndex.gameObject.SetActive(true);
        dayIndex.text = ((daySceneTutoIDX % daySceneTuto.Length) + 1).ToString();
        daySceneTuto[daySceneTutoIDX % daySceneTuto.Length].SetActive(true);
    }

    public void DaySceneTutoOff()
    {
        dayIndex.gameObject.SetActive(false);
        daySceneTuto[daySceneTutoIDX].SetActive(false);
    }

    public void NextDaySceneTuto()
    {
        daySceneTuto[(daySceneTutoIDX % daySceneTuto.Length)].SetActive(false);
        daySceneTutoIDX++;
        dayIndex.text = ((daySceneTutoIDX % daySceneTuto.Length) + 1).ToString();
        daySceneTuto[(daySceneTutoIDX % daySceneTuto.Length)].SetActive(true);
    }

    public void MakeSceneTutoOn()
    {
        pizzaMakeIndex.gameObject.SetActive(true);
        pizzaMakeIndex.text = ((pizzaMakeTutoIDX % pizzaMakeTuto.Length) + 1).ToString();
        pizzaMakeTuto[pizzaMakeTutoIDX % pizzaMakeTuto.Length].SetActive(true);
    }

    public void MakeSceneTutoOff()
    {
        pizzaMakeIndex.gameObject.SetActive(false);
        pizzaMakeTuto[pizzaMakeTutoIDX % pizzaMakeTuto.Length].SetActive(false);
    }

    public void NextPizzaMakeTuto()
    {
        pizzaMakeTuto[pizzaMakeTutoIDX % pizzaMakeTuto.Length].SetActive(false);
        pizzaMakeTutoIDX++;
        pizzaMakeIndex.text = ((pizzaMakeTutoIDX % pizzaMakeTuto.Length) + 1).ToString();
        pizzaMakeTuto[(pizzaMakeTutoIDX % pizzaMakeTuto.Length)].SetActive(true);
    }

    public void MoveSceneTutoOn()
    {
        pizzaMoveIndex.gameObject.SetActive(true);
        pizzaMoveIndex.text = ((pizzaMoveTutoIDX % pizzaMoveTuto.Length) + 1).ToString();
        pizzaMoveTuto[pizzaMoveTutoIDX % pizzaMoveTuto.Length].SetActive(true);
    }

    public void MoveSceneTutoOff()
    {
        pizzaMoveIndex.gameObject.SetActive(false);
        pizzaMoveTuto[pizzaMoveTutoIDX % pizzaMoveTuto.Length].SetActive(false);
    }

    public void NextPizzaMoveTuto()
    {
        pizzaMoveTuto[pizzaMoveTutoIDX % pizzaMoveTuto.Length].SetActive(false);
        pizzaMoveTutoIDX++;
        pizzaMoveIndex.text = ((pizzaMoveTutoIDX % pizzaMoveTuto.Length) + 1).ToString();
        pizzaMoveTuto[(pizzaMoveTutoIDX % pizzaMoveTuto.Length)].SetActive(true);
    }

    public void MoveKeyOn()
    {
        moveKey.SetActive(true);
    }

    public void MoveKeyOff()
    {
        moveKey.SetActive(false);
    }

    public void Quit()
    {
        Button[3].transform.position = Button[3].transform.position + new Vector3(Random.Range(-335,535), Random.Range(-280, 280), 0);
    }

    private void OnValidate()
    {
        if (directionalLight != null) return;

        if (RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    }
}
