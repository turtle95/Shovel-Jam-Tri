﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public enum Painter_BrushMode { PAINT, DECAL };
public class TexturePainterCopy : MonoBehaviour {
    public GameObject brushCursor, brushContainer; //The cursor that overlaps the model and our container for the brushes painted
    public Camera sceneCamera, canvasCam;  //The camera that looks at the model, and the camera that looks at the canvas.
    public Sprite cursorPaint, cursorDecal; // Cursor for the differen functions 
    public RenderTexture canvasTexture; // Render Texture that looks at our Base Texture and the painted brushes
    public Material baseMaterial; // The material of our base texture (Were we will save the painted texture)

   // Painter_BrushMode mode; //Our painter mode (Paint brushes or decals)
    public float brushSize = 0.001f; //The size of our brush
    public Color brushColor; //The selected color
    int brushCounter = 0, MAX_BRUSH_COUNT = 1000; //To avoid having millions of brushes
    bool saving = false; //Flag to check if we are saving the texture

    public GameObject smudge;
    public GameObject crack;

    void Update()
    {
        //brushColor = ColorSelector.GetColor();  //Updates our painted color with the selected color
        if (Input.GetMouseButton(0))
        {
            DoAction();
        }
        UpdateBrushCursor();
    }

    //The main action, instantiates a brush or decal entity at the clicked position on the UV map
    void DoAction()
    {
        if (saving)
            return;
        Vector3 uvWorldPosition = Vector3.zero;
        if (HitTestUVPosition(ref uvWorldPosition))
        {
            GameObject brushObj;
            

                brushObj = Instantiate(smudge); //Paint a brush
                brushObj.GetComponent<SpriteRenderer>().color = brushColor; //Set the brush color

            //brushColor.a = brushSize * 2.0f; // Brushes have alpha to have a merging effect when painted over.
            brushObj.transform.parent = brushContainer.transform; //Add the brush to our container to be wiped later
            brushObj.transform.localPosition = uvWorldPosition; //The position of the brush (in the UVMap)
            brushObj.transform.localScale = Vector3.one * brushSize;//The size of the brush
        }
        brushCounter++; //Add to the max brushes
        if (brushCounter >= MAX_BRUSH_COUNT)
        { //If we reach the max brushes available, flatten the texture and clear the brushes
            brushCursor.SetActive(false);
           

        }
    }

    void CrackScreen()
    {
        Vector3 uvWorldPosition = Vector3.zero;
        GameObject brushObj;
        brushObj = Instantiate(crack); //Paint a decal

        //brushColor.a = brushSize * 2.0f; // Brushes have alpha to have a merging effect when painted over.
        brushObj.transform.parent = brushContainer.transform; //Add the brush to our container to be wiped later
        brushObj.transform.localPosition = uvWorldPosition; //The position of the brush (in the UVMap)
        brushObj.transform.localScale = Vector3.one * brushSize;//The size of the brush
    }

    //To update at realtime the painting cursor on the mesh
    void UpdateBrushCursor()
    {
        Vector3 uvWorldPosition = Vector3.zero;
        if (HitTestUVPosition(ref uvWorldPosition) && !saving)
        {
            brushCursor.SetActive(true);
            brushCursor.transform.position = uvWorldPosition + brushContainer.transform.position;
        }
        else
        {
            brushCursor.SetActive(false);
        }
    }
    //Returns the position on the texuremap according to a hit in the mesh collider
    bool HitTestUVPosition(ref Vector3 uvWorldPosition)
    {
        RaycastHit hit;
        Vector3 cursorPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
        Ray cursorRay = sceneCamera.ScreenPointToRay(cursorPos);
        if (Physics.Raycast(cursorRay, out hit, 200))
        {
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
                return false;
            Vector2 pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
            uvWorldPosition.x = pixelUV.x - canvasCam.orthographicSize;//To center the UV on X
            uvWorldPosition.y = pixelUV.y - canvasCam.orthographicSize;//To center the UV on Y
            uvWorldPosition.z = 0.0f;
            return true;
        }
        else
        {
            return false;
        }

    }
   


    void ClearTexture()
    {
        brushCounter = 0;
        foreach (Transform child in brushContainer.transform)
        {//Clear brushes
            Destroy(child.gameObject);
        }
    }

    ////////////////// PUBLIC METHODS //////////////////

    //public void SetBrushMode(Painter_BrushMode brushMode)
    //{ //Sets if we are painting or placing decals
    //    mode = brushMode;
    //    brushCursor.GetComponent<SpriteRenderer>().sprite = brushMode == Painter_BrushMode.PAINT ? cursorPaint : cursorDecal;
    //}
    public void SetBrushSize(float newBrushSize)
    { //Sets the size of the cursor brush or decal
        brushSize = newBrushSize;
        brushCursor.transform.localScale = Vector3.one * brushSize;
    }

}
