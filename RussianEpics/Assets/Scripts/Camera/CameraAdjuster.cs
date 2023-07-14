using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    const float _defaultRatio = 2f;
    void Start()
    {
        float ratio = (float)Screen.width / Screen.height;

        if (ratio == _defaultRatio) return;

        float difference = _defaultRatio / ratio;

        if (ratio > _defaultRatio)
        {
            difference = -difference;
        }
        Camera camera = GetComponent<Camera>();

        /*Debug.Log(Screen.width);
        Debug.Log(Screen.height);*/

        camera.orthographicSize += difference;
        transform.position = new Vector3(transform.position.x, transform.position.y + difference, transform.position.z);
    }
}
