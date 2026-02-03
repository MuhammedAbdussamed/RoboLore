using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Enemy_IState
{
    void Enter(Enemy_Bot_Base botController) { }

    void Exit(Enemy_Bot_Base botController) { }

    void Update(Enemy_Bot_Base botController) { }
}
