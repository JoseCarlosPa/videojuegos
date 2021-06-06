using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour
{
    public int distanceFromStartingPoint;// cuenta la distancia desde el hex inicial
    public int stepsToGo;// determina el número de pasos a través del hexadecimal
    public int defaultDistance;// Valor predeterminado de la variable distanceFromStartingPoint
    public int defaultstepsToGo;// Valor predeterminado de la variable stepsToGo
    BattleHex hex;
    Text distanceText; // se refiere al componente de texto del mismo objeto

    private void Start()
    {
        hex = GetComponentInParent<BattleHex>();
        distanceText = GetComponent<Text>();
    }
    // establece la distancia desde el hex inicial y la muestra
    public void SetDistanceForGroundUnit(BattleHex initialHex)
    {
        // agrega un paso al paso anterior para obtener la diastancia desde el punto de partida
        distanceFromStartingPoint = initialHex.distanceText.distanceFromStartingPoint
                    + initialHex.distanceText.stepsToGo;
        //display new value of the distanceFromStartingPoint
        //DisplayDistanceText();
    }
    public void SetDistanceForFlyingUnit(BattleHex initialHex)
    {
        stepsToGo = 1;
        // agrega un paso al paso anterior para obtener la diastancia desde el punto de partida
        distanceFromStartingPoint = initialHex.distanceText.distanceFromStartingPoint + stepsToGo;
        //display new value of the distanceFromStartingPoint
        //DisplayDistanceText();

    }
    private void DisplayDistanceText()
    {
        distanceText.text = distanceFromStartingPoint.ToString();
        distanceText.color = new Color32(255, 255, 255, 255);
    }


    public bool EvaluateDistance(BattleHex initialHex)// compara distancias entre dos hexes
    {
        return distanceFromStartingPoint + stepsToGo ==
                initialHex.distanceText.distanceFromStartingPoint;
    }
    public int MakeMePartOfOptimalPath()// incluye este hexadecimal en la lista de rutas óptimas,
    // devuelve el número de pasos para pasar por el hex.
    {
        OptimalPath.optimalPath.Add(hex);
        hex.Landscape.color = new Color32(150, 150, 150, 255);
        return stepsToGo;
    }
    public bool EvaluateDistanceForGround(BattleHex initialHex)
    {
        // distancia para alcanzar el hex inicial y salir de él
        int currentDistance = initialHex.distanceText.distanceFromStartingPoint
                              + initialHex.distanceText.stepsToGo;
        int stepsLimit = BattleController.currentAtacker.heroData.CurrentVelocity;// velocidad de un héroe
        // el valor predeterminado de distanceFromStartingPoint es 20 para establecer la ruta más corta
        return distanceFromStartingPoint > currentDistance &&
                stepsLimit >= currentDistance;// para evaluar si la velocidad es suficiente para alcanzar este hex
    }
    public bool EvaluateDistanceForGroundAI(BattleHex initialHex, int stepsLimit)
    {
        // distancia para alcanzar el hex inicial y salir de él
        int currentDistance = initialHex.distanceText.distanceFromStartingPoint
                              + initialHex.distanceText.stepsToGo;
        // el valor predeterminado de distanceFromStartingPoint es 20 para establecer la ruta más corta
        return distanceFromStartingPoint > currentDistance &&
                stepsLimit >= currentDistance;// para evaluar si la velocidad es suficiente para alcanzar este
    }

}
