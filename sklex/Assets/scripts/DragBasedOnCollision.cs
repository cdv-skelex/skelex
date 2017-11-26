using System.Collections;
using System.Collections.Generic;
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

        _controller.TriggerClicked += (sender, args) => StartDragging();
        _controller.PadClicked += (sender, args) => StartDragging();

        _controller.TriggerUnclicked += (sender, args) => StopDragging();
        _controller.PadUnclicked += (sender, args) => StopDragging();

        /*_controller.PadClicked += (sender, args) =>
        {
            _activeCollider.transform.position = Model.transform.position;
            _activeCollider.transform.rotation = Model.transform.rotation * Quaternion.Euler(new Vector3(-90, 0, 0));
        };*/
    }

    void StartDragging()
    {
        _dragging = true;
        if (_activeCollider != null)
            _activeCollider.transform.parent = gameObject.transform;
    }

    void StopDragging()
    {
        _dragging = false;
        if (_activeCollider != null)
            _activeCollider.transform.parent = Model.transform;
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
            setMaterial(_activeCollider, false);

        _activeCollider = parentForCollision(col);

        setMaterial(_activeCollider, true);
    }

    void OnCollisionExit(Collision col)
    {
        if (_dragging)
            return;
        if (_activeCollider == parentForCollision(col))
        {
            setMaterial(parentForCollision(col), false);
            _activeCollider = null;
        }
    }

    GameObject parentForCollision(Collision col)
    {
        return col.collider.gameObject.transform.parent.gameObject;
    }

    void setMaterial(GameObject obj, bool collision)
    {
        var material = collision ? CollisionMaterial : IdleMaterial;

        foreach (var meshRenderer in obj.transform.GetChild(0).gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderer.material = material;
        }
        var tm = obj.transform.GetChild(2).GetComponent<TextMesh>();
        var c = tm.color;

        var collisionColor = new Color(255f/255f, 0f, 122/255f);
        var idleColor = Color.white;

        var textColor = collision ? collisionColor : idleColor;
        textColor.a = c.a;

        tm.color = textColor;
    }
}
