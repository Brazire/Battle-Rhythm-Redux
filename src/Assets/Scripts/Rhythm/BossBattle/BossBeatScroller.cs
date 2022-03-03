using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeatScroller : MonoBehaviour
{

    private float tempo, multiplier;
    // Start is called before the first frame update
    void Start()
    {
        tempo = BossRhythmManager.brManager.GetTempo();
        multiplier = BossRhythmManager.brManager.GetMultiplier();
    }

    private void Update()
    {
        transform.position -= new Vector3(0f, (tempo * Time.deltaTime * 2), 0f);
    }

}
