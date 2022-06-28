using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyController : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObject.index);
        if(device.GetAxis().x != 0 || device.GetAxis().y != 0)
        {
            Debug.Log(device.GetAxis().x + " " + device.GetAxis().y);
        }
    }
    void Trigger(object sender, ClickedEventArgs e)
    {
        Debug.Log("trigger has been pressed");
    }
}
