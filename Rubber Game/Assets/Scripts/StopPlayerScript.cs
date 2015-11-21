using UnityEngine;
using System.Collections;

public class StopPlayerScript : MonoBehaviour {
	
	public PlayerBehaviour playerScript;

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
			playerScript.StopPlayerSlowly ();
		}
	}
}
