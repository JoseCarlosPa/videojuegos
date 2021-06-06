using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharIcon : MonoBehaviour
{
    [SerializeField] internal Image heroImage;// almacena un objeto hijo con un sprite de héroe
    [SerializeField] internal Image backGround;// almacena un objeto hijo con un sprite de fondo
    [SerializeField] internal TMPro.TextMeshProUGUI stackText;// almacena un objeto hijo con un número de pila
    [SerializeField] internal CharAttributes charAttributes;// acceso a los datos del héroe
    StorageMNG storage;
    string losses = "0";
    private void Start()
    {
        storage = GetComponentInParent<StorageMNG>();
        StorageMNG.OnClickOnGrayIcon += ReturnDefaultState; // suscribe el método ReturnDefaultState a un evento OnRemoveHero
    }
    internal void FillIcon()
    {
        heroImage.sprite = charAttributes.heroSprite;
        stackText.text = charAttributes.stack.ToString();
        charAttributes.isDeployed = false;
    }
    // cambia el sprite y muestra pérdidas
    internal void FillIconWhenGameIsOver(CharAttributes Attributes)
    {
        // muestra el sprite del héroe que participó en la batalla
        heroImage.sprite = Attributes.heroSprite;
        // muestra pérdidas
        if (Attributes.Calculatelosses() != 0)
        {
            losses = "- " + Attributes.Calculatelosses();
        }

        stackText.text = losses;
    }
    public void IconClicked()// responde a un clic en un botón
    {
        StorageMNG storage = GetComponentInParent<StorageMNG>();
        if (!charAttributes.isDeployed)// evalúa si la unidad ya está en el campo de batalla
        {
            storage.TintIcon(this);// marca un regimiento para colocarlo en el campo de batalla
        }
        else
        {
            storage.RemoveRegiment(charAttributes);
            //storage.ReturnRegiment(this);//returns the hero to the storage
        }
    }
    public void HeroIsDeployed()
    {
        backGround.sprite = storage.deployedRegiment;
        charAttributes.isDeployed = true;
        
    }
    public void ReturnDefaultState(CharAttributes selectedCharAttributes)// establece un fondo verde claro para el icono
    {
        if (selectedCharAttributes == charAttributes)// determina si el icono debe responder a un evento
        {
            backGround.sprite = GetComponentInParent<StorageMNG>().defaultIcon;// establece el icono verde claro
            charAttributes.isDeployed = false;// define al héroe como disponible para su despliegue
        }
    }
}
