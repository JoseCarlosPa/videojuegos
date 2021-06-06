using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfItIsNewGroundAI : MonoBehaviour, IEvaluateHex
{
    public bool EvaluateHex(BattleHex evaluatedHex)
    {
        return evaluatedHex.battleHexState
                    == HexState.active// Excluye maleficios inactivos
                    && !evaluatedHex.isStartingHex// Excluye la posición inicial
                    && !evaluatedHex.isIncluded// excluye los hexágonos que ya están marcados como válidos
                    && evaluatedHex.AvailableToGround()// excluye agua y montañas
                    && ifThereIsAI(evaluatedHex);// excluye los hacks ocupados por el regimiento del jugador

    }
    private bool ifThereIsAI(BattleHex evaluatedHex)
    {
        bool AIPosfalse = true;
        // si el hex está ocupado por un objeto de tipo Hero, pero sin un componente Enemy
        if (evaluatedHex.GetComponentInChildren<Hero>() != null &&
            evaluatedHex.GetComponentInChildren<Enemy>() == null)
        {
            AIPosfalse = false;// luego permitimos que el troll pase por este maleficio
        }
        return AIPosfalse;
    }
}
