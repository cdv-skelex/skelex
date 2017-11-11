using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Vector3 _center;
    private GameObject _model;

	// Use this for initialization
	void Start ()
	{
	    var min = new Vector3();
	    var max = new Vector3();

	    foreach (var meshRenderer in GetComponentsInChildren<MeshRenderer>())
	    {
	        min = Vector3.Min(min, meshRenderer.bounds.min);
	        max = Vector3.Max(max, meshRenderer.bounds.max);
	    }

	    _model = gameObject.transform.parent.gameObject;

	    _center = ((min + max) / 2) - (_model.transform.position * 0.5f);
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (Time.time < 2)
	        transform.position = transform.position + _center * Time.deltaTime;

        if (Time.time > 5 && Time.time < 7)
            transform.position = transform.position - _center * Time.deltaTime;
    }
}
