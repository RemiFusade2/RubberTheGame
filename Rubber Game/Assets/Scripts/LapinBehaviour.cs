﻿using UnityEngine;
using System.Collections;

public class LapinBehaviour : MonoBehaviour {

	public GameObject visualGameObject;

	public GameObject bloodParticleSystem;

	public TextMesh qteButtonTextMesh;

	private string qteButtonName;

	private GameObject player;
	private GameObject bkgAudioEngine;

	private PlayerBehaviour getPlayerBehaviourScript()
	{
		if (player == null)
		{
			player = GameObject.Find("Player");
		}
		if (player != null)
		{
			return player.GetComponent<PlayerBehaviour>();
		}
		return null;
	}
	private GameObject getBkgMusicGameObject()
	{
		if (bkgAudioEngine == null)
		{
			bkgAudioEngine = GameObject.Find("BkgAudioEngine");
		}
		return bkgAudioEngine;
	}

	// Use this for initialization
	void Start () 
	{
		isExploding = false;
		int r = Random.Range (0, 3);
		switch (r)
		{
		case 0:
			qteButtonName = "X";
			qteButtonTextMesh.color = Color.blue;
			break;
		case 1:
			qteButtonName = "Y";
			qteButtonTextMesh.color = new Color(0,0.75f,0);
			break;
		case 2:
			qteButtonName = "A";
			qteButtonTextMesh.color = Color.red;
			break;
		case 3:
			qteButtonName = "B";
			qteButtonTextMesh.color = Color.yellow;
			break;
		default:
			break;
		}
		qteButtonTextMesh.text = qteButtonName;
	}

	public bool visibleByCamera;

	private void OnBecameVisible()
	{
		visibleByCamera = true;
	}
	
	private void OnBecameInvisible()
	{
		visibleByCamera = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (visibleByCamera)
		{
			if ((qteButtonName.Equals("X") && Input.GetKeyDown(KeyCode.X)) ||
			    (qteButtonName.Equals("Y") && Input.GetKeyDown(KeyCode.Y)) ||
			    (qteButtonName.Equals("A") && Input.GetKeyDown(KeyCode.A)) ||
			    (qteButtonName.Equals("B") && Input.GetKeyDown(KeyCode.B))    )
			{
				qteButtonTextMesh.text = "";
				PlayerBehaviour playerScript = getPlayerBehaviourScript();
				playerScript.SlowDownAndUseTelekinesisOnRabbit(this.gameObject);
				GameObject bkgMusicEngine = getBkgMusicGameObject();
				bkgMusicEngine.GetComponent<Animator>().SetBool("MaxVolume", false);
			}
		}
	}

	private bool isExploding;

	public void Explode()
	{
		if (!isExploding)
		{
			isExploding = true;
			this.GetComponent<Animator>().SetTrigger("Explode");
		}
	}

	public void ShowBloodParticles()
	{
		bloodParticleSystem.SetActive(true);
		this.GetComponent<AudioSource>().Play();
		PlayerBehaviour playerScript = getPlayerBehaviourScript();
		playerScript.IncreaseScore(10);
		StartCoroutine (WaitAndPutVolumeToMax (1.0f));
	}

	IEnumerator WaitAndPutVolumeToMax(float timer)
	{
		yield return new WaitForSeconds (timer);
		GameObject bkgMusicEngine = getBkgMusicGameObject();
		bkgMusicEngine.GetComponent<Animator>().SetBool("MaxVolume", true);
	}
}
