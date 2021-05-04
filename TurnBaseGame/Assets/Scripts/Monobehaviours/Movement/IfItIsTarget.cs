using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfItIsTarget : MonoBehaviour, IEvaluateHex
{
    public bool EvaluateHex(BattleHex evaluatedHex)
    {
        return evaluatedHex.GetComponentInChildren<Enemy>() != null;
    }

}
