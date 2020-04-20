using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpikesCollider : MonoBehaviour
{
    PlayerStateListener player = new PlayerStateListener();
    void OnTriggerEnter2D(Collider2D collidedObject)
    {
        if (collidedObject.tag == "Player")
        {
            collidedObject.SendMessage("hitDeathTrigger");
            player.resetStatus();

            if (MusicSource.MUSIC_OBJECT == null)
            {
                MusicSource.buscarObjetoMusica(); //BUSCO EL OBJETO QUE CONTIENE LA MUSICA DE FONDO
            }

            new ControlMusica(MusicSource.MUSIC_OBJECT).pararMusica(); //PARO LA MUSICA
            GetComponent<AudioSource>().Play();
        }
    }

}
