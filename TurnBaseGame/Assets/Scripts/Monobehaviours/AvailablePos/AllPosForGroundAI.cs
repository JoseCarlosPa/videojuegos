using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPosForGroundAI : MonoBehaviour
{
    private int step;// cuenta las iteraciones
    List<BattleHex> initialHexes = new List<BattleHex>();// recopila los hexes vecinos para el hex evaluado
    IEvaluateHex checkHex = new IfAILooksForAllTargets();// consulte la interfaz para acceder al comportamiento que necesitamos

    // busca todas las posiciones disponibles
    public void GetAvailablePositions(int stepsLimit, IInitialHexes getHexesToCheck, BattleHex startingHex)
    {
        GetAdjacentHexesExtended(stepsLimit, startingHex);// busca hexágonos adyacentes al hex inicial. Unidad voladora por ahora
        // ejecuta iteraciones para encontrar todas las posiciones disponibles. pasos = número de iteraciones
        for (step = 2; step <= stepsLimit; step++)
        {
            initialHexes = getHexesToCheck.GetNewInitialHexes();// recopila hexágonos listos para una nueva iteración
            foreach (BattleHex hex in initialHexes)
            {
                    GetAdjacentHexesExtended(stepsLimit, hex);// define los hexes vecinos para cada hex de la colección
            }
        }
    }
    public void GetAdjacentHexesExtended(int stepsLimit, BattleHex initialHex)
    {
        List<BattleHex> neighboursToCheck = NeighboursFinder.GetAdjacentHexes(initialHex, checkHex);
        foreach (BattleHex hex in neighboursToCheck)
        {
            // Compara el valor actual de la variable del punto distanceFromStarting con un nuevo valor
            if (hex.distanceText.EvaluateDistanceForGroundAI(initialHex, stepsLimit))
            {
                hex.isNeighboringHex = true;
                hex.distanceText.SetDistanceForGroundUnit(initialHex);// establece la distancia desde el hex de inicio
            }
        }
    }

}
