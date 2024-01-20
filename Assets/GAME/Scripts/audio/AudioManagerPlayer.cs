using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerPlayer : MonoBehaviour
{
    public static AudioManagerPlayer instance;
    public SoundGame[] walkSounds, houseHit, attackSounds, attackVoice, sfxSounds;
    public AudioSource sfxSource1, sfxSource2, sfxSource3, sfxSource4, sfxSource5, sfxSource6;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
           
        }
        else
        {
            Destroy(gameObject);
        }
    }


    #region RandomWalkSound
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
        foreach (SoundGame sound in walkSounds)
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
        int randomIndex = UnityEngine.Random.Range(0, walkSounds.Length);

        // Obtener el elemento de la matriz correspondiente al índice aleatorio
        string randomMusicSound = walkSounds[randomIndex].name;

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
        PlayAttackSFX(randomAttackSound, 0.003f);

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
        PlayAttackVoiceSFX(randomAttackVoiceSound, 0.15f);

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


    #region RandomHouseHit
    public void RandomHouseHit()
    {
        StartCoroutine(RandomHouseHitCoroutine1());
    }


    private IEnumerator RandomHouseHitCoroutine1()
    {
        // Se ejecuta todo el tiempo

        // Elegir una pista de música aleatoria
        string randomHouseHitSound = GetRandomHouseHitSound();
        //cogemos audioClip de esa pista
        AudioClip randomHouseHitClip = FindHouseHitClipByName(randomHouseHitSound);
        //hacemos que suene esa pista
        PlaySFX4(randomHouseHitSound, 0.15f);

        // Esperar a que la pista actual termine
        yield return new WaitForSeconds(randomHouseHitClip.length);


        // Volver a elegir otra pista para reproducir en bucle

    }

    private AudioClip FindHouseHitClipByName(string walkName)
    {
        // Buscar la instancia de AudioClip en musicSounds usando el nombre
        foreach (SoundGame sound in houseHit)
        {
            if (sound.name == walkName)
            {
                return sound.clip;
            }
        }

        // Si no se encuentra, devolver null o manejar el caso según tus necesidades
        return null;
    }

    private string GetRandomHouseHitSound()
    {
        // Elegir un índice aleatorio para la matriz musicSounds
        int randomIndex = UnityEngine.Random.Range(0, houseHit.Length);

        // Obtener el elemento de la matriz correspondiente al índice aleatorio
        string randomMusicSound = houseHit[randomIndex].name;

        return randomMusicSound;
    }

    #endregion
    

    public void NextRound()
    {
        PlaySFX4("nextRound", 0.4f);
    }

    public void TakePassive()
    {
        PlaySFX5("takepasiva", 0.4f);
    }

    public void PassiveEscudo()
    {
        PlaySFX6("escudo", 0.4f);
    }

    public void Win()
    {
        PlaySFX4("win", 0.4f);
    }

    public void Lose()
    {
        PlaySFX4("lose", 0.4f);
    }





    //primer sonido VFX
    public void PlaySFX1(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundGame s = Array.Find(walkSounds, x => x.name == name);

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

    //tercer sonido VFX
    public void PlaySFX4(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundGame s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource4.volume = volume;
            sfxSource4.PlayOneShot(s.clip);
        }
    }

    //tercer sonido VFX
    public void PlaySFX5(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundGame s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource5.volume = volume;
            sfxSource5.PlayOneShot(s.clip);
        }
    }

    //tercer sonido VFX
    public void PlaySFX6(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundGame s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource6.volume = volume;
            sfxSource6.PlayOneShot(s.clip);
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
