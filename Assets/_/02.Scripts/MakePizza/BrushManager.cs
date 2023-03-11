using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushManager : MonoBehaviour
{
    public static BrushManager instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public static BrushManager GetInstance()
    {
        return instance;
    }

    public Texture2D sourceBrush;
    public Texture2D cheeseBrush;
    public Texture2D pepperoniBrush;
    public Texture2D oliveBrush;
    public Texture2D potatoBrush;
    public Texture2D shrimpBrush;
    public Texture2D pineappleBrush;
    public Texture2D sweetPotatoBrush;

    private TexturePaintBrush texturePaintBrush;

    void Start()
    {
        texturePaintBrush = GetComponent<TexturePaintBrush>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void cheeseBrushSet()
    {
        texturePaintBrush.brushTexture = cheeseBrush;
        texturePaintBrush.brushSize = 0.2f;
        texturePaintBrush.SetBrushColor(new Color32(255,255,0,255));
        texturePaintBrush.CopyBrushTexture();
    }

    public void sourceBrushSet(Color32 color)
    {
        texturePaintBrush.brushTexture = sourceBrush;
        texturePaintBrush.brushSize = 0.3f;
        texturePaintBrush.SetBrushColor(color);
        texturePaintBrush.CopyBrushTexture();
    }

    public void PepperoniBrushSet(Color32 color)
    {
        texturePaintBrush.brushTexture = pepperoniBrush;
        texturePaintBrush.brushSize = 0.5f;
        texturePaintBrush.SetBrushColor(color);
        texturePaintBrush.CopyBrushTexture();
    }

    public void OliveBrushSet(Color32 color)
    {
        texturePaintBrush.brushTexture = oliveBrush;
        texturePaintBrush.brushSize = 0.5f;
        texturePaintBrush.SetBrushColor(color);
        texturePaintBrush.CopyBrushTexture();
    }

    public void PotatoBrushSet(Color32 color)
    {
        texturePaintBrush.brushTexture = potatoBrush;
        texturePaintBrush.brushSize = 0.5f;
        texturePaintBrush.SetBrushColor(color);
        texturePaintBrush.CopyBrushTexture();
    }

    public void ShrimpBrushSet(Color32 color)
    {
        texturePaintBrush.brushTexture = shrimpBrush;
        texturePaintBrush.brushSize = 0.5f;
        texturePaintBrush.SetBrushColor(color);
        texturePaintBrush.CopyBrushTexture();
    }

    public void PineappleBrushSet(Color32 color)
    {
        texturePaintBrush.brushTexture = pineappleBrush;
        texturePaintBrush.brushSize = 0.5f;
        texturePaintBrush.SetBrushColor(color);
        texturePaintBrush.CopyBrushTexture();
    }

    public void SweetPotatoBrushSet(Color32 color)
    {
        texturePaintBrush.brushTexture = sweetPotatoBrush;
        texturePaintBrush.brushSize = 0.5f;
        texturePaintBrush.SetBrushColor(color);
        texturePaintBrush.CopyBrushTexture();
    }

    public void CreateBrush()
    {
        if (texturePaintBrush.brushActivate == false)
        {
            texturePaintBrush.brushActivate = true;
        }
    }

    public void DeleteBrush()
    {
        if (texturePaintBrush == null)
            return;
        if (texturePaintBrush.brushActivate)
        {
            texturePaintBrush.brushActivate = false;
        }
    }
}
