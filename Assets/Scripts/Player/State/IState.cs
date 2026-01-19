using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Enter(PlayerController controller) { }

    public void Exit(PlayerController controller) { }

    public void Update(PlayerController controller) {}
}
