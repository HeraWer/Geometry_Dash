using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionsCoins : MonoBehaviour
{

    public TextMesh marcador;
    void OnTriggerEnter2D(Collider2D collidedObject)
    {
        if(collidedObject.tag == "Coin")
        {
            Destroy(collidedObject.gameObject);
            PlayerStateController.points = PlayerStateController.points + 1;
            marcador.text = "" + PlayerStateController.points;
            Debug.Log(PlayerStateController.points);
        }
    }
}
