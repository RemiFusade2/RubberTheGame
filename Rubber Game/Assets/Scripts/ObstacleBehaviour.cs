using UnityEngine;
using System.Collections;

public class ObstacleBehaviour : MonoBehaviour 
{
	public float hitHPLost;
	public float hitSpeedLost;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag.Equals("Player"))
		{
			PlayerBehaviour playerScript = col.GetComponent<PlayerBehaviour>();
			playerScript.HitPlayer(hitHPLost, hitSpeedLost);
		}
	}
}
