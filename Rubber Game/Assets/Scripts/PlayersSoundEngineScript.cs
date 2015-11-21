using UnityEngine;
using System.Collections;

public class PlayersSoundEngineScript : MonoBehaviour {

	public AudioClip introSpeech;
	public AudioClip outroSpeech;
	public AudioClip attendezSpeech;

	private float lastSpeechTime;
	private AudioClip lastSpeechClip;

	public Animator BackgroundMusicAnimator;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayIntro()
	{
		BackgroundMusicAnimator.SetBool ("MaxVolume", false);
		this.GetComponent<AudioSource> ().Stop ();
		this.GetComponent<AudioSource> ().clip = introSpeech;
		this.GetComponent<AudioSource> ().Play ();
		lastSpeechTime = Time.time;
		lastSpeechClip = introSpeech;
	}
	
	public void PlayOutro()
	{
		BackgroundMusicAnimator.SetBool ("MaxVolume", false);
		this.GetComponent<AudioSource> ().Stop ();
		this.GetComponent<AudioSource> ().clip = outroSpeech;
		this.GetComponent<AudioSource> ().Play ();
		lastSpeechTime = Time.time;
		lastSpeechClip = outroSpeech;
	}

	public void PlayAttendez()
	{
		BackgroundMusicAnimator.SetBool ("MaxVolume", false);
		this.GetComponent<AudioSource> ().Stop ();
		this.GetComponent<AudioSource> ().clip = attendezSpeech;
		this.GetComponent<AudioSource> ().Play ();
		lastSpeechTime = Time.time;
		lastSpeechClip = attendezSpeech;
	}
}
