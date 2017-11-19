using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteAnimation : MonoBehaviour
{
    public GameObject[] Top;
    public GameObject[] Bottom;

    public GameObject TopGroup;
    public GameObject BottomGroup;

	// Use this for initialization
	void Start () {
	    foreach (var bone in Top)
	    {
	        bone.transform.parent = TopGroup.transform;
	    }
	    foreach (var bone in Bottom)
	    {
	        bone.transform.parent = BottomGroup.transform;
	    }
    }
	
	// Update is called once per frame
	void Update ()
	{
	    /*var t = Mathf.Min(Time.time, 0.35f);
        foreach (var bone in Top)
        {
            bone.transform.position = transform.position + new Vector3(0f, t, 0f) * transform.localScale.x;
            bone.transform.rotation = Quaternion.Euler(t, 0f, 0f);
        }*/
	}
}
