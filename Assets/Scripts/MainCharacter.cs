using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Transform JumpParticlesTransform;
    public ShockWaveManager _ShockWaveManager;
    
    

private float pressThreshold = 0.1f;
private float holdThreshold = 0.1f;
private float spaceKeyHeldStartTime = 0f;
private Vector2 NormalVectorCollision;
private string levelToLoad;
private Coroutine lerpCoroutine;
private float heldDuration = 0f;
private bool bCanPlayHoldJumpAnim = true;
private CameraShake CameraShake;
private Vector2 LastNormalVectorCollision = new Vector2(0, 0);


void Start()
{
    SetPlayerOnStartPoint();
}

void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Wall"))
    {
        StopLerp();
        rb2D.gravityScale = 0;
        rb2D.velocity = Vector2.zero;
        LastNormalVectorCollision = collision.contacts[0].normal;

        Quaternion targetRotation = Quaternion.FromToRotation(Vector2.up, LastNormalVectorCollision);
        transform.rotation = targetRotation;

        CameraShake = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CameraShake>();
        if (CameraShake != null)
        CameraShake.ShakeCamera();
    
        Instantiate(CollisionParticles, collision.contacts[0].point, Quaternion.identity);
        _ShockWaveManager.CallShockWave();
        
        
    }

    if (collision.gameObject.CompareTag("KillZone"))
    {
        RestartLevel();
    }
}

void Update()
{
    if (Input.GetKeyDown(KeyCode.Space))
    {
        spaceKeyHeldStartTime = Time.time;
        
    }
    if (Input.GetKey(KeyCode.Space)) 
    {
        if (Time.time - spaceKeyHeldStartTime > holdThreshold && bCanPlayHoldJumpAnim) 
        {
            animator.SetBool("JumpHold", true);
            bCanPlayHoldJumpAnim = false;
        }
    }

    if (Input.GetKeyUp(KeyCode.Space))
    {
        heldDuration = Time.time - spaceKeyHeldStartTime;

        if (heldDuration < pressThreshold)
        {
            OnSpaceKeyPressed();
            ResetSpaceKeyHeldStartTime();
        }
        else if (heldDuration > holdThreshold)
        {
            OnSpaceKeyHeld();
            ResetSpaceKeyHeldStartTime();
        }      
        
    }
   
}

void OnSpaceKeyPressed()

{
    animator.SetTrigger("Jump");
  

        Vector2 Direction = GetClosestSlimePiece().transform.position - transform.position;
        RaycastHit2D Hit;

        Debug.DrawRay(transform.position, Direction, Color.red, 2.0f);

        if (Hit = Physics2D.Linecast(transform.position, GetClosestSlimePiece().transform.position))
        {
            Debug.Log(Hit.collider.tag);
            if (Hit.collider.CompareTag("Wall"))
            {
                
                lerpCoroutine = StartCoroutine(LerpToPosition(Hit.point));               
            }
            
        }
        else
        {
            GameObject closestSlimePiece = GetClosestSlimePiece();
            
            if (closestSlimePiece != null)
            {
                lerpCoroutine = StartCoroutine(LerpToPosition(closestSlimePiece.transform.position));
                Destroy(closestSlimePiece);
            }
        }
    
}

void OnSpaceKeyHeld()
{
    Quaternion targetRotation = Quaternion.FromToRotation(Vector2.up, LastNormalVectorCollision);
    Instantiate(JumpParticles, JumpParticlesTransform.position, targetRotation);
    
    float holdDuration = Mathf.Clamp(Time.time - spaceKeyHeldStartTime, 0f, maxHoldDuration);
    float forceMagnitude = Mathf.Lerp(0f, maxForce, holdDuration / maxHoldDuration);

    rb2D.gravityScale = GravityForce;

    if (LastNormalVectorCollision != new Vector2(0, 0)) {
        rb2D.AddForce(LastNormalVectorCollision * forceMagnitude, ForceMode2D.Impulse);
    } else {
        rb2D.AddForce(Vector2.up * forceMagnitude, ForceMode2D.Impulse);
        
    }

    if (animator != null)
    {
        animator.SetBool("JumpHold", false);
        
        bCanPlayHoldJumpAnim = true;
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


void SetPlayerOnStartPoint()
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
        float elapsedTime = 0f;
        Vector3 initialPosition = playerTransform.position;

        while (elapsedTime < 1f)
        {
            playerTransform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime);
            elapsedTime += Time.deltaTime * lerpSpeed;
            yield return null;
        }

        
        playerTransform.position = targetPosition;
    }

    void StopLerp()
    {
        
        if (lerpCoroutine != null)
        {
            StopCoroutine(lerpCoroutine);
            lerpCoroutine = null;
        }
    }

}