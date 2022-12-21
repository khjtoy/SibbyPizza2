using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Answer", menuName = "PlayerData/Answer")]
[Serializable]
public class AnswerSO : ScriptableObject
{
    public Define.Shop place;
    public int time;
    public string pizza;
}
