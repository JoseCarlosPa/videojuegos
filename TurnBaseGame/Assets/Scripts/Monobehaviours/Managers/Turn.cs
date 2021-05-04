using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    BattleController battleController;
    IInitialHexes getInitialHexes = new InitialPos();

    private void Start()
    {
        battleController = GetComponent<BattleController>();
        IInitialHexes getInitialHexes = new InitialPos();
        //a new turn is initialized by pressing the start button
        StartBTN.OnStartingBattle += InitializeNewTurn;
    }
    public void InitializeNewTurn()
    {
        battleController.DefineNewAtacker();//finds an attacking hero
        Hero currentAtacker = BattleController.currentAtacker;//gets local atacker (for parameters)
        IAdjacentFinder adjFinder = currentAtacker.GetTypeOfHero();//determines the type of movement
        int stepsLimit = currentAtacker.heroData.CurrentVelocity;//gets current velocity of the atacker
        GetStartingHex();
        //determines possible positions
        currentAtacker.GetComponent<AvailablePos>().GetAvailablePositions(stepsLimit, adjFinder, getInitialHexes);

        currentAtacker.DefineTargets();//displays potencial targets
    }
    //returns the hex on which the attacking hero stands
    private void GetStartingHex()
    {
        BattleHex startingHex = BattleController.currentAtacker.GetComponentInParent<BattleHex>();
        startingHex.DefineMeAsStartingHex(); //changes the properties of the starting hex

    }
}
