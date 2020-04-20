using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMusica : MonoBehaviour
{

    private GameObject musica;

    public ControlMusica(GameObject music)
    {
        this.musica = music;
    }

    public void pararMusica() {
        musica.GetComponent<AudioSource>().Stop();
    }

    public void playMusica()
    {
        musica.GetComponent<AudioSource>().Play();
    }
}
