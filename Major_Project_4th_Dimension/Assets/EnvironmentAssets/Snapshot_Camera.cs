using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Snapshot_Camera : MonoBehaviour
{
    Camera snapCam;

    int resWidth = 1920;
    int resHeight = 1080;

    private void Awake()
    {
        snapCam = GetComponent<Camera>();
        if (snapCam.targetTexture == null)
        {
            snapCam.targetTexture = new RenderTexture(resWidth, resHeight, 24);
        }
        else
        {
            resWidth = snapCam.targetTexture.width;
            resHeight = snapCam.targetTexture.height;
        }
        snapCam.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void CallTakeSnapshot()
    {
        snapCam.gameObject.SetActive(true);
    }

    public void LateUpdate()
    {
        if (snapCam.gameObject.activeInHierarchy)
        {
            Texture2D snapshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            snapCam.Render();
            RenderTexture.active = snapCam.targetTexture;
            snapshot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            byte[] bytes = snapshot.EncodeToPNG();
            string fileName = SnapShotName();
            System.IO.File.WriteAllBytes(fileName, bytes);
            Debug.Log("Snapshot Taken!");
            snapCam.gameObject.SetActive(false);
        }
    }

    string SnapShotName()
    {
        return string.Format("{0}/Snapshots/snap_{1}x{2}_{3}.png",
            Application.dataPath,
            resWidth,
            resHeight,
            System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));

    }
}
