using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageMNG : MonoBehaviour
{
    [SerializeField] internal CurrentProgress currentProgress;
    [SerializeField] CharIcon iconPrefab;
    List<CharAttributes> regimentIcons = new List<CharAttributes>();
    ScrollRect scrollRect;
    //// sprites para el fondo
    [SerializeField] internal Sprite selectedIcon;
    [SerializeField] internal Sprite defaultIcon;
    [SerializeField] internal Sprite deployedRegiment;
    public static event Action<CharAttributes> OnRemoveHero;// evento cuando un jugador elimina a un héroe
    public delegate void DeleteHero(CharAttributes SOofHero);
    public static event DeleteHero OnClickOnGrayIcon;
    public CharIcon[] charIcons;//collects all icons

    // Se llama al inicio antes de la primera actualización del cuadro
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        CallHeroIcons();

        // enlaza el método DisableME al botón de marcar al iniciar
        StartBTN.OnStartingBattle += DisableMe;
        charIcons = GetComponentsInChildren<CharIcon>();// recopila todos los iconos
    }
    private void CallHeroIcons()// coloca los iconos de los héroes en el almacenamiento
    {
        regimentIcons = currentProgress.heroesOfPlayer;// acceso a los regimientos del jugador
        Transform parentOfIcons = scrollRect.content.transform;// define el objeto padre para todos los iconos
        for (int i = 0; i < regimentIcons.Count; i++)
        {
            CharIcon fighterIcon = Instantiate(iconPrefab, parentOfIcons);// crear una instancia y devolver un icono
            fighterIcon.charAttributes = regimentIcons[i];// asigna un objeto programable a un icono
            fighterIcon.FillIcon();// llena el icono con un sprite y el número de unidades.
            // datos tomados de un objeto programable
        }
    }
    internal void TintIcon(CharIcon clickedIcon)// marca un regimiento para colocarlo en el campo de batalla
    {
        CharIcon[] charIcons = GetComponentsInChildren<CharIcon>();// recopila todos los iconos
        foreach (CharIcon icon in charIcons)
        {
            if (!icon.charAttributes.isDeployed)
                icon.backGround.sprite = defaultIcon;// establece el fondo predeterminado del icono
        }
        clickedIcon.backGround.sprite = selectedIcon;// establece un fondo verde para el icono
        Deployer.readyForDeploymentIcon = clickedIcon;// Guarda el icono seleccionado en la memoria
    }

    private void RemoveHero(Hero hero)//removes the selected hero
    {
        BattleHex parentHex = hero.GetComponentInParent<BattleHex>();// para acceder al método MakeMeDeploymentPosition
        parentHex.MakeMeDeploymentPosition();// devuelve el hex a la posición inicial
        Destroy(hero.gameObject);// elimina al héroe
    }
    public void RemoveRegiment(CharAttributes SOHero)// llama al evento OnRemoveHero
    {
       OnClickOnGrayIcon(SOHero);
    }
    private void DisableMe()// deshabilita el almacenamiento
    {
        gameObject.SetActive(false);
    }

}
