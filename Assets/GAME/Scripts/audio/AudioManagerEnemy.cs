using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerEnemy: MonoBehaviour
{
    public static AudioManagerEnemy instance;
    public SoundGame[] randomSounds, sfxSounds, attackSounds, attackVoice, death;
    public AudioSource sfxSource1, sfxSource2, sfxSource3;

    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;

        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }


    #region RandomSound
    public void RandomWalkSound()
    {
        StartCoroutine(RandomWalkCoroutine1());
    }


    private IEnumerator RandomWalkCoroutine1()
    {
        // Se ejecuta todo el tiempo

        // Elegir una pista de música aleatoria
        string randomWalkSound = GetRandomWalkSound();
        //cogemos audioClip de esa pista
        AudioClip randomWalkClip = FindWalkClipByName(randomWalkSound);
        //hacemos que suene esa pista
        PlaySFX1(randomWalkSound, 0.15f);

        // Esperar a que la pista actual termine
        yield return new WaitForSeconds(randomWalkClip.length);


        // Volver a elegir otra pista para reproducir en bucle

    }

    private AudioClip FindWalkClipByName(string walkName)
    {
        // Buscar la instancia de AudioClip en musicSounds usando el nombre
        foreach (SoundGame sound in randomSounds)
        {
            if (sound.name == walkName)
            {
                return sound.clip;
            }
        }

        // Si no se encuentra, devolver null o manejar el caso según tus necesidades
        return null;
    }

    private string GetRandomWalkSound()
    {
        // Elegir un índice aleatorio para la matriz musicSounds
        int randomIndex = UnityEngine.Random.Range(0, randomSounds.Length);

        // Obtener el elemento de la matriz correspondiente al índice aleatorio
        string randomMusicSound = randomSounds[randomIndex].name;

        return randomMusicSound;
    }

    public void StopWalkSound()
    {
        StopCoroutine(RandomWalkCoroutine1());
    }

    #endregion



    #region RandomAttackSound
    public void RandomAttackSound()
    {
        StartCoroutine(RandomAttackCoroutine1());
    }


    private IEnumerator RandomAttackCoroutine1()
    {
        // Se ejecuta todo el tiempo

        // Elegir una pista de música aleatoria
        string randomAttackSound = GetRandomAttackSound();
        //cogemos audioClip de esa pista
        AudioClip randomAttackClip = FindAttackClipByName(randomAttackSound);
        //hacemos que suene esa pista
        PlayAttackSFX(randomAttackSound, 0.1f);

        // Esperar a que la pista actual termine
        yield return new WaitForSeconds(randomAttackClip.length);


        // Volver a elegir otra pista para reproducir en bucle

    }

    private AudioClip FindAttackClipByName(string AttackName)
    {
        // Buscar la instancia de AudioClip en musicSounds usando el nombre
        foreach (SoundGame sound in attackSounds)
        {
            if (sound.name == AttackName)
            {
                return sound.clip;
            }
        }

        // Si no se encuentra, devolver null o manejar el caso según tus necesidades
        return null;
    }

    private string GetRandomAttackSound()
    {
        // Elegir un índice aleatorio para la matriz musicSounds
        int randomIndex = UnityEngine.Random.Range(0, attackSounds.Length);

        // Obtener el elemento de la matriz correspondiente al índice aleatorio
        string randomAttackSound = attackSounds[randomIndex].name;

        return randomAttackSound;
    }

    #endregion

    #region RandomAttackVoice
    public void RandomAttackVoice()
    {
        StartCoroutine(RandomAttackVoiceCoroutine1());
    }


    private IEnumerator RandomAttackVoiceCoroutine1()
    {
        // Se ejecuta todo el tiempo

        // Elegir una pista de música aleatoria
        string randomAttackVoiceSound = GetRandomAttackVoiceSound();
        //cogemos audioClip de esa pista
        AudioClip randomAttackVoiceClip = FindAttackVoiceClipByName(randomAttackVoiceSound);
        //hacemos que suene esa pista
        PlayAttackVoiceSFX(randomAttackVoiceSound, 0.03f);

        // Esperar a que la pista actual termine
        yield return new WaitForSeconds(randomAttackVoiceClip.length);


        // Volver a elegir otra pista para reproducir en bucle

    }

    private AudioClip FindAttackVoiceClipByName(string AttackName)
    {
        // Buscar la instancia de AudioClip en musicSounds usando el nombre
        foreach (SoundGame sound in attackVoice)
        {
            if (sound.name == AttackName)
            {
                return sound.clip;
            }
        }

        // Si no se encuentra, devolver null o manejar el caso según tus necesidades
        return null;
    }

    private string GetRandomAttackVoiceSound()
    {
        // Elegir un índice aleatorio para la matriz musicSounds
        int randomIndex = UnityEngine.Random.Range(0, attackVoice.Length);

        // Obtener el elemento de la matriz correspondiente al índice aleatorio
        string randomAttackSound = attackVoice[randomIndex].name;

        return randomAttackSound;
    }

    #endregion

    #region RandomDeath
    public void RandomDeath()
    {
        RandomDeathCoroutine1();
    }


    private void RandomDeathCoroutine1()
    {
        // Se ejecuta todo el tiempo

        // Elegir una pista de música aleatoria
        string randomDeathSound = GetRandomDeathSound();
        //cogemos audioClip de esa pista
        AudioClip randomDeathClip = FindDeathClipByName(randomDeathSound);
        //hacemos que suene esa pista
        PlayDeathSFX(randomDeathSound, 0.15f);

        //// Esperar a que la pista actual termine
        //yield return new WaitForSeconds(randomDeathClip.length);


        // Volver a elegir otra pista para reproducir en bucle

    }

    private AudioClip FindDeathClipByName(string DeathName)
    {
        // Buscar la instancia de AudioClip en musicSounds usando el nombre
        foreach (SoundGame sound in attackVoice)
        {
            if (sound.name == DeathName)
            {
                return sound.clip;
            }
        }

        // Si no se encuentra, devolver null o manejar el caso según tus necesidades
        return null;
    }

    private string GetRandomDeathSound()
    {
        // Elegir un índice aleatorio para la matriz musicSounds
        int randomIndex = UnityEngine.Random.Range(0, death.Length);

        // Obtener el elemento de la matriz correspondiente al índice aleatorio
        string randomAttackSound = death[randomIndex].name;

        return randomAttackSound;
    }

    #endregion





    //primer sonido VFX
    public void PlaySFX1(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundGame s = Array.Find(randomSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource1.volume = volume;
            sfxSource1.PlayOneShot(s.clip);
        }
    }

    public void PlayAttackSFX(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundGame s = Array.Find(attackSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource1.volume = volume;
            sfxSource1.PlayOneShot(s.clip);
        }
    }

    public void PlayAttackVoiceSFX(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundGame s = Array.Find(attackVoice, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource2.volume = volume;
            sfxSource2.PlayOneShot(s.clip);
        }
    }

    public void PlayDeathSFX(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundGame s = Array.Find(death, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource3.volume = volume;
            sfxSource3.PlayOneShot(s.clip);
        }
    }







    //SEGUNDO sonido VFX
    public void PlaySFX2(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundGame s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource2.volume = volume;
            sfxSource2.PlayOneShot(s.clip);
        }
    }

    //tercer sonido VFX
    public void PlaySFX3(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundGame s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource3.volume = volume;
            sfxSource3.PlayOneShot(s.clip);
        }
    }

    public void StopSFX()
    {
        sfxSource1.Stop();
        sfxSource2.Stop();
        sfxSource3.Stop();
    }

    public void StopSFX1()
    {
        sfxSource1.Stop();
    }

    public void StopSFX2()
    {
        sfxSource2.Stop();
    }

    public void StopSFX3()
    {
        sfxSource3.Stop();
    }


    public void ToggleSFX()
    {
        sfxSource1.mute = !sfxSource1.mute;
    }


    public void SFXVolume1(float volume)
    {
        sfxSource1.volume = volume;
    }

    public void SFXVolume2(float volume)
    {
        sfxSource2.volume = volume;
    }

    public void SFXVolume3(float volume)
    {
        sfxSource3.volume = volume;
    }

}
