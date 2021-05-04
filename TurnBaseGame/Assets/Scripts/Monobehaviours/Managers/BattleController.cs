using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleHex targetToMove;
    public static Hero currentAtacker;
    public static Hero currentTarget;//Stores information about who is being attacked
    List<Hero> allFighters = new List<Hero>(); //collects all fighters placed on the battlefield

    //collects all fighters placed on the battlefield
    public List<Hero> DefineAllFighters()
    {
        allFighters = FindObjectsOfType<Hero>().ToList();
        return allFighters;
    }
    public void DefineNewAtacker()
    {
        //   sorts fighters by initiative value, in descending order
        List<Hero> allFighters = DefineAllFighters().
                                 OrderByDescending(hero => hero.heroData.InitiativeCurrent).ToList();
        //  the first element of the list has the biggest initiative value
        currentAtacker = allFighters[0];
    }
    public void CleanField()
    {
        foreach (BattleHex hex in FieldManager.activeHexList)
        {
            hex.SetDefaultValue();
        }
    }

}
