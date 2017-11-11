﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureTape : MonoBehaviour
{
    public GameObject Skeleton;

    public GameObject ControllerLeft;
    private SteamVR_TrackedController _trackedLeftController;
    public GameObject ControllerRight;

    public GameObject TapeContainer;
    public GameObject Text;
    private TextMesh _textMesh;
    private Material _material;

	// Use this for initialization
	void Start ()
	{
	    _textMesh = Text.GetComponent<TextMesh>();
	    Text.transform.parent = ControllerLeft.transform;
	    _material = GetComponent<MeshRenderer>().material;
	    _trackedLeftController = ControllerLeft.GetComponent<SteamVR_TrackedController>();

	    _trackedLeftController.PadClicked += (sender, args) => { enabled = true; };
	    _trackedLeftController.PadUnclicked += (sender, args) => { enabled = false; };
	}

    // Update is called once per frame
    void Update ()
	{
	    var left = ControllerLeft.transform.position;
	    var right = ControllerRight.transform.position;
        TapeContainer.transform.position = (left + right) / 2;
	    var rotation = Quaternion.LookRotation(right - left);
	    gameObject.transform.rotation = rotation;
	    var scale = Vector3.Distance(left, right);
	    gameObject.transform.localScale = new Vector3(0.0001f, 0.02f, scale);

	    var s = (scale / (10 * Skeleton.transform.localScale.x)).ToString();
        _textMesh.text = s.Substring(0, Math.Min(4, s.Length)) + "cm";

        _material.mainTextureScale = new Vector2(scale * 10, 1);
	}
}
