using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteAnimation : MonoBehaviour
{
    public GameObject TopGroup;
    public GameObject BottomGroup;

    public SteamVR_TrackedController Controller;

    public GameObject Mouse;

    public GameObject[] Top;
    public GameObject[] Bottom;

    private bool _active;

    // Use this for initialization
	void Start () {
	    Controller.PadClicked += (sender, args) =>
	    {
	        if (-args.padY > Math.Abs(args.padX) && args.padY < 0f)
	            ToggleAnimation();
	    };
    }
	
	// Update is called once per frame
	void Update ()
	{
	}

    void ToggleAnimation()
    {
        _active = !_active;

        Mouse.GetComponent<MeshRenderer>().enabled = _active;

        if (_active)
            StartAnimation();
        else
            StopAnimation();
    }

    void StartAnimation()
    {
        TopGroup.transform.rotation = transform.rotation;
        BottomGroup.transform.rotation = transform.rotation;

        foreach (var bone in Top)
        {
            bone.transform.parent = TopGroup.transform;
        }
        foreach (var bone in Bottom)
        {
            bone.transform.parent = BottomGroup.transform;
        }
    }

    void StopAnimation()
    {
        foreach (var bone in Top)
        {
            bone.transform.parent = transform;
            bone.transform.position = transform.position;
            bone.transform.rotation = transform.rotation * Quaternion.Euler(-90f, 0f, 0f);
        }
        foreach (var bone in Bottom)
        {
            bone.transform.parent = transform;
            bone.transform.position = transform.position;
            bone.transform.rotation = transform.rotation * Quaternion.Euler(-90f, 0f, 0f);
        }
    }
}
