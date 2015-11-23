using UnityEngine;
using System.Collections;

public class LapinBehaviour : MonoBehaviour {

	public GameObject visualGameObject;

	public GameObject bloodParticleSystem;

	public TextMesh qteButtonTextMesh;

	private string qteButtonName;

	private GameObject player;
	private GameObject bkgAudioEngine;

	public Animator BonusAnimator;

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
			qteButtonTextMesh.color = Color.yellow;
			break;
		case 2:
			qteButtonName = "A";
			qteButtonTextMesh.color = new Color(0,0.75f,0);
			break;
		case 3:
			qteButtonName = "B";
			qteButtonTextMesh.color = Color.red;
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
		PlayerBehaviour playerScript = getPlayerBehaviourScript ();
		if (visibleByCamera && playerScript != null && !playerScript.gameIsEnding)
		{
			if ((qteButtonName.Equals("X") && (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("Fire3"))) ||
			    (qteButtonName.Equals("Y") && (Input.GetKeyDown(KeyCode.Y) || Input.GetButtonDown("Jump"))) ||
			 	(qteButtonName.Equals("A") && (Input.GetKeyDown(KeyCode.A) || Input.GetButtonDown("Fire1"))) ||
			 	(qteButtonName.Equals("B") && (Input.GetKeyDown(KeyCode.B) || Input.GetButtonDown("Fire2")))    )
			{
				qteButtonTextMesh.text = "";
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
		BonusAnimator.SetTrigger("BonusShows");
	}

	IEnumerator WaitAndPutVolumeToMax(float timer)
	{
		yield return new WaitForSeconds (timer);
		GameObject bkgMusicEngine = getBkgMusicGameObject();
		bkgMusicEngine.GetComponent<Animator>().SetBool("MaxVolume", true);
	}
}
