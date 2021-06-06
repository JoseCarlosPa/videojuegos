using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingAttack : MonoBehaviour, IAttacking
{
    DamageCounter damageController = new DamageCounter();// acceso al cálculo de daños
    int targetStack;// números finales después del ataque
    public void HeroIsDealingDamage(Hero atacker, Hero Target)
    {
        // calcula el número final de unidades en el regimiento del héroe atacado
        targetStack = damageController.CountTargetStack(atacker, Target);
        int currentInt = Target.heroData.StackCurrent;
        Freeze(Target);
        // asigna un nuevo valor al número de unidades del héroe atacado
        Target.heroData.StackCurrent = targetStack;
        Target.stack.StartCoroutine(Target.stack.CountDownToTargetStack(currentInt, targetStack));
    }

    void Freeze(Hero Target)
    {
        print(2323);
        Target.heroData.InitiativeCurrent = 0;
        Target.GetComponent<SpriteRenderer>().color = new Color32(5, 255, 255, 255);
    }
}
