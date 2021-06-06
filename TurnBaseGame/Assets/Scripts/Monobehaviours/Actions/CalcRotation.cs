using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcRotation : MonoBehaviour
{
    static float direction;// para calcular el ángulo de giro
    static float ZCoordinate;// el ángulo de rotación

    public static Quaternion CalculateRotation(Hero targetToAtack)
    {
        Vector3 targetPosition = targetToAtack.transform.position;// coordenadas de destino
        Hero currentAtacker = BattleController.currentAtacker;// para acceder a las coordenadas del héroe atacante
        Vector3 atackerPosition = currentAtacker.transform.position;// coordenadas del héroe atacante
        ZCoordinate = GetAngle(targetPosition, atackerPosition);// Cálculo del ángulo de rotación
        Quaternion rotation = Quaternion.EulerAngles(0, 0, ZCoordinate);// transformarse en un cuaternión
        return rotation;
    }
    private static float GetAngle(Vector3 targetPosition, Vector3 atackerPosition)
    {
        // calcula el arco tangente del ángulo de rotación
        direction = Mathf.Atan((targetPosition.y - atackerPosition.y) /
                               (targetPosition.x - atackerPosition.x));

        if (targetPosition.x > atackerPosition.x)// si el objetivo está a la derecha del héroe
        {
            ZCoordinate = direction;
        }
        else
        {
            ZCoordinate = Mathf.PI + direction;
        }
        return ZCoordinate;
    }
}
