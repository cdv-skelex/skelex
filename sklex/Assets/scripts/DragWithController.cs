using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragWithController : MonoBehaviour
{
    private SteamVR_TrackedController _controller;
    public GameObject DraggableGameObject;

    private void OnEnable()
    {
        _controller = GetComponent<SteamVR_TrackedController>();
        _controller.TriggerClicked += HandleTriggerClicked;
        _controller.TriggerUnclicked += HandleTriggerUnclicked;
    }

    private void HandleTriggerClicked(object sender, ClickedEventArgs e)
    {
        DraggableGameObject.transform.parent = gameObject.transform;
    }

    private void HandleTriggerUnclicked(object sender, ClickedEventArgs e)
    {
        DraggableGameObject.transform.parent = null;
    }
}
