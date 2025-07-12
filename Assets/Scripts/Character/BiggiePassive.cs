using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggiePassive : CharacterPassive
{
    private int triggerCombo = 10;
    private int bonusCoins = 5;

    public override void OnPerfectStack(StackManager stackManager)
    {
        if (stackManager.comboCount >= triggerCombo && stackManager.comboCount % 5 == 0)
        {
            stackManager.AddCoins(bonusCoins);
        }
    }

    public override void OnGameStart(StackManager stackManager) { }
    public override void OnComboBreak(StackManager stackManager) { }
}