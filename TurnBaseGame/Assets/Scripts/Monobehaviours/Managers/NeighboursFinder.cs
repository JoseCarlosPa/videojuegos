using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighboursFinder : MonoBehaviour
{
    static private BattleHex startingHex;
    static List<BattleHex> allNeighbours = new List<BattleHex>();
    private FieldManager sceneManager;

    // Se llama al inicio antes de la primera actualización del cuadro
    void Start()
    {
      
    }
    static public List<BattleHex> GetAdjacentHexes(BattleHex startingHex, IEvaluateHex checkHex)// Busca y devuelve hexágonos vecinos
    {
        allNeighbours.Clear();
        // resta 1 ya que el índice de la matriz comienza en 1.
        int initialX = startingHex.horizontalCoordinate - 1; // primer índice para lista bidimensional
        int initialY = startingHex.verticalCoordinate - 1;// segundo índice para lista bidimensional
                                                          // itera xey de -1 a 1 para obtener hexágonos adyacentes que se refieren a las coordenadas del hex inicial
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x + y != 0 // excluir dos hexes que no son hexágonos adyacentes
                     && checkHex.EvaluateHex(FieldManager.allHexesArray[initialX + x, initialY + y])) // excluir hexágonos inactivos                  
                {
                    allNeighbours.Add(FieldManager.allHexesArray[initialX + x, initialY + y]);
                }
            }
        }
        return allNeighbours;
    }
}
