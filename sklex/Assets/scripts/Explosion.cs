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

	// Use this for initialization
	void Start ()
	{
	    _children = new GameObject[transform.childCount];
	    _center = new Vector3[transform.childCount];
        _label = new TextMesh[transform.childCount];

	    for (int i = 0; i < transform.childCount; i++)
	    {
	        _children[i] = transform.GetChild(i).gameObject;
	        _label[i] = _children[i].GetComponentInChildren<TextMesh>();
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
            break;
        }

        _center[index] = ((min + max) / 2) - (transform.position * 0.5f);
    }
	
	// Update is called once per frame
	void Update ()
	{
	    for (int i = 0; i < transform.childCount; i++)
	    {
	        Explode(i);
	        //_label[i].transform.rotation = Quaternion.LookRotation(_children[i].transform.position - HMD.transform.position);
	        _label[i].gameObject.transform.rotation =
	            Quaternion.LookRotation(_label[i].gameObject.transform.position - HMD.transform.position);


	        if (!_exploded)
	        {
	            //_label[i].GetComponent<MeshRenderer>().enabled = false;
	            var tm = _label[i].GetComponent<TextMesh>();
	            tm.color = new Color(1f, 1f, 1f, Mathf.Min(1f, (_animationEndTime - Time.time) / 2f));
	        }
	        else
	        {
	            //var mr = _label[i].GetComponent<MeshRenderer>();
	            //mr.enabled = true;
	            var tm = _label[i].GetComponent<TextMesh>();
	            tm.color = new Color(1f, 1f, 1f, Mathf.Min(1f, 1f - (_animationEndTime + 2f - Time.time) / 2f));
	        }
	    }
    }

    void Explode(int index)
    {
        var factor = _exploded ? 1f : -1f;

        if (_animationEndTime - Time.time > 0)
            _children[index].transform.position = transform.rotation * (Quaternion.Inverse(transform.rotation) * _children[index].transform.position + (factor * 5f * _center[index] * Time.deltaTime * transform.localScale.x));
    }

    void ToggleExplosion()
    {
        if (_animationEndTime - Time.time > 0)
            return;
        _exploded = !_exploded;
        _animationEndTime = Time.time + 2;
    }
}
