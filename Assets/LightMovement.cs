using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovement : MonoBehaviour
{
    private float time;
    private Light lightComponent;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        lightComponent = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        lightComponent.intensity = 0.8f + Mathf.Cos(time / 10) / 4;

        float xPos = Mathf.Cos(time/10) * 45;
        float zPos = Mathf.Sin(time/10) * 45;
        transform.position = new Vector3(xPos, transform.position.y, zPos);
        transform.LookAt(new Vector3(50, 0, 50));
    }
}
