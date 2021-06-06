using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    BattleController battleController;
    IInitialHexes getInitialHexes = new InitialPos();
    //public delegate void StartNewTurn();
    //public static event StartNewTurn OnNewTurn;
    public delegate void StartNewRound();
    public static event StartNewRound OnNewRound;
    [SerializeField] GameOver gameOverPanel; // Panel prefabricado
    FieldManager parent; //// Accediendo al objeto HexFiels

    private void Start()
    {
        battleController = GetComponent<BattleController>();
        // se inicializa un nuevo turno presionando el botón de inicio
        StartBTN.OnStartingBattle += InitializeNewTurn;
        parent = FindObjectOfType<FieldManager>();
    }
    public void InitializeNewTurn()
    {
        battleController.CleanField();
        battleController.DefineNewAtacker();// encuentra un héroe atacante
        Hero currentAtacker = BattleController.currentAtacker;// obtiene atacante local (para parámetros)
        GetStartingHex();
        if (currentAtacker.GetComponent<Enemy>() == null)// comprueba si es el turno de un jugador
        {
            IInitialHexes getInitialHexes = new InitialPos();
            currentAtacker.PlayersTurn(getInitialHexes);// El jugador comienza su turno

        }
        // El jugador comienza su turno
        else
        {
            IInitialHexes getInitialHexes = new InitialPosAI();
            currentAtacker.GetComponent<Enemy>().Aisturnbegins(getInitialHexes);
        }
    }
    // devuelve el hex en el que se encuentra el héroe atacante
    private void GetStartingHex()
    {
        BattleHex startingHex = BattleController.currentAtacker.GetComponentInParent<BattleHex>();
        startingHex.DefineMeAsStartingHex(); // cambia las propiedades del hexagono inicial

    }
    public void TurnIsCompleted()
    {
       
        StartCoroutine(NextTurnOrGameOver());
    }
    public IEnumerator NextTurnOrGameOver()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);// duración de la pausa
        yield return wait;
        battleController.events.gameObject.SetActive(true);// habilita la respuesta al clic
        List<Hero> allFighters = battleController.DefineAllFighters();
        if (IfThereIsAIRegiment(allFighters) && IfThereIsPlayerRegiment(allFighters))
        {
            NextTurnOrNextRound(allFighters);
        }
        else
        {

            battleController.CleanField();// limpiando el campo de batalla de marcos y números
            GameOver GameOver = Instantiate(gameOverPanel, parent.transform);
            GameOver.DefeatOrVictory(IfThereIsPlayerRegiment(allFighters));// muestra un mensaje de victoria o derrota
            RemoveAllHeroes(allFighters);

        }
    }
    private void RemoveAllHeroes(List<Hero> allFighters)// elimina a todos los héroes del campo de batalla
    {
        foreach (Hero hero in allFighters)
        {
            Destroy(hero.gameObject);// elimina a todos los héroes que quedan en el campo de batalla
        }
    }
    bool IfThereIsAIRegiment(List<Hero> allFighters)
    {
        return allFighters.Exists(x => x.gameObject.GetComponent<Enemy>());
    }
     bool IfThereIsPlayerRegiment(List<Hero> allFighters)
    {
        return allFighters.Exists(x => !x.gameObject.GetComponent<Enemy>());
    }
    private void NextTurnOrNextRound(List<Hero> allFighters)
    {
        if (allFighters.Exists(x => x.heroData.InitiativeCurrent > 0))
        {
            InitializeNewTurn();
        }
        else
        {
            OnNewRound();
            InitializeNewTurn();
        }
    }
}
