using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPosAI : MonoBehaviour, IInitialHexes
{
    List<BattleHex> initialHexes = new List<BattleHex>();// recopila los hexes vecinos para el hex evaluado
    public List<BattleHex> GetNewInitialHexes()
    {
        initialHexes.Clear();// vacía la matriz antes de volver a llenarla
        foreach (BattleHex hex in FieldManager.activeHexList)
        {
            if (hex.isNeighboringHex & !hex.isIncluded
                && ifThereIsPlayersRegiment(hex))// elimina maleficios innecesarios
            {
                initialHexes.Add(hex);
            }
        }
        return initialHexes;
    }

    // comprueba si el hexágono inicial está ocupado por la escuadra de un jugador
    private bool ifThereIsPlayersRegiment(BattleHex evaluatedHex)
    {
        bool AIPosfalse = true;
        // si el objeto de tipo Hero no contiene una clase de tipo Enemy
        if (evaluatedHex.GetComponentInChildren<Hero>() != null &&
            evaluatedHex.GetComponentInChildren<Enemy>() == null)
        {
            evaluatedHex.DefineMeAsPotencialTarget();
            AIPosfalse = false;
        }
        return AIPosfalse;
    }
}
