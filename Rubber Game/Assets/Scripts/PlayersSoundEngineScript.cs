using UnityEngine;
using System.Collections;

public class PlayersSoundEngineScript : MonoBehaviour {

	public AudioClip introSpeech;
	public AudioClip outroSpeech;
	public AudioClip attendezSpeech;
	public AudioClip scoringSpeech;
	
	public AudioClip pasDeCactusSpeech;
	public AudioClip vousPensezQueSpeech;
	public AudioClip cestNulSpeech;

	private float lastSpeechTime;
	private AudioClip lastSpeechClip;

	public Animator BackgroundMusicAnimator;

	public float timeBeforePlaySound;

	private Coroutine putVolumeToMax;

	public float timerBeforeTimeSpeech;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (WaitAndTryToPlayTimeSpeech (timerBeforeTimeSpeech));
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	private bool playOutro;

	IEnumerator WaitAndTryToPlayTimeSpeech(float timer)
	{
		yield return new WaitForSeconds (timer);
		if (!pasDeCactusHasBeenPlayed)
		{
			PlayPasDeCactus();
		}
		else if (!vousPensezQueHasBeenPlayed)
		{
			PlayVousPensezQue();
		}
		else if (!cestNulHasBeenPlayed)
		{
			PlayCestNul();
		}
		if (!playOutro)
		{
			StartCoroutine (WaitAndTryToPlayTimeSpeech (timer));
		}
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
		if (putVolumeToMax != null)
		{
			StopCoroutine(putVolumeToMax);
		}
		playOutro = true;
		BackgroundMusicAnimator.SetBool ("MaxVolume", false);
		StartCoroutine(WaitAndPlayAudioClip(timeBeforePlaySound, outroSpeech));
	}

	private bool scoringHasBeenPlayed;
	public void PlayScoring()
	{
		if (!scoringHasBeenPlayed && (Time.time - lastSpeechTime) > 30.0f)
		{
			if (putVolumeToMax != null)
			{
				StopCoroutine(putVolumeToMax);
			}
			BackgroundMusicAnimator.SetBool ("MaxVolume", false);
			scoringHasBeenPlayed = true;
			StartCoroutine(WaitAndPlayAudioClip(timeBeforePlaySound, scoringSpeech));
			putVolumeToMax = StartCoroutine(WaitAndPutBackgroundMusicToMaxVolume(timeBeforePlaySound+scoringSpeech.length+1.0f));
		}
	}
	
	private bool pasDeCactusHasBeenPlayed;
	public void PlayPasDeCactus()
	{
		if (!pasDeCactusHasBeenPlayed && (Time.time - lastSpeechTime) > 30.0f)
		{
			if (putVolumeToMax != null)
			{
				StopCoroutine(putVolumeToMax);
			}
			BackgroundMusicAnimator.SetBool ("MaxVolume", false);
			pasDeCactusHasBeenPlayed = true;
			StartCoroutine(WaitAndPlayAudioClip(timeBeforePlaySound, pasDeCactusSpeech));
			putVolumeToMax = StartCoroutine(WaitAndPutBackgroundMusicToMaxVolume(timeBeforePlaySound+pasDeCactusSpeech.length+1.0f));
		}
	}
	private bool vousPensezQueHasBeenPlayed;
	public void PlayVousPensezQue()
	{
		if (!vousPensezQueHasBeenPlayed && (Time.time - lastSpeechTime) > 30.0f)
		{
			if (putVolumeToMax != null)
			{
				StopCoroutine(putVolumeToMax);
			}
			BackgroundMusicAnimator.SetBool ("MaxVolume", false);
			vousPensezQueHasBeenPlayed = true;
			StartCoroutine(WaitAndPlayAudioClip(timeBeforePlaySound, vousPensezQueSpeech));
			putVolumeToMax = StartCoroutine(WaitAndPutBackgroundMusicToMaxVolume(timeBeforePlaySound+vousPensezQueSpeech.length+1.0f));
		}
	}
	private bool cestNulHasBeenPlayed;
	public void PlayCestNul()
	{
		if (!cestNulHasBeenPlayed && (Time.time - lastSpeechTime) > 30.0f)
		{
			if (putVolumeToMax != null)
			{
				StopCoroutine(putVolumeToMax);
			}
			BackgroundMusicAnimator.SetBool ("MaxVolume", false);
			cestNulHasBeenPlayed = true;
			StartCoroutine(WaitAndPlayAudioClip(timeBeforePlaySound, cestNulSpeech));
			putVolumeToMax = StartCoroutine(WaitAndPutBackgroundMusicToMaxVolume(timeBeforePlaySound+cestNulSpeech.length+1.0f));
		}
	}

	public void PlayAttendez()
	{
		BackgroundMusicAnimator.SetBool ("MaxVolume", false);
		StartCoroutine(WaitAndPlayAudioClip(timeBeforePlaySound, attendezSpeech));
	}

	IEnumerator WaitAndPlayAudioClip(float timer, AudioClip clip)
	{
		yield return new WaitForSeconds (timer);
		BackgroundMusicAnimator.SetBool ("MaxVolume", false);
		this.GetComponent<AudioSource> ().Stop ();
		this.GetComponent<AudioSource> ().clip = clip;
		this.GetComponent<AudioSource> ().Play ();
		lastSpeechTime = Time.time;
		lastSpeechClip = clip;
	}

	IEnumerator WaitAndPutBackgroundMusicToMaxVolume(float timer)
	{
		yield return new WaitForSeconds (timer);
		BackgroundMusicAnimator.SetBool ("MaxVolume", true);
	}
}
