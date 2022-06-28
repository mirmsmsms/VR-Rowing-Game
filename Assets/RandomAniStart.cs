using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAniStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
             StartCoroutine(RandomStartTime(child.GetComponent<Animator>()));
    }

    IEnumerator RandomStartTime(Animator anim)
    {
        yield return new WaitForSeconds(Random.Range(0,5));
        anim.enabled = true;
    }
}
