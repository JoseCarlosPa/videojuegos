using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfAILooksForAllTargets : MonoBehaviour, IEvaluateHex
{
    public bool EvaluateHex(BattleHex evaluatedHex)
    {
        return evaluatedHex.battleHexState
                    == HexState.active// Excluye maleficios inactivos
                    && !evaluatedHex.isStartingHex// Excluye la posición inicial
                    && evaluatedHex.AvailableToGround();// excluye agua y montañas
    }

}
