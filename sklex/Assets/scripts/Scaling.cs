using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaling : MonoBehaviour
{
    public GameObject CombinedPivot;
    public GameObject LeftPivot;
    public GameObject RightPivot;


    public GameObject FirstControllerGameObject;
    public GameObject SecondControllerGameObject;

    private SteamVR_TrackedController _firstController;
    private SteamVR_TrackedController _secondController;

    private bool _firstGripped = false;
    private bool _secondGripped = false;

    private void OnEnable()
    {
        _firstController = FirstControllerGameObject.GetComponent<SteamVR_TrackedController>();
        _secondController = SecondControllerGameObject.GetComponent<SteamVR_TrackedController>();

        _firstController.Gripped += (sender, args) => { _firstGripped = true; HandleGripping(); };
        _secondController.Gripped += (sender, args) => { _secondGripped = true; HandleGripping(); };

        _firstController.Ungripped += (sender, args) => { _firstGripped = false; HandleGripping(); };
        _secondController.Ungripped += (sender, args) => { _secondGripped = false; HandleGripping(); };

        //_controller.TriggerClicked += HandleTriggerClicked;
        //_controller.TriggerUnclicked += HandleTriggerUnclicked;
    }

    private void HandleGripping()
    {
        transform.parent = null;
        if (_firstGripped)
            transform.parent = LeftPivot.transform;
        if (_secondGripped)
            transform.parent = RightPivot.transform;
        if (_firstGripped && _secondGripped)
            transform.parent = CombinedPivot.transform;
    }

    void Update () {
		
	}
}
