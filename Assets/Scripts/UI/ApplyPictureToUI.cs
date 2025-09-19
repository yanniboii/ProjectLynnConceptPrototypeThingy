using UnityEngine;
using UnityEngine.UI;

public class ApplyPictureToUI : MonoBehaviour
{
    [SerializeField] private RenderTextureValue renderTextureValue;
    [SerializeField] private EventHolder onPictureTaken;

    [SerializeField] private RawImage rawImage;
    [SerializeField] private int w;
    [SerializeField] private int h;

    private void OnEnable()
    {
        onPictureTaken.Subscribe(SetPicture);
    }

    private void OnDisable()
    {
        onPictureTaken.Unsubscribe(SetPicture);
    }

    private void SetPicture()
    {

        rawImage.texture = renderTextureValue.value;
    }
}
