using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameStates gameState = GameStates.MainMenu;
    public Material paintedMaterial;
    public Material defaultMaterial;
    [SerializeField]
    private GameObject pixel;
    [SerializeField]
    private GameObject wall;
    [SerializeField]
    private float pixelScaleConstant=0.01f;
    [SerializeField]
    private Text percentage;

    [SerializeField] 
    public Transform finishLine;
    private int _paintedPixels=0;
    
    

    private List<GameObject> pixels = new List<GameObject>();
    
    void Awake()
    {
        if (instance==null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }else if (instance!=null)
        {
            Destroy(this);
        }
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameState = GameStates.Paurkour;
        }
    }

    public void PaintCounter()
    {
        _paintedPixels++;
        percentage.text="% "+GetPaintPercenteges()*100;
    }

    float GetPaintPercenteges()
    {
        return _paintedPixels!=0 ? _paintedPixels / (10000 * pixelScaleConstant * pixelScaleConstant):0;
    }
    
    public void BuildWall()
    {
        for (int i = 0; i < 100*pixelScaleConstant; i++)
        {
            for (int j = 0; j < 100*pixelScaleConstant; j++)
            {
                var tempObject = Instantiate(pixel, wall.transform);
                tempObject.transform.position = wall.transform.position + new Vector3(i*pixelScaleConstant, j*pixelScaleConstant, 0);
                tempObject.transform.localScale = new Vector3(pixelScaleConstant, pixelScaleConstant, 0.01f);
                pixels.Add(tempObject);
            }
        }
    }
    
    public void ClearWall()
    {
        var childs  = wall.transform.childCount;
        for (var i = childs - 1; i >= 0; i--)
        {
            DestroyImmediate(wall.transform.GetChild(i).gameObject);
        }
        pixels.Clear();
    }
}

public enum GameStates
{
    MainMenu,
    Paurkour,
    Painting,
    
}