using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndingScript : MonoBehaviour 
{
	public PlayerBehaviour playerScript;

	public GameObject dialoguePanel;
	public Animator copAnimator;
	public Text dialogueTextBox;

	public AudioSource gunFireAudioSource;

	public Animator carAnimator;

	public GameObject Level;
	public GameObject Player;
	public GameObject Rubber;
	public GameObject Trike;

	public CameraChangeBehaviour cameraChange1;
	public CameraChangeBehaviour cameraChange2;
	public CameraChangeBehaviour cameraChange3;
	public StopPlayerScript stopPlayerScript;
	public GameObject objectToShowCamera1;
	public GameObject objectToHideCamera2;

	private bool endingStarted;

	public LastSceneScript lastSceneScript;

	void Start()
	{
		endingStarted = false;
	}

	public void SetEndPanel(GameObject endPanel, Text scoreText)
	{
		lastSceneScript.EndPanel = endPanel;
		lastSceneScript.ScoreText = scoreText;
	}

	public void SetCameras(Camera camera1, Camera camera2, Camera camera3, Camera camera4)
	{
		cameraChange1.previousCamera = camera1;
		cameraChange1.previousCameraAnimator = camera1.GetComponent<Animator> ();
		cameraChange1.newCamera = camera2;
		cameraChange1.newCameraAnimator = camera2.GetComponent<Animator> ();
		
		cameraChange2.previousCamera = camera2;
		cameraChange2.previousCameraAnimator = camera2.GetComponent<Animator> ();
		cameraChange2.newCamera = camera3;
		cameraChange2.newCameraAnimator = camera3.GetComponent<Animator> ();
		
		cameraChange3.previousCamera = camera3;
		cameraChange3.previousCameraAnimator = camera3.GetComponent<Animator> ();
		cameraChange3.newCamera = camera4;
		cameraChange3.newCameraAnimator = camera4.GetComponent<Animator> ();

		cameraChange1.objectToShow = objectToShowCamera1;
		cameraChange2.objectToHide = objectToHideCamera2;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag.Equals("Player") && !endingStarted)
		{
			endingStarted = true;
			StartEnding();
		}
	}

	private void StartEnding()
	{
		Debug.Log ("StartEnding");
		playerScript.StopPlayer ();
		dialoguePanel.GetComponent<Animator> ().SetBool ("Visible", true);
		copAnimator.SetBool ("isTalking", false);
		StartCoroutine (WaitAndFireGun (1.0f));
		StartCoroutine (WaitAndKillRubber (2.0f));
		StartCoroutine (WaitAndHideCop (3.0f));
		StartCoroutine (WaitAndCarGoesAway (4.0f));

		float timeToMoveTrike = 2.0f;
		float timeOffset = 5.0f;
		for (int i = 0 ; i < 100 ; i++)
		{
			StartCoroutine (WaitAndMoveTrike (timeOffset + timeToMoveTrike * (i/100.0f), 0.05f));
		}
		StartCoroutine (WaitAndPutRubberInLevel (5.0f));
		StartCoroutine (WaitAndPutTrikeAsPlayer (timeOffset + timeToMoveTrike));
		
		StartCoroutine (WaitAndMovePlayer (timeOffset + timeToMoveTrike + 1.0f));
	}

	IEnumerator WaitAndFireGun(float timer)
	{
		yield return new WaitForSeconds (timer);
		gunFireAudioSource.Play ();
	}
	
	IEnumerator WaitAndKillRubber(float timer)
	{
		yield return new WaitForSeconds (timer);
		playerScript.KillRubber ();
	}

	IEnumerator WaitAndHideCop(float timer)
	{
		yield return new WaitForSeconds (timer);
		dialoguePanel.GetComponent<Animator> ().SetBool ("Visible", false);
	}
	
	IEnumerator WaitAndCarGoesAway(float timer)
	{
		yield return new WaitForSeconds (timer);
		carAnimator.GetComponent<Animator> ().SetTrigger ("GoAway");
	}
	
	IEnumerator WaitAndMoveTrike(float timer, float deltaTrikePosition)
	{
		yield return new WaitForSeconds (timer);
		Trike.transform.position += Vector3.right * deltaTrikePosition;
	}
	
	IEnumerator WaitAndPutRubberInLevel(float timer)
	{
		yield return new WaitForSeconds (timer);
		Rubber.GetComponent<Animator> ().enabled = false;
		Rubber.transform.SetParent (Level.transform);
	}
	IEnumerator WaitAndPutTrikeAsPlayer(float timer)
	{
		yield return new WaitForSeconds (timer);
		Trike.transform.SetParent (Player.transform);
	}
	
	IEnumerator WaitAndMovePlayer(float timer)
	{
		yield return new WaitForSeconds (timer);
		playerScript.MovePlayer ();
	}
}
