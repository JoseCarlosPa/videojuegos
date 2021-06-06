using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    BattleController battleController;// Referencia de la clase BattleController
    AllPosForGroundAI tocheckTheField;// Referencia del componente AllPosForGroundAI

    public List<BattleHex> PosToOccupy = new List<BattleHex>();// recoge todos los hexágonos que puede ocupar un troll
    List<BattleHex> allTargets = new List<BattleHex>();// la variable se usa para ordenar los regimientos de los jugadores
    List<BattleHex> closeTargets = new List<BattleHex>();// recoge regimientos de jugadores ubicados en la zona de ataque
    BattleHex hexToOccupy;// Hexagonal elegido por el troll para ocuparlo
    AvailablePos availablePos;// acceso a componentes
    Move move;//acceso
    Hero hero;//acceso 
    private void Start()
    {
        battleController = FindObjectOfType<BattleController>();
        tocheckTheField = GetComponent<AllPosForGroundAI>();
        hero = GetComponent<Hero>();
        availablePos = GetComponent<AvailablePos>();
        move = GetComponent<Move>();
        move.lookingToTheRight = false;// para hacer que el troll mire en la dirección correcta
    }
    public void Aisturnbegins(IInitialHexes getInitialHexes)
    {
        int stepsLimit = battleController.stepsToCheckWholeField;// obtiene la velocidad actual del atacante
        BattleHex startintHex = GetComponentInParent<BattleHex>();

        // comienza a buscar todas las unidades del jugador
        tocheckTheField.GetAvailablePositions(stepsLimit, getInitialHexes, startintHex);
        CollectAllPosToOccupy();
        AIMakesDecision();
    }
    List<BattleHex> CollectAllPosToOccupy()
    {
        PosToOccupy.Clear();
        foreach (BattleHex hex in FieldManager.activeHexList)
        {
            if (hex.distanceText.distanceFromStartingPoint <= hero.heroData.CurrentVelocity)
            {
                PosToOccupy.Add(hex);
            }
        }
        return PosToOccupy;
    }


    private List<BattleHex> CheckIfAttackIsAvailable()// comprueba si el regimiento del jugador está en la zona de ataque
    {
        int currentVelocity = BattleController.currentAtacker.heroData.CurrentVelocity;
        closeTargets.Clear();
        // asigna todas las unidades de jugador a una lista
        List<BattleHex> allTargets =battleController. IsLookingForPotentialTargets();
        foreach (BattleHex hex in allTargets)
        {
            // comprueba si el hexágono está en la zona de ataque
            // El luchador cuerpo a cuerpo puede atacar después de moverse, la distancia de ataque es de 1 hex.
            if (hex.distanceText.distanceFromStartingPoint <= currentVelocity + 1)
            {
                closeTargets.Add(hex);
            }
        }
        return closeTargets;
    }
    public BattleHex AISelectsTargetToAttack()// selecciona un objetivo prioritario para el ataque
    {
        allTargets.Clear();// limpia la lista antes de llenarla
        if (CheckIfAttackIsAvailable().Count > 0)
        {
            // ordena todos los regimientos de los jugadores por salud en orden ascendente
            allTargets = CheckIfAttackIsAvailable().
                         OrderBy(hero => hero.GetComponentInChildren<Hero>().heroData.HPCurrent).ToList();
        }
        else
        {
            // ordena los regimientos de todos los jugadores primero por la distancia al objetivo, luego por la propiedad de HP
            allTargets = battleController. IsLookingForPotentialTargets().OrderBy(hero => hero.distanceText.distanceFromStartingPoint).
                        ThenBy(hero => hero.GetComponentInChildren<Hero>().heroData.HPCurrent).ToList();
        }
        BattleController.currentTarget = allTargets[0].GetComponentInChildren<Hero>();
        return allTargets[0];
    }
    void AIIStartsMoving(BattleHex targetToAttack)// determina la distancia desde el objetivo del ataque a cada hex
    {
        battleController.CleanField();// borrar las propiedades de todos los hexes antes de un nuevo cálculo
        targetToAttack.DefineMeAsStartingHex();// el hex de inicio es el hex ocupado por el objetivo del ataque
        int stepsLimit = battleController.stepsToCheckWholeField;// número de pasos suficientes para comprobar todo el campo de batalla
        IInitialHexes getInitialHexes = new InitialPos();// cada regla de validación hexadecimal

        // determina la distancia desde el objetivo del ataque a cada hex
        tocheckTheField.GetAvailablePositions(stepsLimit, getInitialHexes, targetToAttack);
        IAdjacentFinder adjFinder = BattleController.currentAtacker.GetTypeOfHero();// determina el tipo de movimiento
        AIDefinesPath(adjFinder);// determina la ruta óptima y comienza a moverse
    }
    private BattleHex AISelectsPosToOcuppy()// define el punto final del movimiento
    {
        // ordena todas las posiciones que el troll puede ocupar por la distancia al objetivo de ataque
        List<BattleHex> OrderedPos = PosToOccupy.OrderBy(s => s.distanceText.distanceFromStartingPoint).ToList();
        for (int i = 0; i < OrderedPos.Count; i++)
        {

            if (OrderedPos[i].GetComponentInChildren<Hero>() == null)
            {
                hexToOccupy = OrderedPos[i];
                // finaliza la ejecución del bucle for tan pronto como se determina el hex más cercano al objetivo del ataque
                break;
            }
        }

        return hexToOccupy;
    }
    void AIMakesDecision()// la computadora elige si debe atacar o moverse
    {
        BattleHex targetToAttack = AISelectsTargetToAttack();// asigna un objetivo prioritario para el ataque
                                                             // comprueba si el objetivo del ataque está en uno de los hexágonos vecinos
        if (targetToAttack.distanceText.distanceFromStartingPoint > 1)//si no
        {
            AIIStartsMoving(targetToAttack);// la IA comienza a moverse
        }
        else
        {
            hero.HeroIsAtacking();// ataca inmediatamente
        }
    }
    void AIDefinesPath(IAdjacentFinder adjFinder)// determina la ruta óptima y comienza a moverse
    {
        // asigna un valor a la variable estática utilizada por las clases Move y Optimal Path
        BattleController.targetToMove = AISelectsPosToOcuppy();
        battleController.CleanField();// limpia todo el campo de batalla de los cálculos anteriores
        IInitialHexes getInitialHexes = new InitialPos();// cada regla de validación hexadecimal
        int stepsLimit = hero.heroData.CurrentVelocity;
        BattleHex startingHex = BattleController.currentAtacker.GetComponentInParent<BattleHex>();
        startingHex.DefineMeAsStartingHex();// marca un nuevo hexadecimal inicial

        // selecciona todos los hexes que puede ocupar el troll
        availablePos.GetAvailablePositions(stepsLimit, adjFinder, getInitialHexes);
        GetComponent<OptimalPath>().MatchPath();// determina la ruta óptima
        move.StartsMoving();// El troll comienza a moverse
    }
}
