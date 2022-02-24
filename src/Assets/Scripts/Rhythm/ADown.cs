

public class ADown : ActivatorObj
{
    private void OnEnable()
    {
        rControls.Rhythm.Down.started += ActionPressed;
        rControls.Rhythm.Down.canceled += ActionReleased;
        rControls.Rhythm.Down.Enable();
    }

    private void OnDisable()
    {
        rControls.Rhythm.Down.Disable();
    }
}
