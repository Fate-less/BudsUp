using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TappyPassive : CharacterPassive
{
    private int perfectStacks = 3;

    public override void OnGameStart(StackManager stackManager)
    {
        stackManager.ForcePerfectStacks(perfectStacks);
    }

    public override void OnPerfectStack(StackManager stackManager) { }
    public override void OnComboBreak(StackManager stackManager) { }
}