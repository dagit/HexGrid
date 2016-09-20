using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class HexMapEditor : MonoBehaviour {

    public Color[] colors;
    public Texture2D[] textures;
    public HexGrid hexGrid;
    private Color activeColor;
    private int activeTexture = 0;

    void Awake()
    {
        Select(0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) &&
            !EventSystem.current.IsPointerOverGameObject())
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            hexGrid.ColorAndTextureCell(hit.point, activeTexture, activeColor);
        }
    }

    public void Select(int index)
    {
        activeColor = colors[index];
        switch (index)
        {
            case 0: activeTexture = -1; break;
            case 1: activeTexture = 1; break;
            case 2: activeTexture = 2; break;
            case 3: activeTexture = 0; break;
            default: activeTexture = -1; break;
        }
    }

}
