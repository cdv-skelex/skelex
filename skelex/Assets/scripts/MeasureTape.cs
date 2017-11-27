using System;
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

    private MeshRenderer _meshRenderer;
    private Material _material;

    public GameObject HMD;


	// Use this for initialization
	void Start ()
	{
	    _textMesh = Text.GetComponent<TextMesh>();
	    Text.transform.parent = ControllerLeft.transform;
	    Text.transform.position = ControllerLeft.transform.position + new Vector3(0.05f, 0f, 0.05f);
	    Text.transform.rotation = ControllerLeft.transform.rotation * Quaternion.Euler(90, 0, 0);

        _meshRenderer = GetComponent<MeshRenderer>();
	    _material = _meshRenderer.material;

	    _trackedLeftController = ControllerLeft.GetComponent<SteamVR_TrackedController>();

	    _meshRenderer.enabled = false;
	    _textMesh.text = "";
	    _trackedLeftController.PadClicked += (sender, args) =>
	    {
            if (-args.padX > Math.Abs(args.padY) && args.padX < 0f)
	            _meshRenderer.enabled = !_meshRenderer.enabled;
	    };
	    //_trackedLeftController.PadUnclicked += (sender, args) => { _meshRenderer.enabled = false; };
	}

    // Update is called once per frame
    void Update ()
	{
	    var left = ControllerLeft.transform.position;
	    var right = ControllerRight.transform.position;
        TapeContainer.transform.position = (left + right) / 2;
	    var hmdRot = Quaternion.LookRotation(TapeContainer.transform.position - HMD.transform.position);
        var rotation = Quaternion.LookRotation(right - left);
	    gameObject.transform.rotation = Quaternion.Euler(/*-hmdRot.eulerAngles.x*/0, 0, 0) * rotation;
	    var scale = Vector3.Distance(left, right);
	    gameObject.transform.localScale = new Vector3(0.0001f, 0.02f, scale);

	    var s = (10f * scale / Skeleton.transform.localScale.x).ToString();
        _textMesh.text = _meshRenderer.enabled ? s.Substring(0, Math.Min(4, s.Length)) + "cm" : "";

        _material.mainTextureScale = new Vector2(scale * 10, 1);
	}
}
