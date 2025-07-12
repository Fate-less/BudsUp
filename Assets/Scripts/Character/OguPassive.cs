using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OguPassive : CharacterPassive
{
    private int triggerCombo = 10;
    private int multiplier = 2;

    public override void OnPerfectStack(StackManager stackManager)
    {
        if (stackManager.comboCount >= triggerCombo && stackManager.comboCount % 5 == 0)
        {
            stackManager.ApplyScoreMultiplier(multiplier);
        }
    }

    public override void OnGameStart(StackManager stackManager) { }
    public override void OnComboBreak(StackManager stackManager) { }
}