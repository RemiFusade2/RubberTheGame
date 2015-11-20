using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	public GameObject player;

	public Vector3 cameraToPlayerTranslation;
	public float cameraForwardPosition;
	
	private bool isShaking;
	private Vector3 shakeTransform;

	private float shakingForce;
	private float shakingForceAcceleration;

	// Use this for initialization
	void Start () 
	{
		shakingForce = 0.01f;
		shakingForceAcceleration = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 cameraPosition = new Vector3 (player.transform.position.x + cameraToPlayerTranslation.x,
		                                      player.transform.position.y + cameraToPlayerTranslation.y,
		                                      cameraForwardPosition );
		if (isShaking)
		{
			shakeTransform = new Vector3(Random.Range(-shakingForce,shakingForce), Random.Range(-shakingForce,shakingForce), Random.Range(-shakingForce,shakingForce));
			shakingForce += shakingForceAcceleration;
			cameraPosition += shakeTransform;
		}

		this.transform.position = cameraPosition;
	}

	public void Shake(float shakingTime, float shakingPower, float shakingAcceleration)
	{
		shakingForce = shakingPower;
		shakingForceAcceleration = shakingAcceleration;
		isShaking = true;
		StartCoroutine(WaitAndStopShaking(shakingTime));
	}

	IEnumerator WaitAndStopShaking(float timer)
	{
		yield return new WaitForSeconds (timer);
		isShaking = false;
	}
}
