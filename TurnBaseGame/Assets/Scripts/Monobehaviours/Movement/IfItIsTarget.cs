using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfItIsTarget : MonoBehaviour, IEvaluateHex
{
    public bool EvaluateHex(BattleHex evaluatedHex)
    {
        // comprobar si el héroe está en el hexagono o no y
        // si este objeto contiene el componente Enemy
        if (BattleController.currentAtacker.GetComponent<Enemy>() == null)
        {
            return evaluatedHex.GetComponentInChildren<Enemy>() != null;
        }
        else
        {
            return evaluatedHex.GetComponentInChildren<Hero>() != null &&
            evaluatedHex.GetComponentInChildren<Enemy>() == null;
        }
    }

}
