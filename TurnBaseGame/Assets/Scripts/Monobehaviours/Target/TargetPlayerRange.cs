using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayerRange : MonoBehaviour, IDefineTarget
{
    BattleHex initialHex;// Hexagonos cuyos vecinos estamos buscando
    List<BattleHex> neighboursToCheck;// recopila los hexágonos vecinos que coinciden con los criterios de búsqueda
    IEvaluateHex checkHex = new IfItIsTarget();// se refiere a la interfaz para acceder al comportamiento que necesitamos
    IInitialHexes getInitialHexes = new InitialTarget();
    Turn turn;
    public void DefineTargets(Hero currentAtacker)
    {
        // comprueba si hay un enemigo cerca
        if (TargetsNearby(currentAtacker) == false)
        {
            TargetsAtAttackDistance(currentAtacker);
        }
    }

    // si el enemigo está cerca, márcalo y deja de buscar enemigos distantes
    bool TargetsNearby(Hero currentAtacker)
    {
        bool targetNearby = false;// la variable te permite saber si hay un enemigo cerca
        initialHex = currentAtacker.GetComponentInParent<BattleHex>();// hexagonos inicial

        // recolecta hexágonos vecinos
        neighboursToCheck = NeighboursFinder.GetAdjacentHexes(initialHex, checkHex);
        if (neighboursToCheck.Count > 0)// comprobar si la lista no está vacía
        {
            foreach (BattleHex hex in neighboursToCheck)
            {
                hex.DefineMeAsPotencialTarget();// marcar el objetivo potencial
                //hex.lookingForTarget = true;
            }
            targetNearby = true;
        }
        return targetNearby;
    }

    // si no hay enemigos cerca, continúa buscando
    void TargetsAtAttackDistance(Hero currentAtacker)
    {
        int stepsLimit = currentAtacker.heroData.Atackdistanse;// número de niveles de búsqueda
        BattleHex inititalHex = currentAtacker.GetComponentInParent<BattleHex>();// hexadecimal inicial
        IAdjacentFinder adjFinder = new MarkTargets();// regla para verificar el hexadecimal
        
        // verifica toda el área de ataque
        currentAtacker.GetComponent<AvailablePos>().GetAvailablePositions(stepsLimit, adjFinder, getInitialHexes);
        CheckIfItIsNewTurn();
    }
    private void CheckIfItIsNewTurn()
    {
        BattleController battleController = FindObjectOfType<BattleController>();
        if (battleController.IsLookingForPotentialTargets().Count == 0
            && BattleController.currentAtacker.heroData.CurrentVelocity == 0)
        {
            turn = FindObjectOfType<Turn>();
            turn.TurnIsCompleted();
        }
    }
}
