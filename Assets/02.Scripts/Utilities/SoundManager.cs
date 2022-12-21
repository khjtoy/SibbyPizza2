using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    public Define.Sound soundMixer;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}

public class SoundManager : MonoSingleton<SoundManager>
{
    public Sound[] sounds;
    public Dictionary<int, Sound> soundDic = new Dictionary<int, Sound>();
    [SerializeField]
    private AudioMixerGroup[] AudioMixerGroups;

    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private Slider SFXslider;
    [SerializeField]
    private Slider BGMslider;

    public float SFXvolume;
    public float BGMvolume;

    private GameObject SettingPanel;

    private void Awake()
    {
        if (SettingPanel == null)
        {
            SettingPanel = this.gameObject.transform.GetChild(0).gameObject;
        }

        int index = 0;
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.outputAudioMixerGroup = AudioMixerGroups[(int)s.soundMixer];

            s.source.playOnAwake = false;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            soundDic.Add(index++, s);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SettingPanel.activeSelf)
            {
                SettingPanel.SetActive(false);
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SettingPanel.SetActive(true);
            }
        }
    }

    public void CloseSettingPanel()
    {
        SettingPanel.SetActive(false);
    }

    public void SFXSetting()
    {
        SFXvolume = SFXslider.value;
        audioMixer.SetFloat("SFX", SFXvolume);
    }

    public void BGMSetting()
    {
        BGMvolume = BGMslider.value;
        audioMixer.SetFloat("BGM", BGMvolume);
    }

    public void Play(string name)
    {
        Sound s;
        if (soundDic.ContainsKey(name.GetHashCode()))
        {
            s = soundDic[name.GetHashCode()];
        }
        else
        {
            s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + "not found!");
                return;
            }
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s;
        if (soundDic.ContainsKey(name.GetHashCode()))
        {
            s = soundDic[name.GetHashCode()];
        }
        else
        {
            s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + "not found!");
                return;
            }
        }
        s.source.Stop();
    }

    public void AllStop()
    {
        for (int i = 0; i < soundDic.Count; i++)
        {
            Sound s = soundDic[i];
            s.source.Stop();
            Debug.Log(soundDic[i]);
        }
    }

    public void GoMain()
    {
        SceneManager.LoadScene("Start");
    }
}
