using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	
	// How long the object should shake for.
	public float shakeDuration = 0f;
	public float _duration;
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
	public bool shaking = false;
	Vector3 originalPos;
	
	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}
	
	void OnEnable()
	{
		originalPos = camTransform.localPosition;
		_duration = shakeDuration;
	}

	void Update()
	{
		if (_duration > 0 && shaking)
		{
			Debug.Log("Shake");
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

			_duration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			_duration = 0f;
			shaking = false;
			camTransform.localPosition = originalPos;
		}
	}
	public void shake(){
		shaking = true;
		_duration = shakeDuration;
	}
}