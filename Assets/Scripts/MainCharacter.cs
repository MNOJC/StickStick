using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField]
    public Transform playerTransform; 
    public Rigidbody2D rb2D;
    public float lerpSpeed = 1.0f;
    public float maxForce = 10f; 
    public float maxHoldDuration = 2f;
    public Animator animator;
    public float GravityForce = 2;
    public ParticleSystem JumpParticles;
    public ParticleSystem CollisionParticles;
    public ParticleSystem DeathParticles;
    public Transform JumpParticlesTransform;
    private ShockWaveManager _ShockWaveManager;
    private CinemachineVirtualCamera virtualCamera;
    public float zoomFov = 8f;
    public float defaultOrthoSize = 9f;
    public float zoomSpeed = 2f;
    public Collider2D PlayerCollider;
        
private LineRendererController line;
private float pressThreshold = 0.2f;
private float holdThreshold = 0.2f;
private float spaceKeyHeldStartTime = 0f;
private Vector2 NormalVectorCollision;
private string levelToLoad;
private Coroutine lerpCoroutine;
private float heldDuration = 0f;
private bool bCanPlayHoldJumpAnim = true;
private CameraShake CameraShake;
private Vector2 LastNormalVectorCollision = new Vector2(0, 0);
private bool bKeyHeld = false;
private bool bPlayerDead = false;
private bool bCanDash = false;
private bool bCanJump = false;
public bool bGameStart = false;
private bool bCanStopLerp;
private Timer timer;

void Awake() 
{
    _ShockWaveManager = GameObject.Find("ShockWaveManager").GetComponent<ShockWaveManager>();
    virtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
    line = GameObject.Find("LineRenderer").GetComponent<LineRendererController>();
    timer = GameObject.Find("CanvaTimer").GetComponent<Timer>();
}
void Start()
{
    
    DisableInput(true);
    SetPlayerOnStartPoint();
    SetUpLine(this.transform, GetClosestSlimePiece().transform);
    
}

void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Wall"))
    {
        animator.SetTrigger("CancelFlip");
        StopPlayer();
        LastNormalVectorCollision = collision.contacts[0].normal;

        bCanDash = true;

        Quaternion targetRotation = Quaternion.FromToRotation(Vector2.up, LastNormalVectorCollision);
        transform.rotation = targetRotation;

        ShakeCamera(4f, 0.3f);
    
        Instantiate(CollisionParticles, collision.contacts[0].point, targetRotation);
        _ShockWaveManager.CallShockWave();
        Debug.Log("Collision with wall");
        
        
        
    }

    if (collision.gameObject.CompareTag("KillZone"))
    {
        Instantiate(DeathParticles, transform.position, Quaternion.identity);
        bCanStopLerp = true;
        PlayerCollider.enabled = false;
        animator.SetTrigger("Dead");
        bPlayerDead = true;
        StopLerp();

        StopPlayer();

        Time.timeScale = 1.0f;
        ShakeCamera(50f, 1f);
        bCanDash = false;

        Invoke("RestartLevel", 1f);
    }

    if (collision.gameObject.CompareTag("SlimePieces"))
    {
        bCanDash = true;
        collision.gameObject.SetActive(false);
        GameObject closestSlimePiece = GetClosestSlimePiece();

        if (closestSlimePiece != null)
        {
            SetUpLine(this.transform, GetClosestSlimePiece().transform);
            Destroy(collision.gameObject);
        } else {
            line.SetCanRender(false);
        }


        
    }
    if (collision.gameObject.CompareTag("GoldenFork"))
    {
       StopPlayer();
       bCanDash = false;
       bCanJump = false;
    }
}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DetectionZone"))
        {
            //Time.timeScale = 0.1f;
            //StartCoroutine(ZoomCamera(true, 40.0f, 8.0f));            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DetectionZone") && !bPlayerDead)
        {
            //Time.timeScale = 1f;
            //StartCoroutine(ZoomCamera(false, 20f, 9.0f));
        }
    }

void Update()
{
    if (Input.GetKeyDown(KeyCode.Space) && bCanJump && bCanDash)
    {
        
        spaceKeyHeldStartTime = Time.time;
        
    }
    if (Input.GetKey(KeyCode.Space) && bCanJump && bCanDash) 
    {
        if (Time.time - spaceKeyHeldStartTime > holdThreshold && bCanPlayHoldJumpAnim && CheckIfPlayerIsGrounded()) 
        {
            StartCoroutine(ZoomCamera(true, 2.0f, 8.8f));
            bKeyHeld = true;
            animator.SetBool("JumpHold", true);
            bCanPlayHoldJumpAnim = false;
        }
    }

    if (Input.GetKeyUp(KeyCode.Space))
    {
        heldDuration = Time.time - spaceKeyHeldStartTime;
                   
        if (heldDuration < pressThreshold)
        {
            if (bCanDash && bGameStart)
            OnSpaceKeyPressed();
            ResetSpaceKeyHeldStartTime();
            bKeyHeld = false;
        }
        else if (heldDuration > holdThreshold)
        {
            if (bCanJump && bGameStart)
            OnSpaceKeyHeld();
            ResetSpaceKeyHeldStartTime();
            bKeyHeld = false;
        }      
        
    }

    GameObject NearestSlimePiece = GetClosestSlimePiece();
    if (NearestSlimePiece != null)
    {
        SetUpLine(this.transform, NearestSlimePiece.transform);
    }

   
}

void OnSpaceKeyPressed()

{

        timer.StartTimer();
        bCanDash = false;
        animator.SetTrigger("Jump");
        RaycastHit2D Hit;
    
        GameObject closestSlimePiece = GetClosestSlimePiece();
        
        if (closestSlimePiece != null)
        {
        if (Hit = Physics2D.Linecast(transform.position, closestSlimePiece.transform.position))
        {
            
            if (Hit.collider.CompareTag("Wall"))
            {
                lerpCoroutine = StartCoroutine(LerpToPosition(Hit.point));
                bCanStopLerp = true;
                             
            }
             
        } else {
            
            if (closestSlimePiece != null)
            {
                lerpCoroutine = StartCoroutine(LerpToPosition(closestSlimePiece.transform.position));
                bCanStopLerp = false;
            }
            
        }
        
        
        }
    
}

void OnSpaceKeyHeld()
{
    timer.StartTimer();
    animator.SetTrigger("Flip");
    Quaternion targetRotation = Quaternion.FromToRotation(Vector2.up, LastNormalVectorCollision);
    Instantiate(JumpParticles, JumpParticlesTransform.position, targetRotation);
    
    float holdDuration = Mathf.Clamp(Time.time - spaceKeyHeldStartTime, 0f, maxHoldDuration);
    float forceMagnitude = Mathf.Lerp(0f, maxForce, holdDuration / maxHoldDuration);

    rb2D.gravityScale = GravityForce;

    if (CheckIfPlayerIsGrounded())
    {
    if (LastNormalVectorCollision != new Vector2(0, 0)) {
        rb2D.AddForce(LastNormalVectorCollision * forceMagnitude, ForceMode2D.Impulse);
        StartCoroutine(ZoomCamera(false, 2.0f, 9.0f));
    } else {
        rb2D.AddForce(Vector2.up * forceMagnitude, ForceMode2D.Impulse);
        StartCoroutine(ZoomCamera(false, 2.0f, 9.0f));
        
    }

    if (animator != null)
    {
        animator.SetBool("JumpHold", false);
        
        bCanPlayHoldJumpAnim = true;
    }     
    }
}

void ResetSpaceKeyHeldStartTime()
{
    spaceKeyHeldStartTime = 0f;
}
GameObject GetClosestSlimePiece()
{
    GameObject[] slimePieces = GameObject.FindGameObjectsWithTag("SlimePieces");

    if (slimePieces.Length == 0)
    {
        return null;
    }

    GameObject closestSlimePiece = slimePieces[0];
    float closestDistance = Vector3.Distance(playerTransform.position, slimePieces[0].transform.position);

    for (int i = 1; i < slimePieces.Length; i++)
    {
        
        float distance = Vector3.Distance(playerTransform.position, slimePieces[i].transform.position);

        if (distance < closestDistance)
        {        
            closestSlimePiece = slimePieces[i];
            closestDistance = distance;
            
            
        }
    }

    return closestSlimePiece;
}


public void SetPlayerOnStartPoint()
{
    playerTransform.position = GameObject.FindGameObjectWithTag("StartPoint").transform.position;
}

void RestartLevel()
{
    levelToLoad = SceneManager.GetActiveScene().name;
    SceneManager.LoadScene(levelToLoad);
    
}

IEnumerator LerpToPosition(Vector3 targetPosition)
    {
        float duration = 0.2f;
        float currentTime = 0f;

        Vector3 initialPosition = playerTransform.position;

        while (currentTime < duration)
        {
            float t = currentTime / duration;
            playerTransform.position = Vector3.Lerp(initialPosition, targetPosition, t);

            currentTime += Time.deltaTime;
            yield return null;
        }
        playerTransform.position = targetPosition;  
    }

    void StopLerp()
    {
        
        if (lerpCoroutine != null && bCanStopLerp)
        {
            StopCoroutine(lerpCoroutine);
            lerpCoroutine = null;
        }
    }

    bool CheckIfPlayerIsGrounded()
    {
        int groundLayerMask = LayerMask.GetMask("Wall");

        Vector2 direction = -LastNormalVectorCollision;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1.0f, groundLayerMask);
        
        return hit.collider != null;
    }

    void SetUpLine(Transform StartPoint, Transform EndPoint)
    {
        if (bKeyHeld) 
        {
            line.SetCanRender(false); 
            GameObject[] slimePieces = GameObject.FindGameObjectsWithTag("SlimePieces"); 
             foreach (GameObject slimePiece in slimePieces)
             {
                slimePiece.transform.GetChild(0).gameObject.SetActive(false);
             }
        } else {

        HideAllLockParticles();   
        Transform[] LocalLinePoints = new Transform[] { StartPoint, EndPoint };

        if (line != null && LocalLinePoints.Length > 1)
        {
            line.SetUpLine(LocalLinePoints);
            line.SetCanRender(true);       
        }
        
        }
        
    }

    void HideAllLockParticles()
    {
        GameObject[] slimePieces = GameObject.FindGameObjectsWithTag("SlimePieces");

        foreach (GameObject slimePiece in slimePieces)
        {
            if (GetClosestSlimePiece() != slimePiece)
            {
                slimePiece.transform.GetChild(0).gameObject.SetActive(false);
            } else {
                slimePiece.transform.GetChild(0).gameObject.SetActive(true);
            }
            
        }
    }

    IEnumerator ZoomCamera(bool shouldZoom, float LocalZoomSpeed, float LocalZoomFov)
    {
        
        float elapsedTime = 0f;
        float initialSize = virtualCamera.m_Lens.OrthographicSize;
        float targetSize = shouldZoom ? LocalZoomFov : defaultOrthoSize;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * LocalZoomSpeed;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(initialSize, targetSize, elapsedTime);
            yield return null;
        }

        virtualCamera.m_Lens.OrthographicSize = targetSize;
    }

    void ShakeCamera (float Intensity, float ShakeTime)
    {
        CameraShake = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CameraShake>();

            if (CameraShake != null)
            {
                CameraShake.ShakeIntensity = Intensity;
                CameraShake.ShakeTime = ShakeTime;
                CameraShake.ShakeCamera();
            }

    }  

    void StopPlayer()
    {
        rb2D.velocity = Vector2.zero;
        rb2D.gravityScale = 0;
    }     

    public void DisableInput(bool bDisable)
    {
        if (bDisable)
        {
            bCanDash = false;
            bCanJump = false;
        } else {
            rb2D.gravityScale = GravityForce;
            bCanDash = true;
            bCanJump = true;
        }
    }
}