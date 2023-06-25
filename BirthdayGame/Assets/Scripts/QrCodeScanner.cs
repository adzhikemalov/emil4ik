using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class QrCodeScanner : MonoBehaviour
{
    private WebCamTexture _texture;
    
    [SerializeField] private string QrCode = string.Empty;
    [SerializeField] private RawImage CameraOutput;
    [SerializeField] private AspectRatioFitter AspectRatioFitterComponent;
    [SerializeField] private Main MainScript;
    
    Vector3 rotationVector = new Vector3(0f, 0f, 0f);
    // Image uvRect
    Rect defaultRect = new Rect(0f, 0f, 1f, 1f);
    Rect fixedRect = new Rect(0f, 1f, 1f, -1f);

    public void Init()
    {
        _texture = new WebCamTexture();
        CameraOutput.texture = _texture;
        _texture.Play();
        StartCoroutine(GetQRCode());
        Debug.Log("Start coroutine");
    }

    public void Dispose()
    {
        if (_texture)
        {
            _texture.Stop();
            _texture = null;
        }
        StopCoroutine(GetQRCode());
    }

    IEnumerator GetQRCode()
    {
        while (string.IsNullOrEmpty(QrCode))
        {
            try
            {
                IBarcodeReader barCodeReader = new BarcodeReader();
                float h = _texture.height;
                float w = _texture.width;
                
                var Result = barCodeReader.Decode(_texture.GetPixels32(), _texture.width, _texture.height);
                if (Result != null)
                {
                    //Scanned QR Code
                    QrCode = Result.Text;
                    
                    if (!string.IsNullOrEmpty(QrCode))
                    {
                        Debug.Log(QrCode);
                        MainScript.OpenQuestion(QrCode);
                        QrCode = string.Empty;
                        break;
                    }
                }
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }
            yield return null;
        }

        StopCoroutine(GetQRCode());
    }
    // Update is called once per frame
    void Update()
    {
        if (_texture.width < 100)
        {
            Debug.Log("Still waiting another frame for correct info...");
            return;
        }

        // Rotate image to show correct orientation 
        rotationVector.z = -_texture.videoRotationAngle;
        CameraOutput.rectTransform.localEulerAngles = rotationVector;

        // Set AspectRatioFitter's ratio
        float videoRatio =
            (float)_texture.width / (float)_texture.height;
        AspectRatioFitterComponent.aspectRatio = videoRatio;

        // Unflip if vertically flipped
        CameraOutput.uvRect =
            _texture.videoVerticallyMirrored ? fixedRect : defaultRect;

    }
}
