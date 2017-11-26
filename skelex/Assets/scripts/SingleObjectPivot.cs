using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleObjectPivot : MonoBehaviour
{
    public GameObject Controller;

	void Update ()
	{
	    transform.position = Controller.transform.position;
	}
}
