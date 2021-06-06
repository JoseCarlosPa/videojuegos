using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public CurrentProgress currentProgress;// almacena el objeto de la barra
    public List<CharIcon> heroIcons;//stores all icon prefabs
    List<CharAttributes> regimentsSO = new List<CharAttributes>();// almacena todos los objetos programables de héroes
    ScrollRect scrollRect;// utilizado como objeto padre para los iconos
    [SerializeField] CharIcon iconPrefab; //icon prefab

    // para cambiar el texto dependiendo del resultado de la batalla
    [SerializeField] internal TMPro.TextMeshProUGUI VicOrDefeat;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        CallHeroIcons();// crea una instancia de todos los íconos de los héroes que participaron en la batalla
    }

    // cambia el texto dependiendo del resultado de la batalla
    public void DefeatOrVictory(bool victory)
    {
        if (victory)
        {
            VicOrDefeat.text = "Victoria";
        }
        else
        {
            VicOrDefeat.text = "Derrota";
        }
    }
    // crea una instancia de todos los íconos de los héroes que participaron en la batalla
    public void CallHeroIcons()// coloca los iconos de los héroes en el almacenamiento
    {
        regimentsSO = currentProgress.heroesOfPlayer;// acceso a los regimientos del jugador
        Transform parentOfIcons = scrollRect.content.transform;// define el objeto padre para todos los iconos
        for (int i = 0; i < regimentsSO.Count; i++)
        {
            if (regimentsSO[i].isDeployed)
            {
                CharIcon fighterIcon = Instantiate(iconPrefab, parentOfIcons);// crear una instancia y devolver un icono
                fighterIcon.FillIconWhenGameIsOver(regimentsSO[i]);// llena el icono con
                                                                   // datos tomados de un objeto programable
            }
        }
    }
}
