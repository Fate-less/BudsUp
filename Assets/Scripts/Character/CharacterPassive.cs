using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterPassive : MonoBehaviour
{
    public string characterName;
    public Sprite portrait;

    public abstract void OnGameStart(StackManager stackManager);
    public abstract void OnPerfectStack(StackManager stackManager);
    public abstract void OnComboBreak(StackManager stackManager);
}