using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Explosion : MonoBehaviour
{
    public SteamVR_TrackedController Controller;
    public GameObject HMD;

    private GameObject[] _children;
    private Vector3[] _center;
    private TextMesh[] _label;

    private bool _exploded;
    private float _animationEndTime;

    private const float AnimationTime = 6f;

	// Use this for initialization
	void Start ()
	{
	    _children = new GameObject[transform.childCount];
	    _center = new Vector3[transform.childCount];
        _label = new TextMesh[transform.childCount];

	    for (int i = 0; i < transform.childCount - 3 /* - 3 because of top and bottom and muose target */; i++)
	    {
	        _children[i] = transform.GetChild(i).gameObject;
	        _label[i] = _children[i].GetComponentInChildren<TextMesh>();
            CalculateCenter(i);
	    }

	    Controller.PadClicked += (sender, args) =>
	    {
	        if (args.padX > Math.Abs(args.padY) && args.padX > 0f)
                ToggleExplosion();
	    };
	}

    private void CalculateCenter(int index)
    {
        var min = new Vector3();
        var max = new Vector3();

        foreach (var meshRenderer in _children[index].GetComponentsInChildren<MeshRenderer>())
        {
            min = Vector3.Min(min, meshRenderer.bounds.min);
            max = Vector3.Max(max, meshRenderer.bounds.max);
            break;
        }

        _center[index] = (((min + max) / 2) - (transform.position * 0.5f));
        _center[index].Scale(new Vector3(1f, 4f, 1f));
    }
	
	// Update is called once per frame
	void Update ()
	{
	    for (var i = 0; i < transform.childCount - 3; i++)
	    {
	        Explode(i);
	        //_label[i].transform.rotation = Quaternion.LookRotation(_children[i].transform.position - HMD.transform.position);

	        _label[i].gameObject.transform.rotation =
	            Quaternion.LookRotation(_label[i].gameObject.transform.position - HMD.transform.position);

	        var tm = _label[i].GetComponent<TextMesh>();
	        var c = tm.color;

            c.a = _exploded ? Mathf.Min(1f, 1f - (_animationEndTime + AnimationTime - Time.time) / AnimationTime) : 0f;

	        tm.color = c;
        }
    }

    void Explode(int index)
    {
        //var factor = _exploded ? 1f : -1f;

        if (_exploded)
        {
            if (_animationEndTime - Time.time > 0)
                _children[index].transform.position = transform.rotation *
                                                      (Quaternion.Inverse(transform.rotation) *
                                                       _children[index].transform.position +
                                                       (5f * _center[index] * Time.deltaTime *
                                                        transform.localScale.x));
        }
        else
        {
            if (_animationEndTime - Time.time > 0)
            {
                _children[index].transform.position = transform.position;
                _children[index].transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(-90f, 0f, 0f));
            }
        }

    }

    void ToggleExplosion()
    {
        if (_animationEndTime - Time.time > 0)
            return;
        _exploded = !_exploded;
        _animationEndTime = Time.time + 2;
    }
}
