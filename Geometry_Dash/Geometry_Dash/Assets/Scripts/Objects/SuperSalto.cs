using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSalto : MonoBehaviour
{
    public GameObject player = null;
    public float potencia_salto = 15;

void OnTriggerEnter2D(Collider2D collidedObject)
{
    if (collidedObject.tag == "Player") {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.up * potencia_salto;
    }
}

}