// Date   : 10.12.2016 12:26
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public enum MessageType
{
    None,
    NoAmmo,
    ItemCountUpdate
}

public class UIManager : MonoBehaviour {

    public static UIManager main;

    [SerializeField]
    private List<AnimalColor> animalColors = new List<AnimalColor>();

    [SerializeField]
    private List<WeaponColor> weaponColors = new List<WeaponColor>();

    [SerializeField]
    private List<InventoryItemColor> itemColors = new List<InventoryItemColor>();

    [SerializeField]
    private DisplayMessageManager displayMessageManager;

    void Awake()
    {
        main = this;
    }

    public void ShowMessage(string message)
    {
        displayMessageManager.ShowMessage(message);
    }

    public void ShowMessage(AnimalType animal, Weapon weapon, int projectileCount)
    {
        ShowMessage(string.Format(
            "You killed <color=#{0}><b>{1}</b></color> with your <color=#{2}><b>{3}</b></color>!",
            GetColorAsString(animal),
            animal,
            GetColorAsString(weapon),
            weapon
        ));
    }

    string GetColorAsString(AnimalType animalType)
    {
        Color32 animalColor = GetAnimalColor(animalType);
        return string.Format("{0:X2}{1:X2}{2:X2}", animalColor.r, animalColor.g, animalColor.b);
    }

    string GetColorAsString(Weapon weapon) {
        Color32 weaponColor = GetWeaponColor(weapon);
        return string.Format("{0:X2}{1:X2}{2:X2}", weaponColor.r, weaponColor.g, weaponColor.b);
    }


    string GetColorAsString(InventoryItemType weapon)
    {
        Color32 itemColor = GetInventoryItemColor(weapon);
        return string.Format("{0:X2}{1:X2}{2:X2}", itemColor.r, itemColor.g, itemColor.b);
    }

    public void ShowMessage(MessageType messageType, InventoryItemType inventoryItemType, int value)
    {
        if (messageType == MessageType.ItemCountUpdate)
        {
            ShowMessage(string.Format(
                "You now have {0} <color=#{1}><b>{2}</b></color>!",
                value,
                GetColorAsString(inventoryItemType),
                inventoryItemType
            ));
        }
    }

    public void ShowMessage(Weapon weapon, MessageType messageType)
    {
        if (messageType == MessageType.NoAmmo)
        {
            ShowMessage(string.Format(
                "You have ran out of ammo for your <color=#{0}><b>{1}</b></color>",
                GetColorAsString(weapon),
                weapon
            ));
        }
    }

    public Color GetInventoryItemColor(InventoryItemType inventoryItem)
    {
        foreach (InventoryItemColor itemColor in itemColors)
        {
            if (itemColor.Item == inventoryItem)
            {
                return itemColor.Color;
            }
        }
        return Color.white;
    }


    public Color GetAnimalColor(AnimalType animal)
    {
        foreach (AnimalColor animalColor in animalColors)
        {
            if(animalColor.Animal == animal)
            {
                return animalColor.Color;
            }
        }
        return Color.white;
    }

    public Color GetWeaponColor(Weapon weapon)
    {
        foreach (WeaponColor weaponColor in weaponColors)
        {
            if (weaponColor.Weapon == weapon)
            {
                return weaponColor.Color;
            }
        }
        return Color.white;
    }
}

[System.Serializable]
public class AnimalColor: System.Object
{
    public AnimalType Animal;
    public Color Color;
}

[System.Serializable]
public class WeaponColor : System.Object
{
    public Weapon Weapon;
    public Color Color;
}

[System.Serializable]
public class InventoryItemColor : System.Object
{
    public InventoryItemType Item;
    public Color Color;
}