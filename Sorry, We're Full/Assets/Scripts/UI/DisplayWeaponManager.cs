// Date   : 11.12.2016 15:17
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;
using UnityEngine.UI;

public class DisplayWeaponManager : MonoBehaviour
{

    [SerializeField]
    private Image imgWeapon;
    [SerializeField]
    private Text txtWeapon;
    [SerializeField]
    private Text txtWeaponKey;

    [SerializeField]
    private Sprite daggersprite;
    [SerializeField]
    private Sprite shortSwordSprite;
    [SerializeField]
    private Sprite scimitarSprite;

    [SerializeField]
    private GameObject meleeWeaponDisplay;

    [SerializeField]
    private Action action;

    void Start()
    {
        SetKeyString();
    }

    void SetKeyString()
    {
        string keyString = "";
        KeyCode key = KeyManager.main.GetKey(action);
        if (key == KeyCode.Return)
        {
            keyString = "Enter";
        }
        else if (key == KeyCode.LeftShift)
        {
            keyString = "L-Shift";
        }
        else if (key == KeyCode.RightShift)
        {
            keyString = "R-Shift";
        }
        else if (key == KeyCode.LeftControl)
        {
            keyString = "L-Ctrl";
        }
        else if (key == KeyCode.RightControl)
        {
            keyString = "R-Ctrl";
        }
        else if (key == KeyCode.LeftAlt)
        {
            keyString = "L-Alt";
        }
        else if (key == KeyCode.RightAlt)
        {
            keyString = "R-Alt";
        }
        else
        {
            keyString = key.ToString();
        }
        txtWeaponKey.text = keyString;
    }

    public void EquipNewWeapon(InventoryItemType weapon)
    {
        SetKeyString();
        if (!meleeWeaponDisplay.activeSelf)
        {
            meleeWeaponDisplay.SetActive(true);
        }
        if (weapon == InventoryItemType.Dagger)
        {
            imgWeapon.sprite = daggersprite;
        }
        else if (weapon == InventoryItemType.ShortSword)
        {
            imgWeapon.sprite = shortSwordSprite;
        }
        else if (weapon == InventoryItemType.Scimitar)
        {
            imgWeapon.sprite = scimitarSprite;
        }
        
        txtWeapon.text = string.Format("<color=#{0}><b>{1}</b></color>",UIManager.main.GetColorAsString(weapon), weapon);
        SoundManager.main.PlaySound(SoundType.EquipItem);

    }
}
