using UnityEngine;
using System.Collections;

public class SimpleInteractionScript : MonoBehaviour {

	public float force;

	public string menuEffect;

	public Animator introCarAnimator;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		this.GetComponent<Rigidbody> ().AddExplosionForce (force, this.transform.position, 10.0f);

		if (menuEffect != null && !menuEffect.Equals(""))
		{
			if (menuEffect.Equals("Start"))
			{
				introCarAnimator.SetTrigger("StartGame");
			}
		}
	}
}
