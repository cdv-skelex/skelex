using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteAnimation : MonoBehaviour
{
    public GameObject[] Top;
    public GameObject[] Bottom;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//if (Time.time < 5)
        {
            foreach (var bone in Top)
            {
                bone.transform.position = transform.position + new Vector3(0f, Time.deltaTime, 0f);
                bone.transform.rotation *= Quaternion.Euler(1f * Time.deltaTime, 0f, 0f);
            }
        }
	}
}
