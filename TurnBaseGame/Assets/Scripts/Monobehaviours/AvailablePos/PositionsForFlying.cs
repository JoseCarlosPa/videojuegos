using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionsForFlying : MonoBehaviour,IAdjacentFinder
{
    IEvaluateHex checkHex = new IfItIsNewFlying();// consulte la interfaz para acceder al comportamiento que necesitamos
    // individualiza la búsqueda de puestos para un regimiento de vuelo
    public void GetAdjacentHexesExtended(BattleHex initialHex)
    {
        List<BattleHex> neighboursToCheck = NeighboursFinder.GetAdjacentHexes(initialHex,checkHex);
        foreach (BattleHex hex in neighboursToCheck)
        {
            hex.isNeighboringHex = true;// define el hexadecimal como adyacente al hexadecimal inicial evaluado
            hex.distanceText.SetDistanceForFlyingUnit(initialHex);
            hex.MakeMeAvailable();
        }
    }
}
