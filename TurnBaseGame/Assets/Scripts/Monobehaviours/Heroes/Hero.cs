using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    public int velocity = 5;
    public CharAttributes heroData;
    StartBTN startBTN;
    public Stack stack;//variable caching for a child object
    Move moveCpmnt;
    private void Awake()
    {
        heroData.SetCurrentAttributes();//loads the current characteristics of the hero
        moveCpmnt = GetComponent<Move>();
    }
    private void Start()
    {
        StorageMNG.OnClickOnGrayIcon += DestroyMe; //subscribes the DestroyMe method to an OnRemoveHero event
        startBTN = FindObjectOfType<StartBTN>();
        stack = GetComponentInChildren<Stack>();//assigning a child object to a variable
    }
    public abstract void DealsDamage(BattleHex target);

    private void DestroyMe(CharAttributes SOHero)//destroys this object
    {
        if (SOHero == heroData)// compares the player’s choice with the hero
        {
            BattleHex parentHex = GetComponentInParent<BattleHex>();
            parentHex.MakeMeDeploymentPosition();
            startBTN.ControlStartBTN();//checks if it's time to hide the Start button
            Destroy(gameObject);
        }
    }
    void OnDisable()//It is activated when the object is destroyed or disabled
    {
        StorageMNG.OnClickOnGrayIcon -= DestroyMe;//unsubscribes from notifications
    }
    public abstract IAdjacentFinder GetTypeOfHero();//determines the type of movement
    public abstract void DefineTargets();
    public virtual void HeroIsAtacking()//starts an attack
    {
        Vector3 targetPos = BattleController.currentTarget.transform.position;
        moveCpmnt.ControlDirection(targetPos);//rotates the hero towards the target
    }
}
