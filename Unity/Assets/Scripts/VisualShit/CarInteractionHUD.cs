using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInteractionHUD : MonoBehaviour
{

    [SerializeField] Texture2D pointerTexture;


    static CarInteractionHUD instance;
    

    void Awake()
    {
        instance = this;
        Cursor.SetCursor(pointerTexture, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.visible = false;
    }

    public static void SetVisible(bool visible)
    {
        Cursor.visible = visible;
    }

}
