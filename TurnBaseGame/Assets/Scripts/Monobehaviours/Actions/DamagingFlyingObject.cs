using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingFlyingObject : MonoBehaviour
{
    internal Vector3 targetPosition;// El lugar donde está el enemigo
    [SerializeField] Vector3 targetPosAdj;// ajustando el punto donde la flecha golpea al enemigo
    internal bool ArrowFlies = false;// activa o desactiva el movimiento
    [SerializeField] float velocity;// velocidad de vuelo
    IAttacking dealsDamage = new SimpleMeleeAttack();// referencia de comportamiento de ataque simple

    void Update()
    {
        if (ArrowFlies) // activa o desactiva el movimiento
        {
            transform.position = Vector2.MoveTowards(transform.position,
                targetPosition, velocity * Time.deltaTime);// mueve un objeto hacia el objetivo

            // detiene el movimiento si el objeto está muy cerca del objetivo
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                ArrowFlies = false;//para

                //asegura el daño recibido
                Hero currentTarget = BattleController.currentTarget;
                dealsDamage.HeroIsDealingDamage(BattleController.currentAtacker, currentTarget);
                currentTarget.GetComponent<Animator>().SetTrigger("IsTakingDamage");
                DestroyMe();// destruye una flecha después de infligir daño al objetivo
            }
        }
    }
    public void FireArrow(IAttacking attackMethod)// comienza a mover la flecha hacia el objetivo
    {
        Vector3 currentTargetPos = BattleController.currentTarget.transform.position;
        targetPosition = currentTargetPos + targetPosAdj;// aclara las coordenadas del objetivo
        dealsDamage = attackMethod;
        ArrowFlies = true;// comienza a mover la flecha
    }
    private void DestroyMe()
    {
        Destroy(gameObject);// destruye esta flecha
    }
}
