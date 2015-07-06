using UnityEngine;
using System.Collections;

public class EventTest : MonoBehaviour {

    double timeSinceStart = 0;

	// Use this for initialization
	void Start () {
        GameEventDelegate<TimeEvent> oneOff = OneOffTime;

        EventManager.Instance.AddSubscriber(oneOff);

        GameEventDelegate<TimeEvent> persistent = PersistentTime;

        EventManager.Instance.AddSubscriber(persistent, true);
    }

    void Update()
    {
        timeSinceStart += Time.deltaTime;

        if (timeSinceStart > 5)
            EventManager.Instance.Raise(new TimeEvent(timeSinceStart));
    }

    private void OneOffTime(TimeEvent e)
    {
        Debug.Log(string.Format("One Off time Event, {0}", e.time));
        //GameEventDelegate<TimeEvent> ge = OneOffTime;

        //EventManager.Instance.RemoveSubscriber(ge);
        Application.LoadLevel("EventManagerTestSecond");
    }

    private void PersistentTime(TimeEvent e)
    {
        Debug.Log(string.Format("Presistant time Event"));

        if (e.time >= 20)
        {
            GameEventDelegate<TimeEvent> ge = PersistentTime;

            EventManager.Instance.RemoveSubscriber(ge);
        }
    }

}

public class TimeEvent : GameEvent
{
    public double time;
    public TimeEvent(double time)
    {
        this.time = time;
    }
    
}