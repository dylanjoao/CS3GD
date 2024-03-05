using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
        movementStateManager.anim.SetBool(movementStateManager.WalkHash, true);
    }

    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if (movementStateManager.dir.magnitude < 0.1f)
            ExitState(movementStateManager, movementStateManager.Idle);
    }

    void ExitState(MovementStateManager movementStateManager, MovementBaseState state)
    {
        movementStateManager.anim.SetBool(movementStateManager.WalkHash, false);
        movementStateManager.SwitchState(state);
    }
}
