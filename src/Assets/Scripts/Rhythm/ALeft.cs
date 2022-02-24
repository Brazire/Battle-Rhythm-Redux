
public class ALeft : ActivatorObj
{
    private void OnEnable()
    {
        rControls.Rhythm.Left.started += ActionPressed;
        rControls.Rhythm.Left.canceled += ActionReleased;
        rControls.Rhythm.Left.Enable();
    }

    private void OnDisable()
    {
        rControls.Rhythm.Left.Disable();
    }
}
