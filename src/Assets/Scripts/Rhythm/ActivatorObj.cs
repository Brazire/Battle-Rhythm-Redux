using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class ActivatorObj : MonoBehaviour
{
    protected SpriteRenderer sr;
    public Sprite defaultSprite, pressedSprite;
    protected RhythmControls rControls;
    
    // Start is called before the first frame update
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rControls = new RhythmControls();
    }

    protected void ActionPressed(InputAction.CallbackContext obj)
    {
        sr.sprite = pressedSprite;
    }

    protected void ActionReleased(InputAction.CallbackContext obj)
    {
        sr.sprite = defaultSprite;
    }
}
