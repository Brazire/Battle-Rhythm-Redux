public abstract class ScriptableConsumable : ScriptableItem
{
    protected override ItemType Type => ItemType.consumable;

    public float Point;
}
