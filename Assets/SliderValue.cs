using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    void Update()
    {
        float value = GetComponent<Slider>().value;
        GameManager.SetAudioVolume(value);
    }
}