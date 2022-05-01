using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using DG.Tweening;
using UnityEngine;


public class ObstacleMoveController : MonoBehaviour
{

    [SerializeField]
    private MovementType direction;
    [SerializeField]
    private float movementDuration = 20;
    [SerializeField]
    private float movementLenght = 24;
    [SerializeField]
    private Ease easeType;
    [SerializeField]
    private bool positiveMove = true;
    [SerializeField]
    private bool isRelative = false;
    private bool _movementActive = false;
    
    // Update is called once per frame
    void Update()
    {
        if (!_movementActive)
        {
            if (isRelative)
            {
                RelativeMove();
                return;
            }
            
            GlobalMove();
        }
    }

    private void GlobalMove()
    {
        switch (direction)
        {
            case MovementType.X :
                _movementActive = true;
                transform.DOMoveX((positiveMove == true ? 1 : -1) * movementLenght,movementDuration).SetEase(easeType).OnComplete(() =>
                {
                    positiveMove = !positiveMove;
                    _movementActive = false;
                });
                break;
            case MovementType.Y :
                _movementActive = true;
                transform.DOMoveY((positiveMove == true ? 1 : -1) * movementLenght,movementDuration).SetEase(easeType).OnComplete(() =>
                {
                    positiveMove = !positiveMove;
                    _movementActive = false;
                });
                break;
            case MovementType.Z :
                _movementActive = true;
                transform.DOMoveZ((positiveMove == true ? 1 : -1) * movementLenght,movementDuration).SetEase(easeType).OnComplete(() =>
                {
                    positiveMove = !positiveMove;
                    _movementActive = false;
                });
                break;
        }
    }

    private void RelativeMove()
    {
        switch (direction)
        {
            case MovementType.X :
                _movementActive = true;
                transform.DOMoveX((positiveMove == true ? 1 : -1) * movementLenght + transform.position.x,movementDuration).SetEase(easeType).OnComplete(() =>
                {
                    positiveMove = !positiveMove;
                    _movementActive = false;
                });
                break;
            case MovementType.Y :
                _movementActive = true;
                transform.DOMoveY((positiveMove == true ? 1 : -1) * movementLenght + transform.position.y,movementDuration).SetEase(easeType).OnComplete(() =>
                {
                    positiveMove = !positiveMove;
                    _movementActive = false;
                });
                break;
            case MovementType.Z :
                _movementActive = true;
                transform.DOMoveZ((positiveMove == true ? 1 : -1) * movementLenght + transform.position.z,movementDuration).SetEase(easeType).OnComplete(() =>
                {
                    positiveMove = !positiveMove;
                    _movementActive = false;
                });
                break;
        }
    }
}

public enum MovementType
{
    X,
    Y,
    Z
}
