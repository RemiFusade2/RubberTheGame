using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectibleBehaviour : MonoBehaviour {

	public List<AudioClip> listOfSounds;

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
			PlayBottleCrushSound();
			this.GetComponent<Animator>().SetTrigger("Crush");
			PlayerBehaviour playerScript = col.GetComponent<PlayerBehaviour>();
			playerScript.IncreaseScore(1);
		}
	}

	private void PlayBottleCrushSound()
	{
		int r = Random.Range (0, listOfSounds.Count);
		this.GetComponent<AudioSource> ().clip = listOfSounds[r];
		this.GetComponent<AudioSource>().Play();

	}
}
