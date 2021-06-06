using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Hero
{
    [SerializeField] DamagingFlyingObject arrow;// prefabricado de flechas
    [SerializeField] internal Vector3 initialPosCorrection;
    IAttacking dealsDamage = new SimpleMeleeAttack();
    public override void DealsDamage(BattleHex target)
    {

    }

    public override IAdjacentFinder GetTypeOfHero()
    {
        IAdjacentFinder adjFinder = new PositionsForGround();
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
        GetComponent<Animator>().SetTrigger("isAttacking");//activar animacion
        InstantiateArrow();//instantiates una felcha
    }
    private void InstantiateArrow()//instantiates una felcha
    {
        // posición donde aparecerá la flecha
        Vector3 positionForArrow = new Vector3(transform.position.x,
                                 transform.position.y + initialPosCorrection.y, transform.position.z);
        Hero currentTarget = BattleController.currentTarget.GetComponentInChildren<Hero>();
  
        Quaternion rotation = CalcRotation.CalculateRotation(currentTarget); ;// determina el ángulo de rotación de la flecha
        DamagingFlyingObject Arrow = Instantiate(arrow, positionForArrow, rotation, transform);// crea una instancia de un objeto flecha
        Arrow.FireArrow(dealsDamage);//fires the arrow
    }
}
