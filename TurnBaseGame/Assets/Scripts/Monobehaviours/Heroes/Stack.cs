using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stack : MonoBehaviour
{
    Hero parentHero;//refers to the parent Hero object
    private TextMeshProUGUI stackText; //refers to the TMPro component
    private int stack;//property to display the number of unit    
    [SerializeField] float iterationCntrl;//shows how often we will reduce the regiment
    int iterationVal;   //regiment reduction value per unit of time
    public int IterationVal
    {
        get { return iterationVal; }
        set//eliminates rounding to zero
        {
            if (value < 1) { iterationVal = 1; }
            else { iterationVal = value; }
        }
    }
    void Start()
    {
        parentHero = GetComponentInParent<Hero>();
        stackText = GetComponent<TextMeshProUGUI>();
        DisplayCurrentStack(parentHero.heroData.StackCurrent);//displays the initial number of units in a regiment
    }

    public void DisplayCurrentStack(int currentStack)//displays the initial number of units in a regiment
    {
        //takes the value of the initial number of units from the scriptable object
        parentHero.heroData.StackCurrent = currentStack;
        stackText.text = currentStack.ToString();//displays the number of units
    }
    //public IEnumerator Example(int currentValue, int targetValue)
    //{
    //    int diff = currentValue - targetValue;//damage done

    //    //calculation of decrease in stack size
    //    IterationVal = Mathf.FloorToInt(diff * Time.deltaTime / iterationCntrl);
    //    //add IterationVal to avoid negative result
    //    while (currentValue >= targetValue + IterationVal)
    //    {
    //        currentValue -= IterationVal;//reduce the stack size
    //        DisplayCurrentStack(currentValue);//display changes made
    //        yield return null;
    //    }
    //    DisplayCurrentStack(targetValue);//adjustment of the final stack value
    //}
    public IEnumerator CountDownToTargetStack(int currentValue, int targetValue)
    {
        int diff = currentValue - targetValue;//damage done
        //calculation of decrease in stack size
        IterationVal = Mathf.FloorToInt(diff * Time.deltaTime / iterationCntrl);
        WaitForSeconds wait = new WaitForSeconds(0.01f);//pause length
        //add IterationVal to avoid negative result
        while (currentValue >= targetValue + IterationVal)
        {
            currentValue -= IterationVal;//reduce the stack size
            DisplayCurrentStack(currentValue);//display changes made
            yield return wait;
        }
        DisplayCurrentStack(targetValue); //adjustment of the final stack value
    }
}
