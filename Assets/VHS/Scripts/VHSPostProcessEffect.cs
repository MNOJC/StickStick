using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using static UnityEngine.Rendering.DebugUI;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/GlitchEffect")]
[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(VideoPlayer))]
public class VHSPostProcessEffect : MonoBehaviour
{
	public Shader shader;
	public VideoClip VHSClip;
	public float duration = 3.0f;

	private float _yScanline;
	private float _xScanline;
	private Material _material = null;
	private VideoPlayer _player;

	void Start()
	{
		_material = new Material(shader);
		_player = GetComponent<VideoPlayer>();
		_player.isLooping = true;
		_player.audioOutputMode = VideoAudioOutputMode.None;
		_player.clip = VHSClip;
		_player.Play();
		StartCoroutine(LerpAlpha(2.0f, 0.4f, duration));
    }

	

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		_material.SetTexture("_VHSTex", _player.texture);

		_yScanline += Time.deltaTime * 0.01f;
		_xScanline -= Time.deltaTime * 0.1f;

		if (_yScanline >= 1)
		{
			_yScanline = Random.value;
		}
		if (_xScanline <= 0 || Random.value < 0.05)
		{
			_xScanline = Random.value;
		}
		_material.SetFloat("_yScanline", _yScanline);
		_material.SetFloat("_xScanline", _xScanline);
		Graphics.Blit(source, destination, _material);

		
	}

	protected void OnDisable()
	{
		if (_material)
		{
			DestroyImmediate(_material);
		}
	}

	IEnumerator LerpAlpha(float startValue, float endValue, float duration)
	{
        float percent = 0;
        float timeFactor = 1 / duration;
        while (percent < 1)
        {
            percent += Time.deltaTime * timeFactor;
            _player.targetCameraAlpha = Mathf.Lerp(startValue, endValue, Mathf.SmoothStep(0, 1, percent));
            yield return null;
        }
    }
}
