// Date   : 10.12.2016 09:30
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;
using System.Collections.Generic;

public enum SoundType
{
    None,
    OutOfAmmo,
    ProjectileLaunch,
    ProjectileHitObject,
    ProjectileHitAnimal,
    ProjectileHitEnemy,
    PickUpProjectile,
    AnimalDie,
    SwingWeapon,
    SellItem,
    BuyItem,
    EquipItem,
    PlayerWasHit,
    HitMiss,
    PlayerDie
}

public class SoundManager : MonoBehaviour {

    public static SoundManager main;

    [SerializeField]
    private List<GameSound> sounds = new List<GameSound>();
    private RandomWrapper rng;

    private bool isOn = true;

    void Awake()
    {
        main = this;
        rng = new RandomWrapper();
    }
    
    public void PlaySound(SoundType soundType)
    {
        if (isOn) { 
            foreach(GameSound gameSound in sounds)
            {
                if(gameSound.soundType == soundType)
                {
                    rng.Choose(gameSound.sounds).Play();
                }
            }
        }
    }

    public bool Toggle()
    {
        isOn = !isOn;
        return isOn;
    }
}

[System.Serializable]
public class GameSound : System.Object
{
    
    public SoundType soundType;
    public List<AudioSource> sounds = new List<AudioSource>();

}