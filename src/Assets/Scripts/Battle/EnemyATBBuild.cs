using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyATBBuild : MonoBehaviour
{
    private float atbCounter, atbMax;
    private bool waiting;
    private Enemy enemy;
    [SerializeField] ATBSlide slide;
    [SerializeField] GameObject numbers;


    private void Start()
    {
        waiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (atbCounter < atbMax)
        {
            atbCounter += 1 * Time.deltaTime;
            slide.SetSlider(atbCounter);
        }
        else if (!waiting)
        {
            waiting = true;
            enemy.ChoosePostATB();
        }
    }

    public void SetEnemy(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void SetMax(float amount)
    {
        atbMax = amount;
        slide.SetMaxSlider(amount);
    }

    public void ResetCounter()
    {
        atbCounter = 0;
        slide.SetSlider(atbCounter);
        waiting = false;
    }
}
