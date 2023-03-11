using System;
using UnityEngine;

[Serializable]
public class AIAnimationData
{
    [SerializeField] private string movingParameterName = "Moving";
    [SerializeField] private string hitParameterName = "Hit";

    public int MovingParameterHash { get; private set; }
    public int HitParameterHash { get; private set; }

    public void Initialize()
    {
        MovingParameterHash = Animator.StringToHash(movingParameterName);
        HitParameterHash = Animator.StringToHash(hitParameterName);
    }
}
