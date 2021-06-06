using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPos : IInitialHexes
{
    List<BattleHex> initialHexes = new List<BattleHex>();// recopila los hexes vecinos para el hex evaluado
    public List<BattleHex> GetNewInitialHexes()
    {
        initialHexes.Clear();// vacía la matriz antes de volver a llenarla
        foreach (BattleHex hex in FieldManager.activeHexList)
        {
            if (hex.isNeighboringHex & !hex.isIncluded)// elimina maleficios innecesarios
            {
                initialHexes.Add(hex);
            }
        }
        return initialHexes;
    }
}
