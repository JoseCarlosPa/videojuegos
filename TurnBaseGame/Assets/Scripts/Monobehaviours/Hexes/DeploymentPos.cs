using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PositionForRegiment { none, player, enemy };
public class DeploymentPos : MonoBehaviour
{
    public PositionForRegiment regimentPosition;// ayuda a mostrar la posición potencial
    BattleHex parentHex;
    void Start()
    {
        parentHex = GetComponentInParent<BattleHex>();// encuentra el hexadecimal principal
        StartBTN.OnStartingBattle += DisableMe;
    }

    public void OnMouseDown()// se ejecuta cuando el usuario ha presionado el botón del mouse mientras está sobre el Collider.
    {
        // comprueba si el jugador hizo clic en el hex y si es una posición potencial
        if (Deployer.readyForDeploymentIcon != null && regimentPosition ==PositionForRegiment.player)
        {
            Deployer.DeployRegiment(parentHex);// despliega un regimiento
        }
    }
    void DisableMe()
    {
        parentHex.CleanUpDeploymentPosition();
    }

}
