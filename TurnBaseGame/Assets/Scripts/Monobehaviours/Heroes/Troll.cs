using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : Hero
{
    IAttacking dealsDamage = new SimpleMeleeAttack();// referencia de comportamiento de ataque simple
    public override void DealsDamage(BattleHex target)
    {
        dealsDamage.HeroIsDealingDamage(this, BattleController.currentTarget);
    }
    public override IAdjacentFinder GetTypeOfHero()
    {
        IAdjacentFinder adjFinder = new PosForGroundAI();
        return adjFinder;
    }
    public override void DefineTargets()
    {
        BattleHex initialHex = GetComponentInParent<BattleHex>();// hex ocupado por el troll
        IEvaluateHex checkHex = new IfItIsTarget();// regla de validación de hexes vecinos

        // recopilar objetivos potenciales
        List<BattleHex> neighboursToCheck = NeighboursFinder.GetAdjacentHexes(initialHex, checkHex);
        if (neighboursToCheck.Count > 0)// si hay un objetivo en el hex adyacente
        {
            HeroIsAtacking();// ataca al objetivo
        }
        else { turn.TurnIsCompleted(); }// se completa el turno
    }
    public override void HeroIsAtacking()// inicia el ataque
    {
        base.HeroIsAtacking();// Ejecuta el código especificado en el método de la clase principal
        // lanza el clip de animación isAttacking
        GetComponent<Animator>().SetTrigger("isAttacking");
    }

}
