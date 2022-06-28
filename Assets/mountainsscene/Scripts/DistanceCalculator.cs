using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class DistanceCalculator : MonoBehaviour
{
    public Transform[] marker;
    // public TextMeshProUGUI[] distanceText;
    public Transform start;
    private static Vector3 markerdist;
    private static Vector3 startpos;
    private float distance;
    void Start()
    {
        startpos = start.position;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < marker.Length; i++)
        {
            markerdist = marker[i].position;
            distance = Vector3.Distance(markerdist, startpos );
            distance = (float)(int)Math.Ceiling((double)distance);
            marker[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().SetText(distance.ToString() + " m");
        }
    }
}
