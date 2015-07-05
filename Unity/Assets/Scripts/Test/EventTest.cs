using UnityEngine;
using System.Collections;

public class EventTest : MonoBehaviour {

    double timeSinceStart = 0;

	// Use this for initialization
	void Start () {
        GameEventDelegate<TimeEvent> ge = TimeListener;

        EventManager.Instance.AddSubscriber(ge);
	}

    void Update()
    {
        timeSinceStart += Time.deltaTime;

        if (timeSinceStart >= 5)
            EventManager.Instance.Raise(new TimeEvent(timeSinceStart));
    }

    private void TimeListener(TimeEvent e)
    {
        Debug.Log(e.time);
        GameEventDelegate<TimeEvent> ge = TimeListener;

        EventManager.Instance.RemoveSubscriber(ge);
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
