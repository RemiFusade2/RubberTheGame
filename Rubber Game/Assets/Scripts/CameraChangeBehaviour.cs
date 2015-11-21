using UnityEngine;
using System.Collections;

public class CameraChangeBehaviour : MonoBehaviour 
{
	public Camera previousCamera;
	public Animator previousCameraAnimator;

	public Camera newCamera;
	public Animator newCameraAnimator;

	public bool lookup;
	
	public GameObject objectToShow;
	public GameObject objectToHide;

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
			previousCameraAnimator.SetBool("Visible", false);
			newCameraAnimator.SetBool("Visible", true);
			if (lookup)
			{
				newCameraAnimator.SetTrigger("LookUp");
			}
			if (objectToShow != null)
			{
				objectToShow.SetActive(true);
			}
			if (objectToHide != null)
			{
				objectToHide.SetActive(false);
			}
		}
	}


}
