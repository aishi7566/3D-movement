using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : Istate
{
    protected PlayerMovementStateMachine stateMachine;

    protected Vector2 movementInput;

    protected float baseSpeed = 5f;

    protected float speedModifier =1f;
    protected Vector3 currentTargetRotation;
    protected Vector3 timeToReachTargetRotation;
    protected Vector3 dampedTargetRotationCurrentVelocity;
    protected Vector3 dampedTargetRotationPassedTime;
    public PlayerMovementState (PlayerMovementStateMachine playerMovementStateMachine)
    {
        stateMachine = playerMovementStateMachine;
        InitializeData();
    }

    private void InitializeData()
    {
        throw new NotImplementedException();
    }
    #region Istate Methods
    public virtual void Enter()
    {
       Debug.Log("State: "+GetType().Name);
    }

    public virtual void Exit()
    {
        
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

     public virtual void Update()
    {
        
    }
    
    public virtual void PhysicsUpdate()
    {
        Move();
    }
#endregion


#region Main Methods
    private void ReadMovementInput()
    {
        movementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }
     private void Move()
    {
        if (movementInput == Vector2.zero||speedModifier ==0f)
        {
            return;
        }
        
        Vector3 movementDirection = GetMovementInputDirection();

        float movementSpeed = GetMovementSpeed();

        Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();

        stateMachine.Player.Rigidbody.AddForce(movementSpeed * movementDirection-currentPlayerHorizontalVelocity,ForceMode.VelocityChange);
    }

    private float Rotate(Vector3 direction)
    {
        float directionAngle = Mathf.Atan2(direction.x,direction.z)*Mathf.Rad2Deg;

        if (directionAngle < 0f)
        {
            directionAngle +=360f;
        }
        directionAngle+= stateMachine.Player.MainCameraTransform.eulerAngles.y;
        if (directionAngle > 360f)
        {
            directionAngle -=360f;
        }
        return directionAngle;
    }
#endregion

   
#region Reusable Methods
    protected  Vector3 GetMovementInputDirection()
    {
        return new Vector3(movementInput.x,0f, movementInput.y);
    }

    protected float GetMovementSpeed()
    {
        return baseSpeed *speedModifier;
    }  
     protected Vector3 GetPlayerHorizontalVelocity()
    {
        Vector3 playerHorizontalVelocity = stateMachine.Player.Rigidbody.velocity;

        playerHorizontalVelocity.y = 0f;

        return playerHorizontalVelocity;
    }
#endregion
}
