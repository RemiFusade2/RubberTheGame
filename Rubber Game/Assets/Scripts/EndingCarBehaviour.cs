using UnityEngine;
using System.Collections;

public class EndingCarBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayCarSound()
	{
		this.GetComponent<AudioSource> ().Play ();
	}
}
