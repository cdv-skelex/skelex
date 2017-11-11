using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public Material IdleMaterial;
    public Material CollisionMaterial;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        foreach (var meshRenderer in rendererForCollision(col))
        {
            meshRenderer.material = CollisionMaterial;
        }
    }

    void OnCollisionExit(Collision col)
    {
        foreach (var meshRenderer in rendererForCollision(col))
        {
            meshRenderer.material = IdleMaterial;
        }
    }

    MeshRenderer[] rendererForCollision(Collision col)
    {
        return col.collider.gameObject.transform.parent.gameObject.GetComponentsInChildren<MeshRenderer>();
    }
}
