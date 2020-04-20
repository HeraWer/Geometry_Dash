using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AumentoVelocidadPlayer : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collidedObject)
    {
        if (collidedObject.tag == "Player")
        {
            PlayerStateListener.activarAumentarVelocidad();
        }
    }

}
