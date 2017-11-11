using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class DragBasedOnCollision : MonoBehaviour
{
    public GameObject Model;
    public Material IdleMaterial;
    public Material CollisionMaterial;

    private SteamVR_TrackedController _controller;

    private List<GameObject> _collidees = new List<GameObject>();

    private bool _dragging;

    // Use this for initialization
    void Start ()
    {
        _controller = GetComponent<SteamVR_TrackedController>();

        _controller.TriggerClicked += (sender, args) =>
        {
            _dragging = true;
            foreach (var collidee in _collidees)
            {
                collidee.transform.parent = gameObject.transform;
            }
        };

        _controller.TriggerUnclicked += (sender, args) =>
        {
            _dragging = false;
            foreach (var collidee in _collidees)
            {
                collidee.transform.parent = Model.transform;
                foreach (var meshRenderer in collidee.GetComponentsInChildren<MeshRenderer>())
                {
                    meshRenderer.material = IdleMaterial;
                }
            }
 
            _collidees = new List<GameObject>();
        };
    }
	
	// Update is called once per frame
	void Update () {
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject.transform.parent.parent.gameObject != Model)
            return;


        if (_dragging)
            return;

        _collidees.Add(col.collider.gameObject.transform.parent.gameObject);
        foreach (var meshRenderer in rendererForCollision(col))
        {
            meshRenderer.material = CollisionMaterial;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.collider.gameObject.transform.parent.parent.gameObject != Model)
            return;

        if (_dragging)
            return;

        _collidees.Remove(col.collider.gameObject.transform.parent.gameObject);
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
