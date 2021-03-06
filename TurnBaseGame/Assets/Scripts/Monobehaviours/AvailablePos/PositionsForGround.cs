using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionsForGround : MonoBehaviour, IAdjacentFinder
{
    IEvaluateHex checkHex = new IfItIsNewGround();// consulte la interfaz para acceder al comportamiento que necesitamos
    public void GetAdjacentHexesExtended(BattleHex initialHex)
    {
        List<BattleHex> neighboursToCheck = NeighboursFinder.GetAdjacentHexes(initialHex, checkHex);
        foreach (BattleHex hex in neighboursToCheck)
        {
            if (hex.distanceText.EvaluateDistanceForGround(initialHex))
            {
                hex.isNeighboringHex = true;// define el hexadecimal como adyacente al hexadecimal inicial evaluado
                hex.distanceText.SetDistanceForGroundUnit(initialHex);
                hex.MakeMeAvailable();
            }
        }
    }
}
