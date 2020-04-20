using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [Range(1, 20)]
    public float jumpVelocity;
    private static bool estadoJump = false;
    public static bool plataformaTocada = false;
    //private static int rotacionPlayer = 0;

    //Definicio del delegate playerStateHandler   
    public delegate void playerStateHandler(PlayerStateController.playerStates newState);
    //Definicio d'event onStateChange i assignacio de onStateChange com a EventHandler   
    public static event playerStateHandler onStateChange;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            estadoJump = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            estadoJump = true;
        }
        if (estadoJump && plataformaTocada)
        {            
            plataformaTocada = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
            if (onStateChange != null)
                onStateChange(PlayerStateController.playerStates.jump);
        }
    }

    public static void UpdateEstadoJump()
    {
        plataformaTocada = true;
    }
    public static void finSalto()
    {
        plataformaTocada = false;
    }
}
