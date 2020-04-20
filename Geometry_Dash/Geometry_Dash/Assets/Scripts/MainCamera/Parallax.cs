using UnityEngine; 
using System.Collections; 

public class Parallax : MonoBehaviour {    
    public GameObject[] background;
    public float backgroundLayerSpeedModifier; 
    public Camera myCamera;   
    private Vector3 lastCamPos;    
    
    void Start()    
    { 
        lastCamPos = myCamera.transform.position;    
    }    
    
    void Update()    
    { 
        Vector3 currCamPos = myCamera.transform.position; 
        float xPosDiff = lastCamPos.x - currCamPos.x; 
        adjustParallaxPositionsForArray(background, backgroundLayerSpeedModifier, xPosDiff);
        lastCamPos = myCamera.transform.position;    
    }    
    
    void adjustParallaxPositionsForArray(GameObject[] layerArray, float layerSpeedModifier, float xPosDiff)    
    { 
        for(int i = 0; i < layerArray.Length; i++) { 
            Vector3 objPos = layerArray[i].transform.position; 
            objPos.x += xPosDiff * layerSpeedModifier; 
            layerArray[i].transform.position = objPos; 
        }    
    } 
} 