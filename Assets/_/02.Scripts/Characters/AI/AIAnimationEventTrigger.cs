using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationEventTrigger : MonoBehaviour
{
    private AIController ai;

    private void Awake()
    {
        ai = transform.parent.GetComponent<AIController>();
    }

    public void TriggerOnMovementStateAnimationEnterEventAI()
    {
        if (IsInAnimationTransition())
        {
            return;
        }

        ai.OnMovementStateAnimationEnterEvent();
    }

    public void TriggerOnMovementStateAnimationExitEventAI()
    {
        if (IsInAnimationTransition())
        {
            return;
        }

        ai.OnMovementStateAnimationExitEvent();
    }

    public void TriggerOnMovementStateAnimationTransitionEventAI()
    {
        if (IsInAnimationTransition())
        {
            return;
        }

        ai.OnMovementStateAnimationTransitionEvent();
    }

    private bool IsInAnimationTransition(int layerIndex = 0)
    {
        return ai.Animator.IsInTransition(layerIndex);
    }
}
