using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSound : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.AllStop();
        SoundManager.Instance.Play("Day");
    }
}
