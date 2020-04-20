using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PlayerStateController : MonoBehaviour
{ //Definicio dels diferents estats del player   
    public enum playerStates
    {
        idle = 0,
        left,
        right,
        jump,
        landing,
        falling,
        kill,
        resurrect,
        teleport,
        teleport2

    }
    //Definicio del delegate playerStateHandler   
    public delegate void playerStateHandler(PlayerStateController.playerStates newState);
    //Definicio d'event onStateChange i assignacio de onStateChange com a EventHandler   
    public static event playerStateHandler onStateChange;
    // Aquest metode es crida despres de Update() a cada frame.   

    private static int gradosRotacion = 360;
    private static int contador = 0;
    private int siguienteRotacion = 360;
    private bool rotacion = false;
    private bool saltando = false;
    public static bool muerto = false;
    private static bool plataformaTocada = false;
    public static int points = 0;

    void LateUpdate()
    {
        if(!GameStates.gameActive) return;

        if (!muerto)
        {
            // if (gradosRotacion < 90) {
            // Debug.Log("Tiempo: " + Time.time + ", Grados: " + gradosRotacion);
            if (Input.GetKeyUp(KeyCode.Space))
            {
                saltando = false;
            }
            if (Input.GetKeyDown(KeyCode.Space) && !rotacion)
            {
                saltando = true;
            }
            if (saltando && plataformaTocada || rotacion)
            {
                // Debug.Log("Saltando: " + saltando + ", Plataforma: " + plataformaTocada + " || Rotacion: " + rotacion);
                if (plataformaTocada)
                {
                    //ERROR: SOLUCIONAR GIRAR EN EL SUELO
                    plataformaTocada = false;
                    rotacion = true;
                    siguienteRotacion -= 180;
                }
                // Debug.Log("Grados: " + gradosRotacion + ", Siguiente: " + siguienteRotacion);
                if (gradosRotacion != siguienteRotacion)
                {
                    transform.eulerAngles = new Vector3(0, 0, gradosRotacion);
                    // if (contador == 1) {
                    gradosRotacion -= 3;
                    // contador = 0;
                    // } else {
                    // gradosRotacion -= 2;
                    //     contador++;
                    // }
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, gradosRotacion);
                    rotacion = false;
                    if (siguienteRotacion == 0)
                    {
                        siguienteRotacion = 360;
                        gradosRotacion = 360;
                    }
                }
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, gradosRotacion);
            }

            //MANTENGO LA VELOCIDAD DEL PLAYER 
            onStateChange(PlayerStateController.playerStates.right);
        }
    }

    public static void RotacionPlayer()
    {
        plataformaTocada = true;
    }

    public static void ReinicioStatus()
    {

    }
}