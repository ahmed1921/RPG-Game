using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

    CameraRaycaster cameraRayCaster;
    [SerializeField] Texture2D WalCursor = null;
    [SerializeField] Texture2D UnknownCursor = null;
    [SerializeField] Texture2D EnemyCursor = null;
    [SerializeField] Vector2 cursorHotSpot = new Vector2(0,0);
	// Use this for initialization
	void Start () {
        cameraRayCaster = GetComponent<CameraRaycaster>();
        cameraRayCaster.OnLayerChange += OnLayerChanged;
    }
	
	// Update is called once per frame
	void OnLayerChanged (Layer layer) {
        switch (layer)
        {
            case Layer.Walkable:
                Cursor.SetCursor(WalCursor, cursorHotSpot, CursorMode.Auto);
                break;
            case Layer.RaycastEndStop:
                Cursor.SetCursor(UnknownCursor, cursorHotSpot, CursorMode.Auto);
                break;
            case Layer.Enemy:
                Cursor.SetCursor(EnemyCursor, cursorHotSpot, CursorMode.Auto);
                break;
            default:
                Debug.LogError("Don't know which cursor to show");
                break;

        }
    }
}
//TODO consider de- registering on OnLayerChanged on leaving all game scenes