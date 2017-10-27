using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoObjectPivot : MonoBehaviour
{
    public GameObject FirstController;
    public GameObject SecondController;

	void Update ()
	{
	    transform.position = (FirstController.transform.position + SecondController.transform.position) / 2;
	}
}
