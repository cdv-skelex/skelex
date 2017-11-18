﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class CatScan : MonoBehaviour
{
    public SteamVR_TrackedController Controller;
    public GameObject ReferenceSkull;
    public GameObject ReferencePlane;
    public GameObject StudioSkull;
    public GameObject StudioCamera;

    private bool _active;

	// Use this for initialization
	void Start ()
	{
	    Controller.MenuButtonClicked += (sender, args) => { Toggle(); };
	    ReferencePlane.GetComponent<MeshRenderer>().enabled = _active;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    StudioSkull.transform.rotation = ReferenceSkull.transform.rotation * Quaternion.Inverse(ReferencePlane.transform.rotation * Quaternion.Euler(90f, 0f, 0f));

	    if (_active)
	    {
	        
	    }
	}

    void Toggle()
    {
        _active = !_active;
        ReferencePlane.GetComponent<MeshRenderer>().enabled = _active;
    }
}
