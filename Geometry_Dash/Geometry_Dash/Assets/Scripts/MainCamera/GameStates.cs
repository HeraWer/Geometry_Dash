using UnityEngine; 
using System.Collections; 

public class GameStates : MonoBehaviour { 
    public GameObject StartContainer;
    public GameObject hudContainer; 
    public GameObject titleContainer; 
    public static bool gameActive = false; 
    public enum displayStates { 
        titleScreen = 0, 
        hudScreen 
    }

    void Start() { 
        changeDisplayState(displayStates.titleScreen); 
    } 
    
    public void changeDisplayState(displayStates newState) { 
        hudContainer.SetActive(false); 
        titleContainer.SetActive(false); 
        switch(newState) { 
            case displayStates.titleScreen: 
                gameActive = false; 
                titleContainer.SetActive(true); 
                break; 
            case displayStates.hudScreen: 
                Debug.Log("El juego ha empezado");
                gameActive = true; 
                hudContainer.SetActive(true); 
                break; 
        } 
    } 
    
    public void startGame() {
        Destroy(StartContainer);
        changeDisplayState(displayStates.hudScreen); 
    }
}
