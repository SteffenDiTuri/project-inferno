using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public int MP;
    public int SP;
    private bool inCombat;

    public bool IsInCombat()
    {
        return inCombat;
    }

    public void SetCombatState(bool state)
    {
        inCombat = state;
    }
}
