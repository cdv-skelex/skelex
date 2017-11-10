using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureTape : MonoBehaviour
{
    public GameObject ControllerLeft;
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

	    _textMesh.text = scale.ToString();

        _material.mainTextureScale = new Vector2(scale * 10, 1);
	}
}
