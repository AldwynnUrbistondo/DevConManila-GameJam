using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SoundType
{
    PlayerSpawn,
    PlayerProjectile,
    LightDragonProjectile,
    IceDragonProjectile,
    WindDragonProjectile,
    PlayerHitEnemy,
    PlayerWalk,
    EnemyHitPlayer,
    PlayerDeath,
    EnemyDeath,
    PetSpawn,
    OpenShop,
    ButtonClick,
    ButtonBuy,
    Clock
}
public class AudioManager : MonoBehaviour
{
    public AudioSource[] playerProjectileSound;
    public AudioSource[] playerHitEnemySound;
    public AudioSource[] playerDeathSound;
    public AudioSource[] playerWalkSound;
    public AudioSource[] lightDragonProjectileSound;
    public AudioSource[] iceDragonProjectileSound;
    public AudioSource[] windDragonProjectileSound;
    public AudioSource[] enemyHitPlayerSound;
    public AudioSource[] enemyDeathSound;
    public AudioSource[] clockSound;

    public AudioSource playerSpawnSound;
    public AudioSource petSpawnSound;
    public AudioSource openShop;
    public AudioSource buttonClickSound;
    public AudioSource buttonBuySound;

    public void PlaySound(SoundType soundType)
    {
        switch (soundType)
        {
            // = = = = = = = = Multiple Channel = = = = = = = = 
            case SoundType.PlayerProjectile:
                AudioPlay(playerProjectileSound);
                break;

            case SoundType.PlayerWalk:
                AudioPlay(playerWalkSound);
                break;

            case SoundType.PlayerHitEnemy:
                AudioPlay(playerHitEnemySound);
                break;

            case SoundType.PlayerDeath:
                AudioPlay(playerDeathSound);
                break;

            case SoundType.LightDragonProjectile:
                AudioPlay(lightDragonProjectileSound);
                break;

            case SoundType.IceDragonProjectile:
                AudioPlay(iceDragonProjectileSound);
                break;

            case SoundType.WindDragonProjectile:
                AudioPlay(windDragonProjectileSound);
                break;

            case SoundType.EnemyHitPlayer:
                AudioPlay(enemyHitPlayerSound);
                break;

            case SoundType.EnemyDeath:
                AudioPlay(enemyDeathSound);
                break;

            case SoundType.Clock:
                AudioPlay(clockSound);
                break;

            // = = = = = = = = Single Channel = = = = = = = = 

            case SoundType.OpenShop:
                openShop.Play();
                break;

            case SoundType.ButtonClick:
                buttonClickSound.Play();
                break;

            case SoundType.ButtonBuy:
                buttonBuySound.Play();
                break;

            case SoundType.PlayerSpawn:
                playerSpawnSound.Play();
                break;

            case SoundType.PetSpawn:
                petSpawnSound.Play();
                break;

        }
    }

    void AudioPlay(AudioSource[] audioSources)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (!audioSources[i].isPlaying)
            {
                audioSources[i].Play();
                return;
            }
        }

        audioSources[0].Play();

    }
}
