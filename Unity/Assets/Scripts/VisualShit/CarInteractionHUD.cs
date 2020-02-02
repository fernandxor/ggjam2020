using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInteractionHUD : MonoBehaviour
{

    [SerializeField] Texture2D pointerTexture;
    [SerializeField] Texture2D plugTexture;


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

    public static void SetPlugIcon(bool active)
    {
        if(active)
            Cursor.SetCursor(instance.plugTexture, new Vector2(0.3f, 1), CursorMode.ForceSoftware);
        else
            Cursor.SetCursor(instance.pointerTexture, Vector2.zero, CursorMode.ForceSoftware);
    }

}
