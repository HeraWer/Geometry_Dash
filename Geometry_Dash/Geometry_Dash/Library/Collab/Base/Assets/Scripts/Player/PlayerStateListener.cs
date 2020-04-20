using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Animator))]
public class PlayerStateListener : MonoBehaviour
{
    public static float playerWalkSpeed = 8f;
    public GameObject playerRespawnPoint = null;
    private Animator playerAnimator = null;
    private PlayerStateController.playerStates previousState = PlayerStateController.playerStates.idle;
    private PlayerStateController.playerStates currentState = PlayerStateController.playerStates.idle;

    public static bool aumentarVelocidadBoolean = false;
    //Aquest mètode de MonoBehaviour s'executa cada vegada que s'activa l'objecte associat a l'script. 
    //L'objecte s'apunta a escoltar l'event onStateChange: afegeix la funcio onStateChange a la llista de 
    //handlers (manegadors) de l'event PlayerStateController.onStateChange. Amb aixo, cada vegada que 
    //es generi un event PlayerStateController.onStateChange, el sistema passara el control a la funcio 
    //onStateChange (i, sequencialment, a totes les funcions que s'hagin afegit a la llista de handlers //d'aquest event) 
    void OnEnable()
    {
        PlayerStateController.onStateChange += onStateChange;
    }
    //Aquest mètode de MonoBehaviour s'executa cada vegada que es desactiva l'objecte associat a l'script. 
    //Es deixa d'escoltar l'event onStateChange 
    void OnDisable()
    {
        PlayerStateController.onStateChange -= onStateChange;
    }
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }
    void LateUpdate()
    {
        if (aumentarVelocidadBoolean) {
            aumentarVelocidad();
        }
        onStateCycle();
    }

    // Processar l'estat en cada cicle 
    void onStateCycle()
    {
        // Guardar l'actual localScale de l'bjecte (és al component Transform de l'objecte) 
        Vector3 localScale = transform.localScale; transform.localEulerAngles = Vector3.zero;
        switch (currentState)
        {
            case PlayerStateController.playerStates.idle:
                break;
            case PlayerStateController.playerStates.left:
                //moure cap a l'esquerra modificant la posició
                transform.Translate(new Vector3((playerWalkSpeed * -1.0f) * Time.deltaTime, 0.0f, 0.0f));
                if (localScale.x > 0.0f)
                {
                    localScale.x *= -1.0f;
                    transform.localScale = localScale;
                }
                break;
            case PlayerStateController.playerStates.right:
                //moure cap a la dreta  modificant la posició
                transform.Translate(new Vector3(playerWalkSpeed * Time.deltaTime, 0.0f, 0.0f));
                if (localScale.x < 0.0f)
                {
                    localScale.x *= -1.0f;
                    transform.localScale = localScale;
                }
                break;
            case PlayerStateController.playerStates.jump:
                onStateChange(PlayerStateController.playerStates.landing);
                break;
            case PlayerStateController.playerStates.landing:
                break;
            case PlayerStateController.playerStates.falling:
                break;
            case PlayerStateController.playerStates.kill:
                onStateChange(PlayerStateController.playerStates.resurrect);
                break;
            case PlayerStateController.playerStates.resurrect:
                onStateChange(PlayerStateController.playerStates.idle);
                break;
        }
    }
    // onStateChange es crida sempre que canvia l'estat del player 
    public void onStateChange(PlayerStateController.playerStates newState)
    {
        // Si l'estat actual i el nou són el mateix, no cal fer res 
        if (newState == currentState) return;
        // Comprovar que no hi hagi condicions per abortar l'estat 
        if (checkIfAbortOnStateCondition(newState)) return;
        // Comprovar que el pas de l'estat actual al nou estat està permès. Si no ho està, no es continua. 
        if (!checkForValidStatePair(newState)) return;
        // Realitzar les accions necessàries en cada cas per canviar l'estat. 
        // De moment només es gestionen els estats idle, right i left 
        switch (newState)
        {
            case PlayerStateController.playerStates.idle:
                break;
            case PlayerStateController.playerStates.left:
                break;
            case PlayerStateController.playerStates.right:
                break;
            case PlayerStateController.playerStates.jump:
                break;
            case PlayerStateController.playerStates.landing:
                GetComponent<Rigidbody2D>().velocity = Vector2.zero; //velocitat lineal: zero
                break;
            case PlayerStateController.playerStates.falling:
                break;
            case PlayerStateController.playerStates.kill:
                break;
            case PlayerStateController.playerStates.resurrect:
                transform.position = playerRespawnPoint.transform.position; //posicio: la de PlayerRespawnPoint
                transform.rotation = Quaternion.identity; //rotacio: cap 
                GetComponent<Rigidbody2D>().velocity = Vector2.zero; //velocitat lineal: zero    
                break;
        }
        // Guardar estat actual com a estat previ 
        previousState = currentState;
        // Assignar el nou estat com a estat actual del player 
        currentState = newState;
    }
    // Comprovar si es pot passar al nou estat des de l'actual. 
    // Es tracten diversos estats que encara no estan implementats, perquè el 
    // codi sigui més ilustratiu 
    bool checkForValidStatePair(PlayerStateController.playerStates newState)
    {
        bool returnVal = false;
        // Comparar estat actual amb el candidat a nou estat. 
        switch (currentState)
        {
            case PlayerStateController.playerStates.idle:
                // Des de idle es pot passar a qualsevol altre estat 
                returnVal = true;
                break;

            case PlayerStateController.playerStates.right:
                // Des de moving right  es pot passar a qualsevol altre estat 
                returnVal = true;
                break;
            case PlayerStateController.playerStates.jump:
                // Des de  Jump només es pot passar a  landing o a kill. 
                if (newState == PlayerStateController.playerStates.landing
                    || newState == PlayerStateController.playerStates.kill)
                    returnVal = true;
                else
                    returnVal = false;
                break;
            case PlayerStateController.playerStates.landing:
                // Des de landing només es pot passar a idle, left o right. 
                if (newState == PlayerStateController.playerStates.left || newState == PlayerStateController.playerStates.right || newState == PlayerStateController.playerStates.idle)
                {
                    returnVal = true;
                }
                else
                {
                    returnVal = false;
                }
                break;
            case PlayerStateController.playerStates.falling:
                // Des de falling només es pot passar a  landing o a kill. 
                if (newState == PlayerStateController.playerStates.landing || newState == PlayerStateController.playerStates.kill)
                    returnVal = true;
                else
                    returnVal = false;
                break;
            case PlayerStateController.playerStates.kill:
                // Des de kill només es pot passar resurrect 
                if (newState == PlayerStateController.playerStates.resurrect)
                    returnVal = true;
                else
                    returnVal = false;
                break;
            case PlayerStateController.playerStates.resurrect:
                // Des de resurrect només es pot passar Idle 
                if (newState == PlayerStateController.playerStates.idle)
                    returnVal = true;
                else
                    returnVal = false;
                break;
        }
        return returnVal;
    }
    // Aquesta funció comprova si hi ha algun motiu que impedeixi passar al nou estat. 
    // De moment no hi ha cap motiu per cancel·lar cap estat. 
    bool checkIfAbortOnStateCondition(PlayerStateController.playerStates newState)
    {
        bool returnVal = false;
        switch (newState)
        {
            case PlayerStateController.playerStates.idle:
                break;
            case PlayerStateController.playerStates.left:
                break;
            case PlayerStateController.playerStates.right:
                break;
            case PlayerStateController.playerStates.jump:
                break;
            case PlayerStateController.playerStates.landing:
                break;
            case PlayerStateController.playerStates.falling:
                break;
            case PlayerStateController.playerStates.kill:
                break;
            case PlayerStateController.playerStates.resurrect:
                break;
        }
        // Retornar True vol dir 'Abort'. Retornar False vol dir 'Continue'. 
        return returnVal;
    }

    //Metode cridat en caure de la plataforma i col.lisionar amb DeathTrigger 
    public void hitDeathTrigger()
    {
        onStateChange(PlayerStateController.playerStates.kill);
    }

    void aumentarVelocidad()
    {
        // Debug.Log("Velocidad actual: " + aumentarVelocidadBoolean);
        playerWalkSpeed += 4f;
        // Debug.Log("Velocidad actual: " + playerWalkSpeed);
        aumentarVelocidadBoolean = false;
    }

    public static void activarAumentarVelocidad() {
        aumentarVelocidadBoolean = true;
    }

    public void resetStatus() {
        // Debug.Log("Velocidad: " + playerWalkSpeed);
        playerWalkSpeed = 8f;
    }

    public void superSalto()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(2, 20));
    }

}
