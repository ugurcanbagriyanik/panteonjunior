using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelController : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer mesh; 
    void Start()
    {
        mesh.material = GameManager.instance.defaultMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        if (GameManager.instance.gameState==GameStates.Painting)
        {
            mesh.material = GameManager.instance.paintedMaterial;
            GameManager.instance.PaintCounter();
        }
        
    }

}
