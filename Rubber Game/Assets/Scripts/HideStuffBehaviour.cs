﻿using UnityEngine;
using System.Collections;

public class HideStuffBehaviour : MonoBehaviour {

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
			this.gameObject.SetActive(false);
		}
	}
}
