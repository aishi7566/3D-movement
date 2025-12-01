using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDashingState : PlayerGroundedState
{

    private float startTime;

    private int consecutiveDashesUsed;
    public PlayerDashingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.ReusableData.MovementOnSolpesSpeedModifier = movementData.DashData.SpeedModifer;

        base.Enter();

        AddForceOnTransitionFromStationaryState();

        UpdateConsecutiveDashes();
        
        startTime = Time.time;
    }

    public override void OnAnimationTransitionEvent()
    {

        if (stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.HardStoppingState);

                return;
            }

            stateMachine.ChangeState(stateMachine.SprintingState);
    }

    private void UpdateConsecutiveDashes()
    {
        if (!IsConsecutive())
            {
                consecutiveDashesUsed = 0;
            }

            ++consecutiveDashesUsed;

            if (consecutiveDashesUsed == movementData .DashData.ConsecutiveDashesLimitAmount)
            {
                consecutiveDashesUsed = 0;

                stateMachine.Player.Input.DisableActionFor(stateMachine.Player.Input.PlayerActions.Dash, movementData.DashData.DashLimitReachedCooldown);
            }
    }

    private bool IsConsecutive()
    {
        return Time.time < startTime + movementData.DashData.TimeToBeConsideredConsecutive;
    }

    private void AddForceOnTransitionFromStationaryState()
    {
        if(stateMachine.ReusableData.MovementInput != Vector2.zero)
        {
            return; 
        }

        Vector3 characterRotationDirection = stateMachine.Player.transform.forward;

        characterRotationDirection.y = 0f;

        stateMachine.Player.Rigidbody.velocity = characterRotationDirection * GetMovementSpeed();
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        base.OnMovementCanceled(context);
    }

    protected override void OnDashStarted(InputAction.CallbackContext context)
    {
        base.OnDashStarted(context);
    }
}
