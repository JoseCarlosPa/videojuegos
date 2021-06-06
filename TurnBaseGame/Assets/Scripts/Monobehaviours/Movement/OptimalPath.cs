using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptimalPath : MonoBehaviour
{
    public static List<BattleHex> optimalPath = new List<BattleHex>();// recopila hexágonos en la lista
    public static BattleHex nextStep;// hexagono incluido en la lista de rutas óptimas
    public List<Image> landscapes = new List<Image>();// recopila imágenes de maleficios incluidos en la ruta óptima
    BattleHex targetHex;// posición de destino, hexagono pulsada
    IAdjacentFinder AdjacentOption = new PosForPath();
    Move move;
    private void Start()
    {
        move = GetComponent<Move>();
    }
    // recopila hexágonos en la lista de ruta óptima y los resalta
    internal void MatchPath()
    {
        optimalPath.Clear();// borra la lista antes de volver a llenarla
        targetHex = BattleController.targetToMove;// primer hexadecimal incluido en la ruta óptima
        optimalPath.Add(targetHex);

        // define la distancia desde el hex objetivo
        int steps = targetHex.distanceText.distanceFromStartingPoint;
        for (int i = steps; i > 1;)// itera para averiguar todos los hexes que se incluirán en la ruta óptima
        {
            AdjacentOption.GetAdjacentHexesExtended(targetHex);// descubre los hexes adyacentes al targethex
            targetHex = nextStep;// cuando el hexadecimal se incluye en la lista, se convierte en un nuevo hexadecimal objetivo
            i -= nextStep.distanceText.MakeMePartOfOptimalPath();// disminuye la i de forma variable por el valor de stepsToGo
            }
        ManagePath();
    }
    void ManagePath()// invierte el camino óptimo, llena el camino con imágenes de los hexes
    {
        landscapes.Clear();// borra la lista antes de volver a llenarla
        optimalPath.Reverse();// ordena los elementos de la lista en el orden opuesto
        foreach (BattleHex hex in optimalPath)
        {
            landscapes.Add(hex.Landscape);// llena la lista con imágenes
        }
        move.path = landscapes;// envía información sobre la ruta óptima a la clase Move
    }
}
