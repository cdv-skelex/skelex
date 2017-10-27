using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public Material Material;

	// Use this for initialization
	void Start () {
	    //Material.shader = Shader.Find("Specular");
	    Material.SetColor("_SpecColor", Color.white);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        Material.SetColor("_SpecColor", Color.green);
    }

    void OnCollisionExit(Collision col)
    {
        Material.SetColor("_SpecColor", Color.white);
    }
}
