using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChange : MonoBehaviour
{
    [SerializeField] private Texture2D tex;
    [Tooltip("This is measured from the top left of the image.")]
    [SerializeField] private Vector2 hotspot;
    [SerializeField] private CursorMode cursor;
    private void OnMouseEnter()
    {
        Cursor.SetCursor(tex, hotspot, cursor);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursor);
    }
}
