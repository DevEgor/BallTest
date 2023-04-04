using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform LookTarget;
    private Transform _cameraTransform;

    void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, transform.position, Time.deltaTime * 4f);
        _cameraTransform.LookAt(LookTarget);
    }
}
