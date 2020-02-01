using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    static CameraFollow instance;
    public static CameraFollow GetInstance() {
        return instance;
    }


    [SerializeField] Transform target;
    [SerializeField] float nearZoom = 2, farZoom = 5;
    Camera cam;
    float targetSize;
    Vector2 targetPosition;

    private void Awake()
    {
        instance = this;
        cam = GetComponent<Camera>();
        targetSize = cam.orthographicSize;
    }
 
    // Update is called once per frame
    void LateUpdate()
    {

        targetPosition = target.position - Vector3.forward * 10;

        Vector3 v = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
        v.z = -10;

        transform.position = v;

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, Time.deltaTime * 3);

    }

    public void ZoomIn(Transform target)
    {
        this.target = target;

        targetSize = nearZoom;
    }

    public void ZoomOut(Transform target = null)
    {
        if (target != null)
            this.target = target;

        targetSize = farZoom;
    }
}
