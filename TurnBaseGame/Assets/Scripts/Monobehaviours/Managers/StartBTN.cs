using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartBTN : MonoBehaviour
{
    // hace que otros objetos respondan al hacer clic en el botón Inicio
    public delegate void StartsBattle();
    public static event StartsBattle OnStartingBattle;
    [SerializeField] Button startBTN;    // almacena el objeto del botón de inicio
    StorageMNG storage;
    [SerializeField] int minimumHeroesNum;// conjunto mínimo de regimientos para activar la batalla

    private void Start()
    {
        storage = GetComponent<StorageMNG>();
        OnStartingBattle += ControlStartBTN;
        startBTN.gameObject.SetActive(false);// deshabilita el botón Inicio antes de la implementación
    }

    public void StartBattle()// el método se ejecuta cuando se presiona el botón de inicio
    {
        OnStartingBattle();// llama al delegado
    }

    public void ControlStartBTN()// habilita y deshabilita el botón de inicio
    {
        int deployedReg = GetGrayIcons();// el número de regimientos desplegados

                                         // compara el número de unidades en el campo de batalla con el número mínimo requerido
        if (deployedReg >= minimumHeroesNum)
        {
            startBTN.gameObject.SetActive(true);// habilita el botón de inicio
        }
        else
        {
            startBTN.gameObject.SetActive(false);// deshabilita el botón de inicio
        }
    }
    int GetGrayIcons()// cuenta el número de regimientos desplegados en el campo de batalla
    {
        int grayIcons = 0;// empezar con cero en caso de uso múltiple del método
        foreach (CharIcon icon in storage.charIcons)// todos los iconos se almacenan en la clase de almacenamiento
        {
            if (icon.charAttributes.isDeployed)
            {
                grayIcons++;// agrega uno al número si el regimiento está desplegado
            }
        }
        return grayIcons;
    }
}
