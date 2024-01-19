using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerPlayer : MonoBehaviour
{
    public static AudioManagerPlayer instance;
    public SoundGame[] walkSounds, sfxSounds;
    public AudioSource sfxSource1, sfxSource2, sfxSource3;

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
        
            // Elegir una pista de m�sica aleatoria
            string randomWalkSound = GetRandomWalkSound();
            //cogemos audioClip de esa pista
            AudioClip randomWalkClip = FindWalkClipByName(randomWalkSound);
            //hacemos que suene esa pista
            PlaySFX1(randomWalkSound, 0.3f);

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

        // Si no se encuentra, devolver null o manejar el caso seg�n tus necesidades
        return null;
    }

    private string GetRandomWalkSound()
    {
        // Elegir un �ndice aleatorio para la matriz musicSounds
        int randomIndex = UnityEngine.Random.Range(0, walkSounds.Length);

        // Obtener el elemento de la matriz correspondiente al �ndice aleatorio
        string randomMusicSound = walkSounds[randomIndex].name;

        return randomMusicSound;
    }

    #endregion

    public void StopWalkSound()
    {
        StopCoroutine(RandomWalkCoroutine1());
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
