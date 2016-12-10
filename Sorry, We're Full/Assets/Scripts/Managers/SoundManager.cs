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
    SwingWeapon
}

public class SoundManager : MonoBehaviour {

    public static SoundManager main;

    [SerializeField]
    private List<GameSound> sounds = new List<GameSound>();
    private RandomWrapper rng;

    void Awake()
    {
        main = this;
        rng = new RandomWrapper();
    }
    
    public void PlaySound(SoundType soundType)
    {
        foreach(GameSound gameSound in sounds)
        {
            if(gameSound.soundType == soundType)
            {
                rng.Choose(gameSound.sounds).Play();
            }
        }
    }
}

[System.Serializable]
public class GameSound : System.Object
{
    
    public SoundType soundType;
    public List<AudioSource> sounds = new List<AudioSource>();

}