using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IState
{
    public void Enter(PlayerController controller) { Debug.Log("WalkState'e girildi"); }

    public void Exit(PlayerController controller) { }

    public void Update(PlayerController controller) 
    {

    }
}
