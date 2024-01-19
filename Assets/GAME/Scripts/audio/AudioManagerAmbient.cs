using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerAmbient : MonoBehaviour
{
    public static AudioManagerAmbient instance;
    public SoundGame[] musicSounds, sfxSounds, transitionSounds;
    public AudioSource musicSource, sfxSource1, sfxSource2, sfxSource3, transitionSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    private void Start()
    {
        //llama a metodo desde el principio que constantemente está poniendo una musica aleatoria de fondo
        RandomMusic();
    }


    #region RandomMusic
    private void RandomMusic()
    {
        StartCoroutine(RandomMusicCoroutine());
    }

    private IEnumerator RandomMusicCoroutine()
    {
        // Se ejecuta todo el tiempo
        while (true)
        {
            // Elegir una pista de música aleatoria
            string randomMusicSound = GetRandomMusicSound();
            //cogemos audioClip de esa pista
            AudioClip randomMusicClip = FindMusicClipByName(randomMusicSound);
            //hacemos que suene esa pista
            PlayMusic(randomMusicSound, 0.1f);

            // Esperar a que la pista actual termine
            yield return new WaitForSeconds(randomMusicClip.length);

            // Volver a elegir otra pista para reproducir en bucle
        }
    }

    private AudioClip FindMusicClipByName(string musicName)
    {
        // Buscar la instancia de AudioClip en musicSounds usando el nombre
        foreach (SoundGame sound in musicSounds)
        {
            if (sound.name == musicName)
            {
                return sound.clip;
            }
        }

        // Si no se encuentra, devolver null o manejar el caso según tus necesidades
        return null;
    }

    private string GetRandomMusicSound()
    {
        // Elegir un índice aleatorio para la matriz musicSounds
        int randomIndex = UnityEngine.Random.Range(0, musicSounds.Length);

        // Obtener el elemento de la matriz correspondiente al índice aleatorio
        string randomMusicSound = musicSounds[randomIndex].name;

        return randomMusicSound;
    }

    #endregion


    public void PlayMusic(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundGame s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            musicSource.volume = volume;
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

   

    //primer sonido VFX
    public void PlaySFX1(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundGame s = Array.Find(sfxSounds, x => x.name == name);

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

    //primer sonido TRANSITION
    public void PlayTransition(string name, float volume)
    {
        //buscamos la musica que queremos poner en el musicSound
        SoundGame s = Array.Find(transitionSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            transitionSource.volume = volume;
            transitionSource.PlayOneShot(s.clip);
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



    public void StopMusic()
    {
        musicSource.Stop();
    }

   

    public void StopTransition()
    {
        transitionSource.Stop();

    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource1.mute = !sfxSource1.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
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

    public void TransitionVolume(float volume)
    {
        transitionSource.volume = volume;
    }





    #region FuncionalidadExclusiva_Secuencia4
    public void PulsarBotonSound()
    {
        //sonido pala golpe al acabar animacion
        PlaySFX1("clickButton", 1f);
    }
    #endregion
}


