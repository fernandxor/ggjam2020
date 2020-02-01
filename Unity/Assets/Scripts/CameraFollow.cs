using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform target;
    Camera camera;

    static CameraFollow cam;

    public static CameraFollow GetInstance() {
        return cam;
    }

    private void Awake()
    {
        cam = this;
        camera = GetComponent<Camera>();
    }
 
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position - Vector3.forward * 10;
    }

    public void ZoomIn(Transform target)
    {
        this.target = target;
        camera.orthographicSize = camera.orthographicSize / 2;
    }

    public void ZoomOut(Transform target = null)
    {
        if (target != null)
            this.target = target;
        camera.orthographicSize = camera.orthographicSize * 2;
    }
}
