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

    private GameObject _activeCollider;

    private bool _dragging;

    // Use this for initialization
    void Start ()
    {
        _controller = GetComponent<SteamVR_TrackedController>();

        _controller.TriggerClicked += (sender, args) =>
        {
            _dragging = true;
            _activeCollider.transform.parent = gameObject.transform;
        };

        _controller.TriggerUnclicked += (sender, args) =>
        {
            _dragging = false;
            _activeCollider.transform.parent = Model.transform;
        };

        _controller.PadClicked += (sender, args) =>
        {
            _activeCollider.transform.position = Model.transform.position;
            _activeCollider.transform.rotation = Model.transform.rotation * Quaternion.Euler(new Vector3(-90, 0, 0));
        };
    }
	
	// Update is called once per frame
	void Update () {
	}

    void OnCollisionEnter(Collision col)
    {
        if (_dragging)
            return;
        if (col.collider.gameObject.transform.parent.parent.gameObject != Model)
            return;

        if (_activeCollider != null)
        {
            setMaterial(_activeCollider, IdleMaterial);
        }

        _activeCollider = parentForCollision(col);

        setMaterial(_activeCollider, CollisionMaterial);
    }

    void OnCollisionExit(Collision col)
    {
        if (_dragging)
            return;
        if (_activeCollider == parentForCollision(col))
        {
            setMaterial(parentForCollision(col), IdleMaterial);
            _activeCollider = null;
        }
    }

    GameObject parentForCollision(Collision col)
    {
        return col.collider.gameObject.transform.parent.gameObject;
    }

    void setMaterial(GameObject obj, Material material)
    {
        foreach (var meshRenderer in obj.transform.GetChild(0).gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderer.material = material;
        }
    }
}
