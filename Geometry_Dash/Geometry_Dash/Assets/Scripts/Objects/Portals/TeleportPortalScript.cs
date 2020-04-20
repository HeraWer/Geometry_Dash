using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPortalScript : MonoBehaviour
{
    PlayerStateListener player = new PlayerStateListener();

    void OnTriggerEnter2D(Collider2D collidedObject)
    {
        Debug.Log(gameObject);
        if(gameObject.tag == "portal1"){
            collidedObject.SendMessage("teleportPortalPlayer");
        }else if(gameObject.tag == "portal2") {
            collidedObject.SendMessage("teleportPortalPlayer2");
        }
        
    }
}
