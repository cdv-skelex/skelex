using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public SteamVR_TrackedController Controller;

    private GameObject[] _children;
    private Vector3[] _center;

    private bool _exploded;
    private float _animationEndTime;

	// Use this for initialization
	void Start ()
	{
	    _children = new GameObject[transform.childCount];
	    _center = new Vector3[transform.childCount];

	    for (int i = 0; i < transform.childCount; i++)
	    {
	        _children[i] = transform.GetChild(i).gameObject;
            CalculateCenter(i);
	    }

	    Controller.PadClicked += (sender, args) => ToggleExplosion();
	}

    private void CalculateCenter(int index)
    {
        var min = new Vector3();
        var max = new Vector3();

        foreach (var meshRenderer in _children[index].GetComponentsInChildren<MeshRenderer>())
        {
            min = Vector3.Min(min, meshRenderer.bounds.min);
            max = Vector3.Max(max, meshRenderer.bounds.max);
        }

        _center[index] = ((min + max) / 2) - (transform.position * 0.5f);
    }
	
	// Update is called once per frame
	void Update ()
	{
	    for (int i = 0; i < transform.childCount; i++)
	    {
	        Explode(i);
	    }
    }

    void Explode(int index)
    {
        var factor = _exploded ? 1f : -1f;

        if (_animationEndTime - Time.time > 0)
            _children[index].transform.position = _children[index].transform.position + factor * 1.5f * _center[index] * Time.deltaTime;
    }

    void ToggleExplosion()
    {
        if (_animationEndTime - Time.time > 0)
            return;
        _exploded = !_exploded;
        _animationEndTime = Time.time + 2;
    }
}
