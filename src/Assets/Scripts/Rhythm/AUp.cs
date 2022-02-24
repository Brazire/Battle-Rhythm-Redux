
public class AUp : ActivatorObj
{ 
    private void OnEnable()
    {
        rControls.Rhythm.Up.started += ActionPressed;
        rControls.Rhythm.Up.canceled += ActionReleased;
        rControls.Rhythm.Up.Enable();
    }

    private void OnDisable()
    {
        rControls.Rhythm.Up.Disable();
    }
}
