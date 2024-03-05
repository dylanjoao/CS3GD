using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
        
    }

    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if (movementStateManager.dir.magnitude > 0.1f)
        {
            movementStateManager.SwitchState(movementStateManager.Walk);
        }

    }
}
