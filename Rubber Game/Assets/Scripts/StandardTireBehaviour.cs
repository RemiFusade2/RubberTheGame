using UnityEngine;
using System.Collections;

public class StandardTireBehaviour : MonoBehaviour {

	public Transform positionToReach;

	public float speed;

	public Animator rubberAnimator;

	// Use this for initialization
	void Start () {
	
		rubberAnimator.SetTrigger ("Birth");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.tag.Equals("Player"))
		{
			WakeUp();
		}
	}

	public void WakeUp()
	{
		this.GetComponent<Animator> ().SetTrigger ("WakeUp");
	}

	public void MoveTireToPosition()
	{
		Vector3 currentPosition = this.transform.position;
		float timer = 0;
		float currentMagnitude = (currentPosition - positionToReach.position).magnitude;
		float deltaTime = 0.03f;
		while ( currentMagnitude > 0.1f )
		{
			currentMagnitude = (currentPosition - positionToReach.position).magnitude;
			Vector3 deltaMovement = (positionToReach.position-currentPosition).normalized * speed * deltaTime;
			if (currentMagnitude < (speed*deltaTime))
			{
				deltaMovement = positionToReach.position-currentPosition;
			}
			StartCoroutine(WaitAndMoveTire(timer, deltaMovement));
			currentPosition += deltaMovement;
			timer += deltaTime;
		}
		StartCoroutine(WaitAndPutTireInPlayerFrame(timer));
	}

	IEnumerator WaitAndMoveTire(float timer, Vector3 deltaMovement)
	{
		yield return new WaitForSeconds (timer);
		this.transform.LookAt (this.transform.position + deltaMovement);
		this.transform.position += deltaMovement;
	}
	
	IEnumerator WaitAndPutTireInPlayerFrame(float timer)
	{
		yield return new WaitForSeconds (timer);
		this.transform.LookAt (this.transform.localPosition + this.transform.forward);
		this.transform.parent = GameObject.Find ("Player").transform;
	}
}
