using UnityEngine;
using System.Collections;

public class CollectibleBehaviour : MonoBehaviour {

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
			this.GetComponent<AudioSource>().Play();
			this.GetComponent<Animator>().SetTrigger("Crush");
			PlayerBehaviour playerScript = col.GetComponent<PlayerBehaviour>();
			playerScript.IncreaseScore(1);
		}
	}
}
