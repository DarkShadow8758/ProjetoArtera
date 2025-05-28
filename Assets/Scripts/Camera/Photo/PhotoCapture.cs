using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;

    [Header("Flash Effect")]
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashTime;
    [SerializeField] private AudioSource cameraSound;
    [SerializeField] private AudioClip cameraFlashSound;

    [Header("Photo Fader Effect")]
    [SerializeField] private Animator fadingAnimation;
    [Header("Photo verify")]
    [SerializeField] private LayerMask photoLayerMask;
    private Texture2D screenCapture;
    private bool viewingPhoto;

    void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!viewingPhoto)
            {
                StartCoroutine((CapturePhoto()));
            }
            else
            {
                RemovePhoto();
            }
        }
    }

    IEnumerator CapturePhoto()
    {
        //Camera UI set false
        viewingPhoto = true;

        yield return new WaitForEndOfFrame();

         // Raycast do centro da tela
        Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Vector3 rayDirection = Camera.main.transform.forward;
        float rayDistance = 100f;

        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, rayDistance, photoLayerMask))
        {
            // Dispara o evento do PhotoEvents
            GameEventsManager.instance.photoCapture.PhotoCapture(true);
            // Se quiser, pode passar mais informações no evento
        }

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);

        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        ShowPhoto();
        SavePhotoToDisk();
    }
    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;

        photoFrame.SetActive(true);
        cameraSound.PlayOneShot(cameraFlashSound, 1f);
        //Do flash
        StartCoroutine(CameraFlashEffect());
        fadingAnimation.Play("PhotoFade");
    }
    IEnumerator CameraFlashEffect()
    {
        //Play some audio
        cameraFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        cameraFlash.SetActive(false);
    }
    void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
        //CameraUI true 
    }
    void SavePhotoToDisk()
    {
        //convert to png
        byte[] pngData = screenCapture.EncodeToPNG();

        if (pngData != null)
        {
            //define path
            string folderPath = Application.persistentDataPath + "Photos/PlayerCaptures/SavedPhotos";
            //in the future, add personal name based in objcet capture
            string fileName = "Cam_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".png";

            //Create Folder if not exists
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }

            //save png
            string filePath = System.IO.Path.Combine(folderPath, fileName);
            System.IO.File.WriteAllBytes(filePath, pngData);

            Debug.Log("Photo saved to: " + filePath);
        }
        else
        {
            Debug.LogError("Failed to encode the photo to PNG.");
        }
    }
}
