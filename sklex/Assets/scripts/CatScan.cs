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
    public Camera StudioCamera;

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
	    if (!_active)
	        return;

	    StudioSkull.transform.rotation = ReferenceSkull.transform.rotation * Quaternion.Inverse(ReferencePlane.transform.rotation * Quaternion.Euler(90f, 0f, 0f));

	    var dist = Vector3.Distance(ReferencePlane.transform.position, ReferenceSkull.transform.position) /
	               ReferenceSkull.transform.localScale.x;

        StudioCamera.nearClipPlane = dist - 0.05f;
	    StudioCamera.farClipPlane = dist + 0.05f;
    }

    void Toggle()
    {
        _active = !_active;
        ReferencePlane.GetComponent<MeshRenderer>().enabled = _active;

        if (!_active)
        {
            StudioCamera.nearClipPlane = 1000000f;
            StudioCamera.farClipPlane = 1000001f;
        }
    }
}
