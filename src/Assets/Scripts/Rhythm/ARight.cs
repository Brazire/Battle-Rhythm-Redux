
public class ARight : ActivatorObj
{
    private void OnEnable()
    {
        rControls.Rhythm.Right.started += ActionPressed;
        rControls.Rhythm.Right.canceled += ActionReleased;
        rControls.Rhythm.Right.Enable();
    }

    private void OnDisable()
    {
        rControls.Rhythm.Right.Disable();
    }
}
