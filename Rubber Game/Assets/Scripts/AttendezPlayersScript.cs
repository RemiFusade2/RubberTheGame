using UnityEngine;
using System.Collections;

public class AttendezPlayersScript : MonoBehaviour {

	public PlayersSoundEngineScript playersSoundEngine;

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
			playersSoundEngine.PlayAttendez();
		}
	}
}
