using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fairy : Hero
{
    [SerializeField] DamagingFlyingObject mageBall;// prefabricado de flechas
    [SerializeField] internal Vector3 initialPosCorrection;// ajusta la posición de la flecha
    IAttacking dealsDamage = new FreezingAttack();// referencia de comportamiento de ataque simple


    public override void DealsDamage(BattleHex target)
    {

    }
    public override IAdjacentFinder GetTypeOfHero()
    {
        IAdjacentFinder adjFinder = new PositionsForFlying();
        return adjFinder;
    }
    public override void DefineTargets()
    {
        IDefineTarget wayToLookForTargets = new TargetPlayerRange();
        wayToLookForTargets.DefineTargets(this);
    }
    public override void HeroIsAtacking()// inicia el ataque
    {
        base.HeroIsAtacking();
        GetComponent<Animator>().SetTrigger("isAttacking");// activa la animación
        InstantiateBall();// instancia una flecha
    }
    private void InstantiateBall()// instancia una flecha
    {
        // posición donde aparecerá la flecha
        Vector3 positionForArrow = new Vector3(transform.position.x,
                                 transform.position.y + initialPosCorrection.y, transform.position.z);
        Hero currentTarget = BattleController.currentTarget.GetComponentInChildren<Hero>();

        Quaternion rotation = CalcRotation.CalculateRotation(currentTarget); ;// determina el ángulo de rotación de la flecha
        DamagingFlyingObject ball = Instantiate(mageBall, positionForArrow, rotation, transform);// crea una instancia de un objeto flecha
        ball.FireArrow(dealsDamage);//fires the arrow
    }
}
