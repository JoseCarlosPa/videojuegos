using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayerMelee : MonoBehaviour, IDefineTarget
{
    BattleHex initialHex;// Hex cuyos vecinos estamos buscando
    List<BattleHex> neighboursToCheck;// recopila los hexágonos vecinos que coinciden con los criterios de búsqueda
    IEvaluateHex checkHex = new IfItIsTarget();// se refiere a la interfaz para acceder al comportamiento que necesitamos
    Turn turn;
    public void DefineTargets(Hero currentAtacker)
    {
        initialHex = currentAtacker.GetComponentInParent<BattleHex>();

        // recolectar tergets potenciales
        neighboursToCheck = NeighboursFinder.GetAdjacentHexes(initialHex, checkHex);
        int currentAttackerVelocity = BattleController.currentAtacker.heroData.CurrentVelocity;
        if (neighboursToCheck.Count > 0)
        {
            foreach (BattleHex hex in neighboursToCheck)
            {
                hex.DefineMeAsPotencialTarget();// marcar el objetivo potencial
            }
        }
        else if (neighboursToCheck.Count == 0 && currentAttackerVelocity == 0)
        {
            turn = FindObjectOfType<Turn>();
            turn.TurnIsCompleted();
        }
    }
}
