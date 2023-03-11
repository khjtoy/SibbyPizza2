using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementStateMachine : StateMachine
{
    public AIController AI { get; }

    public AIIdlingState IdlingState { get; }
    public AIRoamingState RoamingState { get; }
    public AIBumpState BumpState { get; }
 
    public AIMovementStateMachine(AIController aIController)
    {
        AI = aIController;

        IdlingState = new AIIdlingState(this);
        RoamingState = new AIRoamingState(this);
        BumpState = new AIBumpState(this);
    }
}
