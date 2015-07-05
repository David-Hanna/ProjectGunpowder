/*
 * 	EventManager.cs
 * 
 *	Type Safe Event System for Unity Game Engine. 
 *	
 *	Author: Adam LeBlanc
 *	Author's Email: leblanca37@gmail.com
 *
 *	Version: 0.1
 *	Last Editited: 7/4/2015
 */

    //TODO : Add in ussage examples in a long comment
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/*
 * Extend this class to create custome event types, and to pass parameters with the events/
 */
public class GameEvent
{

}


public delegate void GameEventDelegate<T>(T e) where T : GameEvent;

public class EventManager : MonoBehaviour 
{

    public static EventManager Instance
    {
        get;
        private set;
    }

	private Dictionary<Type, Delegate> delegates = new Dictionary<Type, Delegate>();
	private Dictionary<Type, Delegate> persistentDelegates = new Dictionary<Type, Delegate>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void OnLevelWasLoaded()
    {
        //TODO: Code to clone persistentDelegates and assign the clone to delegates
    }

    public void AddSubscriber<T>(GameEventDelegate<T> listener, bool persistent = false) where T : GameEvent
	{

        Delegate tempdel;
        if (delegates.TryGetValue(typeof(T), out tempdel))
        {
            delegates[typeof(T)] = Delegate.Combine(tempdel, listener);
        }
        else
        {
            delegates[typeof(T)] = listener;
        }

       //TODO : Add in code for persistent delegates
       //If presistent == true
       //add delegate to presistentDelegates using same logic as above

	}

	public void RemoveSubscriber<T>(GameEventDelegate<T> listener) where T : GameEvent
	{
        Delegate tempdel;
        if (delegates.TryGetValue(typeof(T), out tempdel))
        {
            Debug.Log("Removed an Event");
            Delegate curDel = Delegate.Remove(tempdel, listener);

            if (curDel == null)
            {
                delegates.Remove(typeof(T));
            }
            else
            {
                delegates[typeof(T)] = curDel;
            }
        }
        //TODO : Add in code for persistent delegates
        //same logic as above
    }

    public void Raise<T>(T e) where T : GameEvent
    {
        //Should maybe throw an exception?
        if (e == null)
            return;
        Delegate del;
        if (delegates.TryGetValue(typeof(T), out del))
        {
            GameEventDelegate<T> callback = del as GameEventDelegate<T>;
            if (callback != null)
                callback(e);
        }
    }
}
