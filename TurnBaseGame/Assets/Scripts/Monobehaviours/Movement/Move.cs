using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public bool isMoving = false;// habilita y deshabilita el movimiento
    Hero hero;// Referencia del componente Hero
    public List<Image> path;// Imágenes de hexes incluidos en Ruta óptima
    private int totalSteps;// número de hexes incluidos en la ruta óptima
    private int currentStep;// índice de lista que define el objetivo actual para el movimiento
    Vector3 targetPos;// coordenadas del hex definido como el objetivo actual para el movimiento
    float speedOfAnim = 2f;// determina la velocidad de movimiento
    internal bool lookingToTheRight = true;// determina la rotación del héroe
    SpriteRenderer heroSprite;// Referencia del componente SpriteRenderer
    BattleController battleController;
    void Start()
    {
        hero = GetComponent<Hero>();
        heroSprite = GetComponent<SpriteRenderer>();
        battleController = FindObjectOfType<BattleController>();
    }

    void Update()
    {
        if (isMoving)// comienza a moverse si el valor de la variable isMoving es verdadero
            HeroIsMoving();
    }
    public void StartsMoving()
    {
        battleController.events.gameObject.SetActive(false);// inhabilita la respuesta al clic
        battleController.CleanField();// establece el estado inicial para todos los hexes activos
        currentStep = 0;// actualiza el valor de la variable para comenzar con el primer hexadecimal de la ruta óptima
        totalSteps = path.Count - 1;// número de hexes incluidos en la ruta óptima, utilizado como índice
        isMoving = true;// permite el movimiento
        hero.GetComponent<Animator>().SetBool("IsMoving", true);// habilita la animación del movimiento
        ResetTargetPos();// cambia los elementos de la lista de rutas definiendo el siguiente paso
    }
    private void ResetTargetPos()
    {
        targetPos = new Vector3(path[currentStep].transform.position.x,
      path[currentStep].transform.position.y,
      transform.position.z);// define el siguiente paso cambiando el valor de la variable currentStep
        ControlDirection(targetPos);// controla la rotación del héroe
    }
    private void HeroIsMoving()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos,
                        speedOfAnim * Time.deltaTime);// mueve un héroe en las coordenadas dadas
        ManageSteps();
        ManageSortingLayer(path[currentStep].GetComponentInParent   <BattleHex>());
    }
    private void ManageSteps()// cambia el valor de la variable currentStep dependiendo
                              // en la distancia al objetivo actual
    {
        if (Vector3.Distance(transform.position, targetPos) < 0.1f
      && currentStep < totalSteps)// compara las coordenadas de la posición actual del héroe
                                  // y la distancia a la posición actual del objetivo
        {
            currentStep++;// agrega uno al valor de la variable currentStep
            ResetTargetPos();// establece un nuevo hexadecimal objetivo
        }
        else if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            StopsMoving();// detiene el movimiento si el héroe alcanza el punto final del movimiento
        }
    }
    private void StopsMoving()
    {
        isMoving = !isMoving;// invierte el valor de una variable
        transform.parent = path[currentStep].transform;
        hero.GetComponent<Animator>().SetBool("IsMoving", false);// detiene la animación del movimiento
        hero.heroData.CurrentVelocity = 0;
        hero.DefineTargets();
        battleController.events.gameObject.SetActive(true);// habilita la respuesta al clic
    }
    internal void ControlDirection(Vector3 targetPos)
    {
        // compara las coordenadas del héroe y las coordenadas del hex objetivo
        // rota al héroe si es necesario
        if (transform.position.x > targetPos.x && lookingToTheRight ||
            transform.position.x < targetPos.x && !lookingToTheRight)
        {
            heroSprite.flipX = !heroSprite.flipX;// rota un sprite del héroe
            lookingToTheRight = !lookingToTheRight;// establece el valor opuesto para una variable
        }
    }
    internal void ManageSortingLayer(BattleHex targetHex)
    {
        heroSprite = GetComponent<SpriteRenderer>();
        Canvas canvasOfStack = GetComponentInChildren<Canvas>();
        int currentLayer = 16 - targetHex.verticalCoordinate;
        canvasOfStack.sortingOrder = currentLayer+1;
        heroSprite.sortingOrder = currentLayer;
        //hero.stack.GetComponent<Canvas>().sortingOrder = currentLayer;
    }
}
