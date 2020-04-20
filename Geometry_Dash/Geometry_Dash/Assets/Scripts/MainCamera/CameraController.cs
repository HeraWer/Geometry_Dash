using UnityEngine;
using System.Collections;
public class CameraController : MonoBehaviour
{
    public PlayerStateController.playerStates currentPlayerState = PlayerStateController.playerStates.idle;
    public GameObject playerObject = null;
    public float cameraTrackingSpeed = 0.2f;
    private Vector3 lastTargetPosition = Vector3.zero;
    private Vector3 currTargetPosition = Vector3.zero;
    private float currLerpDistance = 0.0f;

    void Start()
    {
        //Establir la posicio inicial de la camara com la del Player, per evitar un salt inicial. 
        Vector3 playerPos = playerObject.transform.position;
        Vector3 cameraPos = transform.position;
        Vector3 startTargPos = playerPos;
        //Igualar coordenada z del Player  a la de la camara, i evitar moviments fora del pla. 
        startTargPos.z = cameraPos.z;
        lastTargetPosition = startTargPos;
        currTargetPosition = startTargPos;
        currLerpDistance = 1.0f;
    }
    //afegir listener de l'event canvi d'estat del Player 
    void OnEnable()
    {
        PlayerStateController.onStateChange += onPlayerStateChange;
    }
    //eliminar listener de l'event canvi d'estat del Player 
    void OnDisable()
    {
        PlayerStateController.onStateChange -= onPlayerStateChange;
    }
    //Tractament de l'event canvi d'estat del Player: l'estat actual passa a ser el nou estat que indica l'event 
    //El tractament concret es fara a onStateCycle() 
    void onPlayerStateChange(PlayerStateController.playerStates newState)
    {
        currentPlayerState = newState;
    }
    //Tractament realitzat en cada frame 
    void LateUpdate()
    {
        // Tractaments segons el nou estat del Player 
        onStateCycle(); 
        // Moure camera a la nova posicio del Player 
        currLerpDistance += cameraTrackingSpeed;
        transform.position = Vector3.Lerp(lastTargetPosition, currTargetPosition, currLerpDistance);
    }

    // Tractament PEL QUE FA A LA CAMERA! que correspon a cada estat del Player 
    void onStateCycle()
    {
        switch (currentPlayerState)
        {
            case PlayerStateController.playerStates.idle:
                trackPlayer();
                break;
            case PlayerStateController.playerStates.left:
                trackPlayer();
                break;
            case PlayerStateController.playerStates.right:
                trackPlayer();
                break;
        }
    }

    //Fer que la camera segueixi al Player 
    void trackPlayer()
    {
        // Obtenir i guardar posicio actual de la camera i del Player 
        Vector3 currCamPos = transform.position;
        Vector3 currPlayerPos = playerObject.transform.position;
        //Si les posicions son iguals, no cal fer res
        if (currCamPos.x == currPlayerPos.x && currCamPos.y == currPlayerPos.y)
        {
            // Positions are the same - tell the camera not to move, then abort. 
            currLerpDistance = 1f;
            lastTargetPosition = currCamPos;
            currTargetPosition = currCamPos; return;
        }
        // Actualitzar variables (posicio anterior i posicio actual ...)  per fer que
        // la camara vagi a la posicio del Player 
        currLerpDistance = 0f;
        lastTargetPosition = currCamPos;
        currTargetPosition = currPlayerPos;
        //No volem que canvii la component z, estem en 2D! 
        currTargetPosition.z = currCamPos.z;
    }
    //Fer que la camera NO segueixi al Player 
    void stopTrackingPlayer()
    {
        // Fer que la posicio desti de la camara sigui la posicio actual. 
        Vector3 currCamPos = transform.position;
        currTargetPosition = currCamPos;
        lastTargetPosition = currCamPos;
        currLerpDistance = 1.0f;
    }
}
