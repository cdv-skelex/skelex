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
    private List<GameObject[]> _matchingBones; // will always be GameObject[2]

    private bool _active;

	// Use this for initialization
	void Start ()
	{
	    Controller.MenuButtonClicked += (sender, args) => { Toggle(); };
	    ReferencePlane.GetComponent<MeshRenderer>().enabled = _active;

	    _camera = StudioCamera.GetComponent<Camera>();

	    for (var i = 0; i < ReferenceSkull.transform.childCount; i++)
        {
            var reference = ReferenceSkull.transform.GetChild(i).gameObject;
            var studio = StudioSkull.transform.GetChild(i).gameObject;
            var pair = new GameObject[2]{ reference, studio };
            _matchingBones.Add(pair);
            //studio.transform.rotation = reference.transform.rotation;
            //studio.transform.position = StudioSkull.transform.position + ((reference.transform.position - ReferenceSkull.transform.position) * (StudioSkull.transform.localScale.x / ReferenceSkull.transform.localScale.x));
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
    }

    void Toggle()
    {
        _active = !_active;
        ReferencePlane.GetComponent<MeshRenderer>().enabled = _active;

        if (!_active)
        {
            _camera.nearClipPlane = 1000000f;
            _camera.farClipPlane = 1000001f;
        }
    }
}
