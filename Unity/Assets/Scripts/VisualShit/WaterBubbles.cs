using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBubbles : MonoBehaviour
{

    [SerializeField]
    float speed = 1f;

    RawImage rawImage;
    float angle;
    Rect rect;

    void Awake()
    {
        rawImage = GetComponent<RawImage>();
        rect = rawImage.uvRect;
    }

    void Update()
    {

        angle = Mathf.Repeat(angle + Time.deltaTime * 180 * speed, 360);

        rect.x = Mathf.Sin(Mathf.Deg2Rad * angle);

        rect.y = Mathf.Repeat(rect.y - Time.deltaTime * 2f * speed, 1f);

        rawImage.uvRect = rect;
    }
}
