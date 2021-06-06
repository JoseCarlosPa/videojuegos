using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailablePos : MonoBehaviour
{
    private int step;// cuenta las iteraciones
    List<BattleHex> initialHexes = new List<BattleHex>();// recopila los hexes vecinos para el hex evaluado

    public void GetAvailablePositions(int stepsLimit,
        IAdjacentFinder AdjFinder, IInitialHexes getHexesToCheck)// busca todas las posiciones disponibles
    {
        BattleHex startingHex = BattleController.currentAtacker.GetComponentInParent<BattleHex>();
        AdjFinder.GetAdjacentHexesExtended(startingHex);// busca hexágonos adyacentes al hex inicial. Unidad voladora por ahora
        // ejecuta iteraciones para encontrar todas las posiciones disponibles. pasos = número de iteraciones
        for (step = 2; step <= stepsLimit; step++)
        {
            initialHexes = getHexesToCheck.GetNewInitialHexes();// recopila hexágonos listos para una nueva iteración
            foreach (BattleHex hex in initialHexes)
            {
                AdjFinder.GetAdjacentHexesExtended(hex);// define los hexes vecinos para cada hex de la colección
                hex.isIncluded = true;// define el hexadecimal evaluado como posición disponible
            }
        }
    }
    //internal List<BattleHex> GetNewInitialHexes()//collects objects whose neighbours need to be found
    //{
    //    initialHexes.Clear();// empty the array before filling it again
    //    foreach (BattleHex hex in FieldManager.allHexesArray)
    //    {
    //        if (hex.isNeighboringHex & !hex.isIncluded)//eliminates unnecessary hexes
    //        {
    //            initialHexes.Add(hex);
    //        }
    //    }
    //    return initialHexes;
    //}
}
