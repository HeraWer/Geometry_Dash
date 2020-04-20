using UnityEngine; 
using System.Collections; 

public class StartButtonController : MonoBehaviour { 
    public GameObject upSprite; 
    public GameObject downSprite; 
    public float downTime = 0.1f; 
    public GameStates stateManager = null; 
    private enum buttonStates { up = 0, down } 
    private buttonStates currentState = buttonStates.up; 
    private float nextStateTime = 0.0f; 
    
    void Start() { 
        upSprite.SetActive(true);
        downSprite.SetActive(true);
    } 
    
    void OnMouseDown() { 
        if(nextStateTime == 0.0f && currentState == StartButtonController.buttonStates.up) { 
            nextStateTime = Time.time + downTime; 
            upSprite.SetActive(false); 
            downSprite.SetActive(true); 
            currentState = StartButtonController.buttonStates.down; 
        } 
    } 
    
    void Update() { 
        if(nextStateTime > 0.0f) { 
            if(nextStateTime < Time.time) { 
                // Retornar el botó a estat “no polsat” 
                nextStateTime = 0.0f; 
                upSprite.SetActive(false); 
                downSprite.SetActive(false); 
                currentState = StartButtonController.buttonStates.up; 
                // Començar el joc 
                stateManager.startGame(); 
            } 
        } 
    } 
}