using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LastSceneScript : MonoBehaviour 
{
	public Animator bigCompaniesPictureAnimator;

	public GameObject EndPanel;
	public Text ScoreText;

	private int score;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.tag.Equals("Player"))
		{
			score = col.gameObject.GetComponent<PlayerBehaviour>().score;
			bigCompaniesPictureAnimator.SetBool("Show", true);
			StartCoroutine(WaitAndShowEndScreen(16.0f, score));
			StartCoroutine(WaitAndKojimaMesCouilles(18.8f));
			StartCoroutine(WaitAndEndGame(20.0f));
		}
	}

	IEnumerator WaitAndShowEndScreen (float timer, int score)
	{
		yield return new WaitForSeconds(timer);
		GameObject.Find ("BkgAudioEngine").GetComponent<Animator> ().SetBool ("MaxVolume", false);
		ScoreText.text = "Score : " + score;
		EndPanel.SetActive (true);
	}
	
	IEnumerator WaitAndKojimaMesCouilles (float timer)
	{
		yield return new WaitForSeconds(timer);
		this.GetComponent<AudioSource> ().Play ();
	}

	IEnumerator WaitAndEndGame (float timer)
	{
		yield return new WaitForSeconds(timer);
		Application.Quit ();
	}
}
