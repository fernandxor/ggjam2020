using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBar : MonoBehaviour
{
    Image image;
    [SerializeField] Player player;
    [SerializeField] Image fillerImage;

    float colorInterpolator, colorValue;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        var normalizedHealth = player.Health / 100f;

        fillerImage.fillAmount = normalizedHealth;

        if (normalizedHealth < 0.3f)
        {
            colorValue = Mathf.Repeat(colorValue + Time.deltaTime * 4, 2f);
            colorInterpolator = Mathf.PingPong(colorValue, 1f);
            image.color = Color.Lerp(Color.black, Color.red, colorInterpolator);
        }

    }
}
