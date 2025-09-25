using UnityEngine;
using UnityEngine.UI;

public class FakeButtonPress : MonoBehaviour
{
    [SerializeField] private EventHolder onPictureTaken;
    [SerializeField] private Image flashImage;
    [SerializeField] private RawImage pressedImage;
    [SerializeField] private RawImage unPressedImage;

    private void OnEnable()
    {
        onPictureTaken.Subscribe(FakePress);
    }

    private void FixedUpdate()
    {

    }

    private void OnDisable()
    {
        onPictureTaken.Unsubscribe(FakePress);
    }


    void FakePress()
    {

    }
}
