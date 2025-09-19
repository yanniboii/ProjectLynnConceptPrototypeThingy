using System.Collections;
using UnityEngine;

public class TakePicture : MonoBehaviour
{
    [SerializeField] private int w;
    [SerializeField] private int h;
    [SerializeField] private Texture2DValue texture2DValue;
    [SerializeField] private RenderTextureValue renderTextureValue;
    [SerializeField] private EventHolder onPictureTaken;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        texture2DValue.value = new Texture2D(w, h, TextureFormat.RGB24, false);
    }

    IEnumerator CapturePhoto_Co()
    {
        yield return new WaitForEndOfFrame();

        if (renderTextureValue.value == null)
            renderTextureValue.value = new RenderTexture(w, h, 24);

        Camera.main.targetTexture = renderTextureValue.value;

        Camera.main.Render();

        Camera.main.targetTexture = null;

        onPictureTaken.Invoke();
    }

    public void Capture()
    {
        StartCoroutine(CapturePhoto_Co());
    }
}
