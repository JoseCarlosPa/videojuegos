using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCounter : MonoBehaviour
{
    int totalDamage;// daño causado por todo el regimiento atacante
    int targetTotalHP; // la salud de toda la unidad atacada
    int targetStack;// número de unidades en el regimiento atacado después del ataque
    public int TargetStack
    {
        get { return targetStack; }
        set// excluye el valor de la variable negativa
        {
            if (value > 0) { targetStack = value; }
            else { targetStack = 0; }
        }
    }
    int damagebyUnit;// daño causado por una unidad
    int DamageByUnit// daño causado por una unidad
    {
        get { return damagebyUnit; }
        set// excluye el valor de la variable negativa
        {
            if (value > 0) { damagebyUnit = value; }
            else { damagebyUnit = 1; }// establece el valor en uno si la resistencia es mayor que el ataque
        }
    }

    // calcula el número de unidades en un regimiento atacado después del ataque
    internal int CountTargetStack(Hero currentAtacker, Hero target)
    {
        totalDamage = CountDamageDealt(currentAtacker, target);// asigna el daño infligido a la variable

        // calcula la salud de todo el regimiento después del ataque
        targetTotalHP = target.heroData.HPCurrent * target.heroData.StackCurrent - totalDamage;

        // calcula el número de unidades en un regimiento atacado después del ataque
        TargetStack = targetTotalHP / target.heroData.HPCurrent;
        return targetStack;
    }

    // calcula el daño causado por todo el regimiento atacante
    private int CountDamageDealt(Hero currentAtacker, Hero target)
    {
        // calcula el daño causado por una unidad
        DamageByUnit = currentAtacker.heroData.AtackCurrent - target.heroData.ResistanceCurrent;
        // calcula el daño hecho por todo el regimiento
        int DamageByRegiment = DamageByUnit * currentAtacker.heroData.StackCurrent;
        return DamageByRegiment;
    }
}
