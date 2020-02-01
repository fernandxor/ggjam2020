using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBar : MonoBehaviour
{

    [SerializeField] Player player;
    [SerializeField] Image fillerImage;

    void Update()
    {
        fillerImage.fillAmount = player.Health / 100f;
    }
}
