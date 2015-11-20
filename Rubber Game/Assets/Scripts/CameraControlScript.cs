using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraControlScript : MonoBehaviour {

	public List<GameObject> availableCameras;

	private int currentCameraIndex;

	// Use this for initialization
	void Start () 
	{
		currentCameraIndex = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.KeypadPlus))
		{
			currentCameraIndex++;
			currentCameraIndex = (currentCameraIndex >= availableCameras.Count) ? 0 : currentCameraIndex;
			DeactivateAllCamera();
			availableCameras[currentCameraIndex].SetActive( true );
		}
		if (Input.GetKeyDown(KeyCode.KeypadMinus))
		{
			currentCameraIndex--;
			currentCameraIndex = (currentCameraIndex < 0) ? availableCameras.Count-1 : currentCameraIndex;
			DeactivateAllCamera();
			availableCameras[currentCameraIndex].SetActive( true );
		}
	}

	private void DeactivateAllCamera()
	{
		foreach (GameObject cam in availableCameras)
		{
			cam.SetActive( false );
		}
	}
}
