using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Deployer : MonoBehaviour
{
    public static CharIcon readyForDeploymentIcon;// almacena información sobre el icono en el que el jugador hizo clic
    List<BattleHex> enemiesPositions = new List<BattleHex>();// almacena hexágonos para desplegar enemigos
    List<CharAttributes> enemiesToDeploy = new List<CharAttributes>();// almacena los objetos programables de los enemigos
    static StorageMNG storage;
    int enemiesNum;// para contar el número de objetos enemigos

    void Start()
    {
        ActivatePositionsForRegiments();
        storage = FindObjectOfType<StorageMNG>();
        enemiesToDeploy = storage.currentProgress.enemies;// llena la lista con los objetos programables de los enemigos
        enemiesNum = enemiesToDeploy.Count();// cuenta el número de unidades enemigas
        PlaceEnemies();// coloca unidades enemigas en el campo de batalla
    }
    // El método DeployRegiment crea una instancia del héroe en el campo de batalla
    public static void DeployRegiment(BattleHex parentObject)// el héroe aparece en parentObject
    {
        Hero regiment = readyForDeploymentIcon.charAttributes.heroSO;// obtiene el héroe prefabricado
        Hero fighter = Instantiate(regiment, parentObject.Landscape.transform);// crea una instancia del héroe y
        fighter.GetComponent<Move>().ManageSortingLayer(parentObject);
        
        parentObject.CleanUpDeploymentPosition();// oculta la marca de verificación y deshabilita el colisionador
        readyForDeploymentIcon.HeroIsDeployed();// marca el icono en gris
        readyForDeploymentIcon = null;// borra una variable para evitar que el héroe reaparezca
        storage.GetComponent<StartBTN>().ControlStartBTN();// habilita el botón de inicio
    }
    void ActivatePositionsForRegiments()// muestra la marca de verificación y habilita el colisionador
    {
        foreach (BattleHex hex in FieldManager.allHexesArray)
        {
            if (hex.deploymentPos.regimentPosition == PositionForRegiment.player)// si la variable de un hexadecimal es verdadera entonces
                                                                                 // un hexagono se define como una posición posible
            {
                hex.MakeMeDeploymentPosition();// mostrar la marca de verificación y habilitar los colisionadores
            }
        }
    }
    internal List<BattleHex> GetEnemiesPos()// devuelve una lista con hexágonos para enemigos
    {
        enemiesPositions.Clear();// limpia la lista antes de una nueva iteración
        foreach (BattleHex hex in FieldManager.activeHexList)
        {
            // comprobar si la posición está destinada a una unidad enemiga
            if (hex.deploymentPos.regimentPosition == PositionForRegiment.enemy)
            {
                enemiesPositions.Add(hex);
            }
        }
        return enemiesPositions;
    }

    private void PlaceEnemies()// coloca unidades enemigas en el campo de batalla
    {
        List<BattleHex> enemiesPositions = GetEnemiesPos();// recoge todas las posiciones de los enemigos
        for (int i = 0; i < enemiesNum; i++)// usa el bucle para excluir posiciones ocupadas
        {       
            int positionsNum = enemiesPositions.Count();// actualiza el número de posiciones desocupadas
            int randomIndex = UnityEngine.Random.Range(0, positionsNum - 1);// devuelve un número aleatorio que           
            Image landscape = enemiesPositions[randomIndex].Landscape;// objeto principal para usar el método de instanciar
            InstantiateEnemy(enemiesToDeploy[i], landscape);// crea una instancia de un enemigo
            //prohibiir opcuasion de hexagonos
            enemiesPositions.RemoveAt(randomIndex);
        }
    }
    private void InstantiateEnemy(CharAttributes charAttributes, Image hexPosition)
    {
        Hero enemy = Instantiate(charAttributes.heroSO, hexPosition.transform);// crea una instancia de un enemigo
        enemy.gameObject.AddComponent<Enemy>();// agrega el script Enemy a un objeto héroe definido como enemigo

        // adjunta el script AllPosForGroundAI a un objeto héroe definido como enemigo
        enemy.gameObject.AddComponent<AllPosForGroundAI>();
    }
}
