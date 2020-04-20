using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSource : MonoBehaviour
{
    public static GameObject MUSIC_OBJECT = null;

    public static void buscarObjetoMusica()
    {
        //BUSCO LOS OBJETOS DE LA ESCENA
        Scene escena = SceneManager.GetActiveScene();
        GameObject[] objetos = escena.GetRootGameObjects();
        for (int i = 0; i < objetos.Length; i++) //LOS RECORRO
        {
            if (objetos[i].tag.Equals("MainCamera")) //BUSCO LA MAIN CAMERA
            {
                MUSIC_OBJECT = objetos[i]; //GUARDO EL OBJETO QUE CONTIENE LA MUSICA
                break;
            }
        }
    }

}
