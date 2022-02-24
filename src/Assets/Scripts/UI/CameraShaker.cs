using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private float shake, shakeAmount, decreaseFactor;
    [SerializeField] GameObject camera;

    // Update is called once per frame
    void Update()
    {
        if (shake > 0f)
        {
            camera.transform.position = Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            camera.transform.position = new Vector3(0f, 0f, 0f);
            shake = 0f;
        }
    }

    public void StartShake()
    {
        shake = 0.4f;
    }
}
