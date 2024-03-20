using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveManager : MonoBehaviour
{
    [SerializeField]
    private float _shockWaveTime = 0.75f;
    private Coroutine _shockWaveCoroutine;
    private Material material;
    private static int _waveDistanceFromCenter = Shader.PropertyToID("_WaveDistanceFromCenter");
    private static float _ringSpawnPosition = Shader.PropertyToID("RingSpawnPosition");

    private void Start()
    {
        
    }

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    public void CallShockWave()
    {
        _shockWaveCoroutine = StartCoroutine(ShockWaveAction(-0.1f, 1f));
    }


    private void update()
    {
    
    }
    private IEnumerator ShockWaveAction(float startValue, float endValue)
    {
        material.SetFloat(_waveDistanceFromCenter, startValue);

        float lerpedAmount = 0f;

        float elapsedTime = 0f;
        while (elapsedTime < _shockWaveTime)
        {
            elapsedTime += Time.deltaTime;
            lerpedAmount = Mathf.Lerp(startValue, endValue, elapsedTime / _shockWaveTime);
            material.SetFloat(_waveDistanceFromCenter, lerpedAmount);
            
            yield return null;
        }
    }

    public void UpdateRingSpawnPosition(float x, float y)
    {
        //material.SetFloat(_ringSpawnPosition, )
    }
}
