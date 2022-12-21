using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadCutSceneSound : MonoBehaviour
{
    
    void Start()
    {
        SoundManager.Instance.AllStop();
        SoundManager.Instance.Play("GameOver");
    }
}
