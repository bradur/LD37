// Date   : 10.12.2016 12:26
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public enum MessageType
{
    None,
    NoAmmo
}

public class UIManager : MonoBehaviour {

    public static UIManager main;

    [SerializeField]
    private List<AnimalColor> animalColors = new List<AnimalColor>();

    [SerializeField]
    private List<WeaponColor> weaponColors = new List<WeaponColor>();

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
