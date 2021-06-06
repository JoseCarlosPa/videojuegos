using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    public int velocity = 5;
    public CharAttributes heroData;
    StartBTN startBTN;
    public Stack stack;// almacenamiento en caché de variables para un objeto hijo
    Move moveCpmnt;
    BattleController battleController;
    internal Turn turn;
    private void Awake()
    {
        heroData.SetCurrentAttributes();// carga las características actuales del héroe
        moveCpmnt = GetComponent<Move>();
        battleController = FindObjectOfType<BattleController>();
        turn = FindObjectOfType<Turn>();
    }
    private void Start()
    {
        StorageMNG.OnClickOnGrayIcon += DestroyMe;// suscribe el método DestroyMe a un evento OnRemoveHero
        startBTN = FindObjectOfType<StartBTN>();
        stack = GetComponentInChildren<Stack>();// asignar un objeto hijo a una variable
        Turn.OnNewRound += heroData.SetDefaultVelocityAndInitiative;
    }
    public abstract void DealsDamage(BattleHex target);

    private void DestroyMe(CharAttributes SOHero)// destruye este objeto
    {
        if (SOHero == heroData)// compara la elección del jugador con el héroe
        {
            BattleHex parentHex = GetComponentInParent<BattleHex>();
            parentHex.MakeMeDeploymentPosition();
            startBTN.ControlStartBTN();/// comprueba si es hora de ocultar el botón Inicio
            Destroy(gameObject);
        }
    }
    void OnDisable()// Se activa cuando el objeto es destruido o deshabilitado
    {
        StorageMNG.OnClickOnGrayIcon -= DestroyMe;// se da de baja de las notificaciones
    }
    public abstract IAdjacentFinder GetTypeOfHero();// determina el tipo de movimiento
    public abstract void DefineTargets();
    public virtual void HeroIsAtacking()// inicia un ataque
    {
        Vector3 targetPos = BattleController.currentTarget.transform.position;
        moveCpmnt.ControlDirection(targetPos);// gira al héroe hacia el objetivo
    }
    public void PlayersTurn(IInitialHexes getInitialHexes)
    {
        IAdjacentFinder adjFinder = GetTypeOfHero();// determina el tipo de movimiento
        int stepsLimit = heroData.CurrentVelocity;// obtiene la velocidad actual del atacante

        // determina posibles posiciones para el regimiento de un jugador
        GetComponent<AvailablePos>().GetAvailablePositions(stepsLimit, adjFinder, getInitialHexes);
        DefineTargets();// muestra objetivos potenciales
    }
    public void HeroIsKilled()
    {
        Turn.OnNewRound -= heroData.SetDefaultVelocityAndInitiative;
        battleController.RemoveHeroWhenItIsKilled(this);
    }
}
