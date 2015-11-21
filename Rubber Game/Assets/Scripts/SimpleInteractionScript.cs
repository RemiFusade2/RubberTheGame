using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleInteractionScript : MonoBehaviour {

	public float force;

	public string menuEffect;

	public Animator introCarAnimator;

	public List<AudioClip> collisionSounds;

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

	private float lastCollisionTime;

	void OnCollisionEnter(Collision col)
	{
		float velocity = col.relativeVelocity.magnitude;
		float volumeRatio = velocity / 5.0f;
		
		if (Time.time - lastCollisionTime > 5.0f)
		{
			lastCollisionTime = Time.time;
			int r = Random.Range(0, collisionSounds.Count);
			this.GetComponent<AudioSource>().Stop();
			this.GetComponent<AudioSource>().volume = volumeRatio;
			this.GetComponent<AudioSource>().clip = collisionSounds[r];
			this.GetComponent<AudioSource>().Play();
		}
	}
}
