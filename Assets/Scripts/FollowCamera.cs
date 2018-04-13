using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    private Transform mainCam;

    private void Start()
    {
        mainCam = GameObject.Find("Main Camera").transform;
    }

    private void Update()
    {
        transform.LookAt(mainCam);
    }
}
