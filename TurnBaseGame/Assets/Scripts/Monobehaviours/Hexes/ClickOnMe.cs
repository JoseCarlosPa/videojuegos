using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOnMe : MonoBehaviour, IPointerClickHandler
{
    BattleHex hex;
    public bool isTargetToMove = false;// se vuelve cierto cuando se hace clic en el hex
    public FieldManager fieldManager;
    BattleController battleController;

    void Awake()
    {
        hex = GetComponent<BattleHex>();
        fieldManager = FindObjectOfType<FieldManager>();
        battleController = FindObjectOfType<BattleController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (hex.potencialTarget)
        {
            battleController.events.gameObject.SetActive(false);// inhabilita la respuesta al clic
            BattleController.currentTarget = this.GetComponentInChildren<Hero>();
            BattleController.currentAtacker.HeroIsAtacking();
            return;
        }
        if (!isTargetToMove)
        SelectTargetToMove();
       else
        {
            BattleController.currentAtacker.GetComponent<Move>().StartsMoving();
        }
    }

    private void SelectTargetToMove()
    {
        ClearPreviousSelectionOfTargetHex();
        if (hex.isNeighboringHex)
        {
            hex.MakeMeTargetToMove();
            BattleController.currentAtacker.GetComponent<OptimalPath>().MatchPath();
        }
    }

    public void ClearPreviousSelectionOfTargetHex()// Cancela la selección anterior
    {
        foreach (BattleHex hex in FieldManager.activeHexList)// busca el hex seleccionado en la lista de hexes activos
        {
            if (hex.clickOnMe.isTargetToMove == true)// evalúa hexadecimal si es objetivo
            {
                hex.GetComponent<ClickOnMe>().isTargetToMove = false;// anula el booleano
                hex.MakeMeAvailable();// establece marco blanco
            }
            hex.Landscape.color = new Color32(250, 250, 250, 250);
        }
    }
}



