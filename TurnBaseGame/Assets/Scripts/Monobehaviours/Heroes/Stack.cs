using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stack : MonoBehaviour
{
    Hero parentHero;// se refiere al objeto Hero padre
    public TextMeshProUGUI stackText; // se refiere al componente TMPro
    private int stack;// propiedad para mostrar el número de unidad
    [SerializeField] float iterationCntrl;// muestra con qué frecuencia reduciremos el regimiento
    int iterationVal;   // valor de reducción del regimiento por unidad de tiempo
    Turn turn;
    public int IterationVal
    {
        get { return iterationVal; }
        set// elimina el redondeo a cero
        {
            if (value < 1) { iterationVal = 1; }
            else { iterationVal = value; }
        }
    }
    void Start()
    {
        parentHero = GetComponentInParent<Hero>();
        stackText = GetComponent<TextMeshProUGUI>();
        DisplayCurrentStack(parentHero.heroData.StackCurrent);// muestra el número inicial de unidades en un regimiento
        turn = FindObjectOfType<Turn>();
    }

    public void DisplayCurrentStack(int currentStack)// muestra el número inicial de unidades en un regimiento
    {
        // toma el valor del número inicial de unidades del objeto programable
        parentHero.heroData.StackCurrent = currentStack;
        stackText.text = currentStack.ToString();// muestra el número de unidades
    }
 
    public IEnumerator CountDownToTargetStack(int currentValue, int targetValue)
    {
        int diff = currentValue - targetValue;//El daño está hecho
        // cálculo de la disminución del tamaño de la pila
        IterationVal = Mathf.FloorToInt(diff * Time.deltaTime / iterationCntrl);
        WaitForSeconds wait = new WaitForSeconds(0.01f);// duración de la pausa
        // agrega IterationVal para evitar un resultado negativo
        while (currentValue >= targetValue + IterationVal)
        {
            currentValue -= IterationVal;// reducir el tamaño de la pila
            DisplayCurrentStack(currentValue);// muestra los cambios realizados
            yield return wait;
        }
        DisplayCurrentStack(targetValue);// ajuste del valor final de la pila
        CheckIfHeroIsKilled();
    }
    void CheckIfHeroIsKilled()
    {

        if (parentHero.heroData.StackCurrent == 0)
        {
            parentHero.GetComponent<Animator>().SetTrigger("IsDead");
        }
        else
        {
            turn.TurnIsCompleted();
        }
    }
}
