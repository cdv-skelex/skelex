﻿using UnityEngine;
using System.Collections;
using System.Linq;

[ExecuteInEditMode]
public class ClippableObject : MonoBehaviour
{
    public Material IdleMaterial;
    public Material CollisionMaterial;

    public void OnEnable() {
        //let's just create a new material instance.
        /*IdleMaterial = new Material(Shader.Find("Custom/StandardClippable")) {
            hideFlags = HideFlags.HideAndDontSave
        };*/
        //IdleMaterial.color = Color.blue;

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var model = child.transform.GetChild(0);
            foreach (var mr in model.GetComponentsInChildren<MeshRenderer>())
            {
                //mr.sharedMaterial = IdleMaterial;
            }
        }
    }

    public void Start() { }

    //only 3 clip planes for now, will need to modify the shader for more.
    [Range(0, 3)]
    public int clipPlanes = 0;

    //preview size for the planes. Shown when the object is selected.
    public float planePreviewSize = 5.0f;

    //Positions and rotations for the planes. The rotations will be converted into normals to be used by the shaders.
    public Vector3 plane1Position = Vector3.zero;
    public Vector3 plane1Rotation = new Vector3(0, 0, 0);

    public Vector3 plane2Position = Vector3.zero;
    public Vector3 plane2Rotation = new Vector3(0, 90, 90);

    public Vector3 plane3Position = Vector3.zero;
    public Vector3 plane3Rotation = new Vector3(0, 0, 90);

    //Only used for previewing a plane. Draws diagonals and edges of a limited flat plane.
    private void DrawPlane(Vector3 position, Vector3 euler) {
        var forward = Quaternion.Euler(euler) * Vector3.forward;
        var left = Quaternion.Euler(euler) * Vector3.left;

        var forwardLeft = position + forward * planePreviewSize * 0.5f + left * planePreviewSize * 0.5f;
        var forwardRight = forwardLeft - left * planePreviewSize;
        var backRight = forwardRight - forward * planePreviewSize;
        var backLeft = forwardLeft - forward * planePreviewSize;

        Gizmos.DrawLine(position, forwardLeft);
        Gizmos.DrawLine(position, forwardRight);
        Gizmos.DrawLine(position, backRight);
        Gizmos.DrawLine(position, backLeft);

        Gizmos.DrawLine(forwardLeft, forwardRight);
        Gizmos.DrawLine(forwardRight, backRight);
        Gizmos.DrawLine(backRight, backLeft);
        Gizmos.DrawLine(backLeft, forwardLeft);
    }

    private void OnDrawGizmosSelected() {
        if (clipPlanes >= 1) {
            DrawPlane(plane1Position, plane1Rotation);
        }
        if (clipPlanes >= 2) {
            DrawPlane(plane2Position, plane2Rotation);
        }
        if (clipPlanes >= 3) {
            DrawPlane(plane3Position, plane3Rotation);
        }
    }

    //Ideally the planes do not need to be updated every frame, but we'll just keep the logic here for simplicity purposes.
    public void Update()
    {
        UpdateMaterial(IdleMaterial);
        UpdateMaterial(CollisionMaterial);
    }

    void UpdateMaterial(Material m)
    {
        //Only should enable one keyword. If you want to enable any one of them, you actually need to disable the others. 
        //This may be a bug...
        switch (clipPlanes)
        {
            case 0:
                m.DisableKeyword("CLIP_ONE");
                m.DisableKeyword("CLIP_TWO");
                m.DisableKeyword("CLIP_THREE");
                break;
            case 1:
                m.EnableKeyword("CLIP_ONE");
                m.DisableKeyword("CLIP_TWO");
                m.DisableKeyword("CLIP_THREE");
                break;
            case 2:
                m.DisableKeyword("CLIP_ONE");
                m.EnableKeyword("CLIP_TWO");
                m.DisableKeyword("CLIP_THREE");
                break;
            case 3:
                m.DisableKeyword("CLIP_ONE");
                m.DisableKeyword("CLIP_TWO");
                m.EnableKeyword("CLIP_THREE");
                break;
        }

        //pass the planes to the shader if necessary.
        if (clipPlanes >= 1)
        {
            m.SetVector("_planePos", plane1Position);
            //plane normal vector is the rotated 'up' vector.
            m.SetVector("_planeNorm", Quaternion.Euler(plane1Rotation) * Vector3.up);
        }

        if (clipPlanes >= 2)
        {
            m.SetVector("_planePos2", plane2Position);
            m.SetVector("_planeNorm2", Quaternion.Euler(plane2Rotation) * Vector3.up);
        }

        if (clipPlanes >= 3)
        {
            m.SetVector("_planePos3", plane3Position);
            m.SetVector("_planeNorm3", Quaternion.Euler(plane3Rotation) * Vector3.up);
        }
    }
}