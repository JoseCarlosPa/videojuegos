using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfItIsTargetRange : IEvaluateHex
{
    public bool EvaluateHex(BattleHex evaluatedHex)
    {
        return evaluatedHex.battleHexState
                   == HexState.active// excluir hexágonos inactivos
                   && !evaluatedHex.isStartingHex // excluir hexadecimal inicial
                   && !evaluatedHex.lookingForTarget;// excluir hexadecimal previamente comprobado

    }


}
