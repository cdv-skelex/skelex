// blatantly stolen from https://steamcommunity.com/app/358720/discussions/0/357287304420388604/

using UnityEngine;
using System.Runtime.InteropServices;

/// <summary>
/// Override the texture of the Vive controllers, with your own texture, after SteamVR has loaded and applied the original texture.
/// </summary>
public class OverrideControllerTexture : MonoBehaviour
{
    #region Public variables
    [Header("Variables")]
    public Texture2D newBodyTexture; //The new texture.
    #endregion
    void OnEnable()
    {
        //Subscribe to the event that is called by SteamVR_RenderModel, when the controller mesh + texture, has been loaded completely.
        SteamVR_Events.RenderModelLoaded.Listen(OnControllerLoaded);
    }
    void OnDisable()
    {
        //Unsubscribe to the event if this object is disabled.
        SteamVR_Events.RenderModelLoaded.Remove(OnControllerLoaded);
    }

    /// <summary>
    /// Override the texture of each of the parts, with your texture.
    /// </summary>
    /// <param name="newTexture">Override texture</param>
    /// <param name="modelTransform">Transform of the gameobject, which has the SteamVR_RenderModel component.</param>
    public void UpdateControllerTexture(Texture2D newTexture, Transform modelTransform)
    {
        modelTransform.Find("body").GetComponent<MeshRenderer>().material.mainTexture = newTexture;
        modelTransform.Find("button").GetComponent<MeshRenderer>().material.mainTexture = newTexture;
        modelTransform.Find("led").GetComponent<MeshRenderer>().material.mainTexture = newTexture;
        modelTransform.Find("lgrip").GetComponent<MeshRenderer>().material.mainTexture = newTexture;
        modelTransform.Find("rgrip").GetComponent<MeshRenderer>().material.mainTexture = newTexture;
        modelTransform.Find("scroll_wheel").GetComponent<MeshRenderer>().material.mainTexture = newTexture;
        modelTransform.Find("sys_button").GetComponent<MeshRenderer>().material.mainTexture = newTexture;
        modelTransform.Find("trackpad").GetComponent<MeshRenderer>().material.mainTexture = newTexture;
        modelTransform.Find("trackpad_scroll_cut").GetComponent<MeshRenderer>().material.mainTexture = newTexture;
        //modelTransform.Find("trackpad_touch").GetComponent<MeshRenderer>().material.mainTexture = newTexture;
        var touchpad = modelTransform.Find("trackpad_touch").GetComponent<MeshRenderer>();
        var oldMat = touchpad.material;
        touchpad.material = new Material(Shader.Find("Specular"));
        //touchpad.material.CopyPropertiesFromMaterial(oldMat);
        modelTransform.Find("trackpad_touch").GetComponent<MeshRenderer>().material.mainTexture = newTexture;

        modelTransform.Find("trigger").GetComponent<MeshRenderer>().material.mainTexture = newTexture;
    }

    /// <summary>
    /// Call this method when the RenderModelLoaded event is triggered.
    /// </summary>
    /// <param name="args">SteamVR_RenderModel renderModel, bool success</param>
    void OnControllerLoaded(SteamVR_RenderModel renderModel, bool success)
    {
        if (!success) return;
        if (gameObject == renderModel.gameObject)
            UpdateControllerTexture(newBodyTexture, renderModel.gameObject.transform);
    }

}