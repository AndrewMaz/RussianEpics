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

        Camera camera = GetComponent<Camera>();

        camera.orthographicSize += _defaultRatio / ratio;
        transform.position = new Vector3(transform.position.x, transform.position.y + _defaultRatio / ratio, transform.position.z);
    }
}
