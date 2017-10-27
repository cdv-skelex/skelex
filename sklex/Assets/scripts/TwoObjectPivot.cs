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
	    var controllerRotation = Quaternion.LookRotation(FirstController.transform.position - SecondController.transform.position);
	    transform.rotation = Quaternion.Euler(0, controllerRotation.eulerAngles.y, 0);
	}
}
