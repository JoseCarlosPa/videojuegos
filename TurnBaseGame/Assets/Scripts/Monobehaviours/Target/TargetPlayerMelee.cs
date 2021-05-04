using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayerMelee : MonoBehaviour, IDefineTarget
{
    BattleHex initialHex;//Hex whose neighbors we are looking for
    List<BattleHex> neighboursToCheck;//collects neighbouring hexes that match the search criteria
    IEvaluateHex checkHex = new IfItIsTarget();//refers to the interface to access the behavior we need 
    public void DefineTargets(Hero currentAtacker)
    {
        initialHex = currentAtacker.GetComponentInParent<BattleHex>();

        //collect potencial tergets
        neighboursToCheck = NeighboursFinder.GetAdjacentHexes(initialHex, checkHex);
        foreach (BattleHex hex in neighboursToCheck)
        {
            hex.DefineMeAsPotencialTarget();//mark potential target
        }
    }
}
