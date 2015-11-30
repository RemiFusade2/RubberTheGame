using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class IntroCarBehaviour : MonoBehaviour {

	public Animator introPanelAnimator;
	public Animator copAnimator;

	public Animator bkgMusicAnimator;

	public AudioSource audioEngine;

	public Text UIMessageText;

	public List<AudioClip> dialogueVoices;
	public List<float> dialogueVoicesTimerToNext;

	public List<string> dialogueScripts;
	public List<float> dialogueScriptsTimerToNext;

	public int debugStartingStateVoice;
	public int debugStartingStateScript;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowIntroDialogue()
	{
		introPanelAnimator.SetBool ("Visible", true);

		StartCoroutine(WaitAndSwitchToNextVoice(1.0f, debugStartingStateVoice));
		StartCoroutine(WaitAndSwitchToNextScript(1.0f, debugStartingStateScript));

		bkgMusicAnimator.SetBool ("MaxVolume", false);
	}
	
	IEnumerator WaitAndSwitchToNextVoice(float timer, int voiceIndex)
	{
		yield return new WaitForSeconds (timer);
		
		if (voiceIndex < dialogueVoices.Count)
		{
			if (voiceIndex == 7)
			{
				this.GetComponent<Animator>().SetTrigger("Jumps");
				copAnimator.SetBool("isTalking", false);
				audioEngine.clip = dialogueVoices[voiceIndex];
				audioEngine.Play();
			}
			else
			{
				copAnimator.SetBool("isTalking", true);
				audioEngine.clip = dialogueVoices[voiceIndex];
				audioEngine.Play();
			}
			StartCoroutine(WaitAndSwitchToNextVoice(dialogueVoicesTimerToNext[voiceIndex], voiceIndex+1));
		}
		else
		{
			copAnimator.SetBool("isTalking", false);
			audioEngine.Stop();
			introPanelAnimator.SetBool ("Visible", false);
			StartCoroutine(WaitAndShowLoadingScreen(1.0f));
		}
	}

	
	IEnumerator WaitAndSwitchToNextScript(float timer, int scriptIndex)
	{
		yield return new WaitForSeconds (timer);
		
		if (scriptIndex < dialogueScripts.Count)
		{
			UIMessageText.text = dialogueScripts[scriptIndex];
			StartCoroutine(WaitAndSwitchToNextScript(dialogueScriptsTimerToNext[scriptIndex], scriptIndex+1));
		}
		else
		{
			UIMessageText.text = "";
		}
	}

	public GameObject loadingScreen;
	
	IEnumerator WaitAndShowLoadingScreen(float timer)
	{
		yield return new WaitForSeconds (timer);
		loadingScreen.SetActive (true);
		StartCoroutine(WaitAndStartGame(2.0f));
	}

	IEnumerator WaitAndStartGame(float timer)
	{
		yield return new WaitForSeconds (timer);
		Application.LoadLevel ("sideScrolling");
	}

	public void PlayCarSound()
	{
		this.GetComponent<AudioSource> ().Play ();
	}
}
