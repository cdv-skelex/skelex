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

    private GameObject _skull;

    public GameObject[] Top;
    public GameObject[] Bottom;

    private bool _active;

    private Animator _animator;

    // Use this for initialization
	void Start () {
	    Controller.PadClicked += (sender, args) =>
	    {
	        if (-args.padY > Math.Abs(args.padX) && args.padY < 0f)
	            ToggleAnimation();
	    };

	    _skull = transform.parent.gameObject;

	    _animator = _skull.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var dist = Vector3.Distance(transform.position, Controller.transform.position);
	    _animator.speed = 2f / dist;
	}

    void ToggleAnimation()
    {
        _active = !_active;

        if (_active)
            StartAnimation();
        else
            StopAnimation();
    }

    void StartAnimation()
    {
        foreach (var renderer in Mouse.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.enabled = true;
        }
        TopGroup.transform.rotation = _skull.transform.rotation;
        BottomGroup.transform.rotation = _skull.transform.rotation;

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
        foreach (var renderer in Mouse.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.enabled = false;
        }

        foreach (var bone in Top)
        {
            bone.transform.parent = _skull.transform;
            bone.transform.position = _skull.transform.position;
            bone.transform.rotation = _skull.transform.rotation * Quaternion.Euler(-90f, 0f, 0f);
        }
        foreach (var bone in Bottom)
        {
            bone.transform.parent = _skull.transform;
            bone.transform.position = _skull.transform.position;
            bone.transform.rotation = _skull.transform.rotation * Quaternion.Euler(-90f, 0f, 0f);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject == Mouse)
            StopAnimation();
        
    }
}
