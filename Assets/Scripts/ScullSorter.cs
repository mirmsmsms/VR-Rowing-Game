using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScullSorter : MonoBehaviour
{
    public enum Seats {One = 1, Two = 2, Four= 4, Eight = 8};
    public enum Oar {Sweep, Scull, None};

    public Seats seats;
    public Oar oar;
    public bool coxed;

    public float rowerGap;
    public float bowOffset;
    public float sternOffset;
    public float hullOffset;
    public float hullExpand;

    public GameObject cox;
    public Transform bow;
    public Transform stern;
    public Transform hull;

    Scull scull;

    // Start is called before the first frame update
    void Start()
    {
        scull = GetComponent<Scull>();
        Sort();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Sort();
        }
    }

    void Sort()
    {
        int rowerCount = (int)seats;
        for (int i = 0; i < scull.rowers.Length; i++)
        {
            scull.rowers[i].gameObject.SetActive(i < rowerCount);
            scull.rowers[i].transform.localPosition = new Vector3(0, 0, (i + (coxed ? 1 : 0)) * rowerGap);
            if (oar == Oar.Scull)
            {
                scull.rowers[i].collarL.SetActive(true);
                scull.rowers[i].collarR.SetActive(true);
                scull.rowers[i].riggerL.SetActive(true);
                scull.rowers[i].riggerR.SetActive(true);
            }
            else if (oar == Oar.Sweep)
            {
                scull.rowers[i].collarL.SetActive(i % 2 != 0);
                scull.rowers[i].collarR.SetActive(i % 2 == 0);
                scull.rowers[i].riggerL.SetActive(i % 2 != 0);
                scull.rowers[i].riggerR.SetActive(i % 2 == 0);
            }
            else
            {
                scull.rowers[i].collarL.SetActive(false);
                scull.rowers[i].collarR.SetActive(false);
                scull.rowers[i].riggerL.SetActive(false);
                scull.rowers[i].riggerR.SetActive(false);
            }
        }

        bow.localPosition = new Vector3(0, 0, bowOffset);
        stern.localPosition = new Vector3(0, 0, sternOffset + (rowerGap * (rowerCount - 1 + (coxed ? 1 : 0))));
        hull.localScale = new Vector3(1, 1, hullOffset + (hullExpand * (rowerCount + (coxed ? 1 : 0))));
        cox.SetActive(coxed);

    }
}
