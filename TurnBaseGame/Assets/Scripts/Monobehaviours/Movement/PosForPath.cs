using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosForPath : MonoBehaviour, IAdjacentFinder
{
    IEvaluateHex checkHex = new IfItIsOptimalPath();// seleccione la opción para encontrar la ruta óptima
    public void GetAdjacentHexesExtended(BattleHex initialHex)
    {
        // recolecta hexes para seleccionar un nuevo enlace de la cadena de ruta óptima
        List<BattleHex> neighboursToCheck = NeighboursFinder.GetAdjacentHexes(initialHex, checkHex);
        foreach (BattleHex hex in neighboursToCheck)
        {
            // comparar distancias entre dos hexes
            if (hex.distanceText.EvaluateDistance(initialHex))
            {
                OptimalPath.nextStep = hex;// guarda el hexadecimal incluido en la ruta óptima
                break;// ya que tenemos que dejar de buscar otros maleficios
            }
        }
    }
}
