using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLabel : MonoBehaviour {
	public GameObject HMD;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation =
	            Quaternion.LookRotation(transform.position - HMD.transform.position);
	}
}
