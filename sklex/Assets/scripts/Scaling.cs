using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaling : MonoBehaviour
{
    public GameObject CombinedPivot;
    public GameObject FirstPivot;
    public GameObject SecondPivot;

    public GameObject FirstControllerGameObject;
    public GameObject SecondControllerGameObject;

    private SteamVR_TrackedController _firstController;
    private SteamVR_TrackedController _secondController;

    private bool _firstGripped = false;
    private bool _secondGripped = false;

    private bool _scalingActive = false;
    private float _startingDistance;
    private Vector3 _startingScale;


    private void OnEnable()
    {
        _firstController = FirstControllerGameObject.GetComponent<SteamVR_TrackedController>();
        _secondController = SecondControllerGameObject.GetComponent<SteamVR_TrackedController>();

        _firstController.Gripped += (sender, args) => { _firstGripped = true; HandleGripping(); };
        _secondController.Gripped += (sender, args) => { _secondGripped = true; HandleGripping(); };

        _firstController.Ungripped += (sender, args) => { _firstGripped = false; HandleGripping(); };
        _secondController.Ungripped += (sender, args) => { _secondGripped = false; HandleGripping(); };
    }

    private void HandleGripping()
    {
        transform.parent = null;
        if (_firstGripped)
            transform.parent = FirstPivot.transform;
        if (_secondGripped)
            transform.parent = SecondPivot.transform;
        if (_firstGripped && _secondGripped)
        {
            transform.parent = CombinedPivot.transform;

            if (!_scalingActive)
            {
                _scalingActive = true;
                _startingDistance = ControllerDistance();
                _startingScale = transform.localScale;
            }
        }
        else
            _scalingActive = false;
    }

    private float ControllerDistance()
    {
        return Vector3.Distance(FirstControllerGameObject.transform.position,
            SecondControllerGameObject.transform.position);
    }

    void Update () {
        if (_scalingActive)
        {
            transform.localScale = _startingScale * (ControllerDistance() / _startingDistance);
        }
	}
}
