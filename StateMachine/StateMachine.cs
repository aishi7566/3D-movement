using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
{
  protected Istate currentState;

  public void ChangeState(Istate newSate)
    {
        currentState?.Exit();
        currentState = newSate;
        currentState.Enter();
    }

   public void HandleInput()
    {
        currentState?.HandleInput();
    } 

    public void UpInput()
    {
        currentState?.Update();
    } 
    
    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    } 
}
