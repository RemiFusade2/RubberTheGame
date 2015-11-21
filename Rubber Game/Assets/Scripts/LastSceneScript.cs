using UnityEngine;
using System.Collections;

public class LastSceneScript : MonoBehaviour {

	public Animator bigCompaniesPictureAnimator;

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
			bigCompaniesPictureAnimator.SetBool("Show", true);
		}
	}
}
