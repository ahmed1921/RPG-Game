using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{

    public Layer[] LayerProperties =
    {
       Layer.Enemy,
       Layer.Walkable
    };
    [SerializeField] float distanceToground = 100f;
    Camera ViewCamera;
    RaycastHit rayCastHit;
    public RaycastHit hit
    {
        get
        {
            return rayCastHit;
        }
    }
    Layer LayerHit;
    public Layer CurrentLayer
    {
        get
        {
            return LayerHit;
        }
    }

    public delegate void OnLayerChanged(Layer newLayer); //Declare Delegate Type
    public event OnLayerChanged OnLayerChange; //instantiate an observer set.



    // Use this for initialization
    void Start()
    {
        ViewCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        foreach (Layer layer in LayerProperties)
        {
            var hit = RaycastForlayer(layer);
            if (hit.HasValue)
            {
                rayCastHit = hit.Value;
                if (LayerHit != layer)
                {
                    LayerHit = layer;
                    OnLayerChange(layer);
                }
                return;
            }
        }
        rayCastHit.distance = distanceToground;
        LayerHit = Layer.RaycastEndStop;
    }

    RaycastHit? RaycastForlayer(Layer layer)
    {
        int layermask = 1 << (int)layer;
        Ray ray = ViewCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hashit = Physics.Raycast(ray, out hit, distanceToground, layermask);
        if (hashit)
        {
            return hit;
        }
        return null;

    }
}
