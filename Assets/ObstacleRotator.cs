using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ObstacleRotator : MonoBehaviour
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
                transform.Rotate(new Vector3(0,-1,0)*rotationSpeed*Time.deltaTime);
                break;
            case RotationType.ToLeft:
                transform.Rotate(new Vector3(0,1,0)*rotationSpeed*Time.deltaTime);
                break;
        }    
    }
}

