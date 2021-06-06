using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkTargets : IAdjacentFinder
{
    IEvaluateHex checkHex = new IfItIsTargetRange();// para acceder al comportamiento que necesitamos
    public void GetAdjacentHexesExtended(BattleHex initialHex)
    {
        // recolecta hexes que cumplen con los criterios definidos por la regla IfItIsTargetRange
        List<BattleHex> neighboursToCheck = NeighboursFinder.GetAdjacentHexes(initialHex, checkHex);

        foreach (BattleHex hex in neighboursToCheck)
        {
            hex.lookingForTarget = true;// define el hexadecimal como adyacente al hexadecimal evaluado
            if (hex.GetComponentInChildren<Enemy>() != null)
            {
                hex.DefineMeAsPotencialTarget();// marca hexadecimal como un objetivo potencial
            }
        }
    }
}
