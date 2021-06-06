using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Hero
{
    IAttacking dealsDamage = new SimpleMeleeAttack();// referencia de comportamiento de ataque simple
    public override void DealsDamage(BattleHex target)
    {
        // // lanza métodos de daño
        dealsDamage.HeroIsDealingDamage(this, BattleController.currentTarget);
    }
public override IAdjacentFinder GetTypeOfHero()
    {
        IAdjacentFinder adjFinder = new PositionsForGround();
        return adjFinder;
    }
    public override void DefineTargets()
    {
        IDefineTarget wayToLookForTargets = new TargetPlayerMelee();
        wayToLookForTargets.DefineTargets(this);
    }
    public override void HeroIsAtacking()// inicia el ataque
    {
        base.HeroIsAtacking();
        GetComponent<Animator>().SetTrigger("isAttacking");
    }
}
