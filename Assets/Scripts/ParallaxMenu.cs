using UnityEngine;
using UnityEngine.UI;

public class ParallaxMenu : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] private float animationSpeed = 0.1f;

    private void Update()
    {
        if (rawImage)
        {
            rawImage.uvRect = new Rect(
                rawImage.uvRect.x + animationSpeed * Time.deltaTime, 
                rawImage.uvRect.y, 
                rawImage.uvRect.width, 
                rawImage.uvRect.height
            );
        }
    }
}
