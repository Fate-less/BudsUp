using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BamPassive : CharacterPassive
{
    private float slowMultiplier = 0.95f;

    public override void OnGameStart(StackManager stackManager)
    {
        stackManager.SetBlockSpeedMultiplier(slowMultiplier);
    }

    public override void OnComboBreak(StackManager stackManager)
    {
        stackManager.SetBlockSpeedMultiplier(slowMultiplier);
    }

    public override void OnPerfectStack(StackManager stackManager) { }
}