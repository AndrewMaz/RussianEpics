
public class OldManQuestEvent : Event
{
    EventService _eventService;
    public void Initialize(EventService eventsService)
    {
        _eventService = eventsService;
        _eventService.AddEvent(this);
    }
    public override void Start()
    {
        base.Start();
    }
    public override void Finish()
    {
        base.Finish();
        _eventService.AddFinishedEvent(this);
    }
}
