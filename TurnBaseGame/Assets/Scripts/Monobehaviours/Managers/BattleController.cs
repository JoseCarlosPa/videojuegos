using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleController : MonoBehaviour
{
    public static BattleHex targetToMove;
    public static Hero currentAtacker;
    public static Hero currentTarget;// Almacena información sobre quién está siendo atacado
    List<Hero> allFighters = new List<Hero>();// Recoge todos los luchadores colocados en el campo de batalla
    public int stepsToCheckWholeField;// número de iteraciones para comprobar todo el campo de batalla
    public List<BattleHex> potentialTargets = new List<BattleHex>();// recoge los regimientos de todos los jugadores
    Turn turn;
    public EventSystem events;// para deshabilitar la respuesta al clic
    private void Start()
    {
        turn = GetComponent<Turn>();
        events = FindObjectOfType<EventSystem>();
    }

    // Recoge todos los luchadores colocados en el campo de batalla
    public List<Hero> DefineAllFighters()
    {
        allFighters = FindObjectsOfType<Hero>().ToList();
        return allFighters;
    }
    public void DefineNewAtacker()
    {
        // clasifica a los luchadores por valor de iniciativa, en orden descendente
        List<Hero> allFighters = DefineAllFighters().
                                 OrderByDescending(hero => hero.heroData.InitiativeCurrent).ToList();
        // el primer elemento de la lista tiene el mayor valor de iniciativa
        currentAtacker = allFighters[0];
        currentAtacker.heroData.InitiativeCurrent = 0;
    }
    public void CleanField()
    {
        foreach (BattleHex hex in FieldManager.activeHexList)
        {
            hex.SetDefaultValue();
        }
    }
    public void RemoveHeroWhenItIsKilled(Hero hero)
    {
        print("killed");
        Destroy(hero.gameObject);
        turn.TurnIsCompleted();
    }
   public List<BattleHex> IsLookingForPotentialTargets()// reúne todos los regimientos de los jugadores en una lista
    {
        potentialTargets.Clear();
        foreach (BattleHex hex in FieldManager.activeHexList)
        {
            // comprueba si el hex está marcado como ocupado por el regimiento de un jugador
            if (hex.potencialTarget)
            {
                potentialTargets.Add(hex);
            }
        }
        return potentialTargets;
    }

}
