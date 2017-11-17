using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject Plane;
    private ClippableObject _clippable;

	// Use this for initialization
	void Start ()
	{
	    _clippable = GetComponent<ClippableObject>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _clippable.plane1Position = Plane.transform.position;
	    _clippable.plane1Rotation = Plane.transform.rotation.eulerAngles;
	}
}
