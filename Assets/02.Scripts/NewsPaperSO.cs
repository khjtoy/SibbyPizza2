using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewsPaper", menuName = "ScriptableObjects/NewsPaper")]
public class NewsPaperSO : ScriptableObject
{
    public Sprite image;

    [Multiline(50)]
    public string story;
}
