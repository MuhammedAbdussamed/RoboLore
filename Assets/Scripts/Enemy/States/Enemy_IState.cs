using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Enemy_IState
{
    public void Enter(Enemy_Bot_Base botController) { }

    public void Exit(Enemy_Bot_Base botController) { }

    public void Update(Enemy_Bot_Base botController) { }
}
