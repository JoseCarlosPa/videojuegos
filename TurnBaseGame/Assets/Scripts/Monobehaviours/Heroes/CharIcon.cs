using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharIcon : MonoBehaviour
{
    [SerializeField] internal Image heroImage;//stores a child object with a hero's sprite
    [SerializeField] internal Image backGround;//stores a child object with a background sprite
    [SerializeField] internal TMPro.TextMeshProUGUI stackText;//stores a child object with a number of stack
    [SerializeField] internal CharAttributes charAttributes;//access to hero data
    internal bool deployed = false;//evaluates if the unit is already on the battlefield
                                   // Start is called before the first frame update
    StorageMNG storage;
    private void Start()
    {
        storage = GetComponentInParent<StorageMNG>();
        StorageMNG.OnClickOnGrayIcon += ReturnDefaultState; //subscribes the ReturnDefaultState method to an OnRemoveHero event
    }
    internal void FillIcon()
    {
        heroImage.sprite = charAttributes.heroSprite;
        stackText.text = charAttributes.stack.ToString();
    }
    public void IconClicked()//responds to a click on a button
    {
        StorageMNG storage = GetComponentInParent<StorageMNG>();
        if (!deployed)//evaluates if the unit is already on the battlefield
        {
            storage.TintIcon(this);//marks a regiment to be placed on the battlefield
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
        deployed = true;
    }
    public void ReturnDefaultState(CharAttributes selectedCharAttributes)//sets light green background to the icon
    {
        if (selectedCharAttributes == charAttributes)//determines if the icon should respond to an event
        {
            backGround.sprite = GetComponentInParent<StorageMNG>().defaultIcon; //sets light green icon
            deployed = false;//defines the hero as available for deployment
        }
    }
}
