using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScan : MonoBehaviour
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
	    _clippable.plane1Position = Plane.transform.position - new Vector3(0f, Plane.transform.localScale.y, 0f);
	    _clippable.plane2Position = Plane.transform.position + new Vector3(0f, Plane.transform.localScale.y, 0f);
        _clippable.plane1Rotation = Plane.transform.rotation.eulerAngles;
	    _clippable.plane2Rotation = Plane.transform.rotation.eulerAngles;
    }
}
