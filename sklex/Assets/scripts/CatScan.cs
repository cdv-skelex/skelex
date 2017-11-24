using System;
using System.Collections;
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
    private Camera _camera;
    private List<GameObject[]> _matchingBones = new List<GameObject[]>(); // will always be GameObject[2]

    private bool _active;

	// Use this for initialization
	void Start ()
	{
	    Controller.PadClicked += (sender, args) =>
	    {
	        if (args.padY > Math.Abs(args.padX) && args.padX > 0f)
                Toggle();
	    };
        ReferencePlane.GetComponent<MeshRenderer>().enabled = _active;
        /*Toggle();
        Toggle();*/

	    _camera = StudioCamera.GetComponent<Camera>();

	    for (var i = 0; i < ReferenceSkull.transform.childCount; i++)
        {
            var reference = ReferenceSkull.transform.GetChild(i).gameObject;
            var studio = StudioSkull.transform.GetChild(i).gameObject;

            var pair = new[]{reference, studio};
            _matchingBones.Add(pair);
        }
	}

    // Update is called once per frame
    void Update ()
	{
	    if (!_active)
	        return;

	    StudioSkull.transform.rotation = ReferenceSkull.transform.rotation;

        var offset = (ReferencePlane.transform.position - ReferenceSkull.transform.position) / ReferenceSkull.transform.localScale.x;

	    StudioCamera.transform.position = StudioSkull.transform.position + offset / StudioSkull.transform.localScale.x;
	    StudioCamera.transform.rotation = ReferencePlane.transform.rotation * Quaternion.Euler(90f, 0f, 0f);

        _camera.nearClipPlane = 0.1f;
	    _camera.farClipPlane = 0.3f;

	    foreach (var pair in _matchingBones)
	    {
	        var reference = pair[0].transform;
	        var studio = pair[1].transform;

	        studio.rotation = reference.rotation;
	        studio.position = StudioSkull.transform.position +
	                          ((reference.position - ReferenceSkull.transform.position) /
	                           (StudioSkull.transform.localScale.x * ReferenceSkull.transform.localScale.x));
	    }
    }

    void Toggle()
    {
        _active = !_active;
        ReferencePlane.GetComponent<MeshRenderer>().enabled = _active;

        var bgColor = _active ? Color.black : Color.clear;
        StudioCamera.GetComponent<Camera>().backgroundColor = bgColor;

        if (!_active)
        {
            _camera.nearClipPlane = 1000000f;
            _camera.farClipPlane = 1000001f;
        }
    }
}
