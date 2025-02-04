namespace WidgetBoard;

public class Scheduler
{
    public void ScheduleAction(TimeSpan timeSpan, Action action)
    {
        Task.Run(async () =>
        {
            await Task.Delay(timeSpan);
            action.Invoke();
        });
    }
}