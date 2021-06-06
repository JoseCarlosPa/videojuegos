using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HexState { inactive, active };
public class BattleHex : MonoBehaviour
{
    public int horizontalCoordinate;
    public int verticalCoordinate;
    public HexState battleHexState;
    public bool isSecondLevel = false;
    public ClickOnMe clickOnMe;
    public Image Landscape;
    public Distance distanceText;// acceso al objeto DistanceText
    public DeploymentPos deploymentPos;// acceso al objeto DeploymentPos
    [SerializeField] protected Image currentState;
    public bool isStartingHex = false;
    public bool isNeighboringHex = false;// Ayuda a definir un hex como vecino a evaluado
    public bool isIncluded = false;// ayuda a definir un hex como posición disponible
    public bool potencialTarget;// ayuda a identificar posibles objetivos
    public bool lookingForTarget;// ayuda a identificar posibles objetivos

    private void Awake()
    {
        clickOnMe = GetComponent<ClickOnMe>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MakeMeActive()// establece el estado activo en este hexagono
    {
        battleHexState = HexState.active;
    }
    public void MakeMeInActive()// establece el estado inactivo en este hexagono
    {
        if (battleHexState != HexState.active)// excluye los hexes activos modificados manualmente
        {
            Landscape.color = new Color32(120, 120, 120, 250);// establece un nuevo color en un hexadecimal inactivo
        }

    }
    public virtual void MakeMeAvailable()
    {
        currentState.sprite = clickOnMe.fieldManager.availableToMove;// establece el marco blanco en hexagano
        currentState.color = new Color32(255, 255, 255, 255);
    }
    public virtual void MakeMeTargetToMove()// define un hexadecimal como posición seleccionada
    {
        clickOnMe.isTargetToMove = true;
        BattleController.targetToMove = this;
        currentState.sprite = clickOnMe.fieldManager.availableAsTarget;// establece el marco verde en hexagano
    }
    public void DefineMeAsStartingHex()// define este hex como posición inicial
    {
        distanceText.distanceFromStartingPoint = 0;
        isStartingHex = true;
        distanceText.stepsToGo = 1;// para salir de desiertos y pantanos
    }
    public virtual bool AvailableToGround()
    {
        return true;
    }
    public void MakeMeDeploymentPosition()// muestra el hex como una posición potencial para el héroe
    {
        deploymentPos.GetComponent<PolygonCollider2D>().enabled = true;// habilita colisionador (y hacer clic)
        deploymentPos.GetComponent<Image>().color = new Color32(255, 255, 255, 255);// muestra una marca de verificación
    }
    public void CleanUpDeploymentPosition()// oculta una marca de verificación, deshabilita el colisionador
    {
        deploymentPos.GetComponent<PolygonCollider2D>().enabled = false;// deshabilita el colisionador (y haciendo clic)
        deploymentPos.GetComponent<Image>().color = new Color32(255, 255, 255, 0);// oculta una marca de verificación
    }
    internal void DefineMeAsPotencialTarget()// define hexadecimal como un objetivo potencial para el ataque
    {
        currentState.color = new Color(255, 0, 0, 255);// muestra un marco rojo
        potencialTarget = true;
    }
    public void SetDefaultValue()// devuelve el hexadecimal al estado inicial
    {
        isStartingHex = false;
        isNeighboringHex = false;
        isIncluded = false;
        lookingForTarget = false;
        distanceText.GetComponent<Text>().color = new Color32(255, 255, 255, 0);//esconder numberos
        currentState.color = new Color32(255, 255, 255, 0);//esconder el frame
        Landscape.color = new Color32(255, 255, 255, 255);//esconder el camino mas optimo
        distanceText.distanceFromStartingPoint = distanceText.defaultDistance;
        distanceText.stepsToGo = distanceText.defaultstepsToGo;
        potencialTarget = false;
    }
}
    
