using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlatformRotator : MonoBehaviour
{

    [SerializeField]
    public RotationType rotationType;

    [SerializeField]
    private float rotationSpeed = 20;
    
    // Update is called once per frame
    void Update()
    {
        switch (rotationType)
        {
            case RotationType.ToRight:
                transform.Rotate(new Vector3(0,0,-1)*rotationSpeed*Time.deltaTime);
                break;
            case RotationType.ToLeft:
                transform.Rotate(new Vector3(0,0,1)*rotationSpeed*Time.deltaTime);
                break;
            case RotationType.ToForward:
                transform.Rotate(new Vector3(1,0,0)*rotationSpeed*Time.deltaTime);
                break;
            case RotationType.ToBackward:
                transform.Rotate(new Vector3(-1,0,0)*rotationSpeed*Time.deltaTime);
                break;
        }    
    }
}

public enum RotationType
{
    ToRight,
    ToLeft,
    ToForward,
    ToBackward
}
