using Abstracts;
using Assets.Scripts.Interfaces;

public class Boss : Enemy, IEventable
{
    private Event _eventItem;
    public void SetEventItem(Event eventItem)
    {
        _eventItem = eventItem;
    }
    public Event GetEvent()
    {
        return _eventItem;
    }
    private new void OnEnable()
    {
        base.OnEnable();
    }
}
