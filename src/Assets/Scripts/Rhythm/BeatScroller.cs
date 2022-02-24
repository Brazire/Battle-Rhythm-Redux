using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{

    private float tempo, multiplier;
    // Start is called before the first frame update
    void Start()
    {
        tempo = RhythmManager.rManager.GetTempo();
        multiplier = RhythmManager.rManager.GetMultiplier();
    }

    private void Update()
    {
        transform.position -= new Vector3(0f, (tempo * Time.deltaTime * 2), 0f);
    }


}
