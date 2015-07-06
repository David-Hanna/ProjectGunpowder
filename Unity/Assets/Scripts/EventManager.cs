/*
 * 	EventManager.cs
 * 
 *	Type Safe Event System for Unity Game Engine. 
 *	
 *	Author: Adam LeBlanc
 *	Author's Email: leblanca37@gmail.com
 *
 *	Version: 0.1.2
 *	Last Editited: 7/4/2015
 */


    /*
        *************HOW TO USE***************
        - Add this script to an empty gameobject. This gameobject will not be destroyed when a new level is loaded.
        - Add a reference to this gameobject in any script you wish to listen for or raise events.
        - Create your own event types by extending the GameEvent class.
        - Create a Method that conforms to the GameEventDelegate
            - Any method you wish to be called when an event is raised musy confrom to the GameEventDelegate
                - This means that the one and only parameter the method takes must be a type which extends GameEvent
                - In the event where GameEvent type B extends A, and Event A is raised, methods listening for B will not recive the callback.
        - Create a delegate that points to the method you with to have called when the type of event it takes as a parameter is raised
        - Add it to the event manager with the AddSubscriber method.
            - The First parameter is required, and is the delegate.
            - The second parameter is optional, and if true, will make it so the subscriber will still be notifed when an event is raised,
                even if a new level if loaded. It is up to you to make sure the gameobject the script that the delegate belongs to is not destroyed when
                a new level is loaded.
        - To remove an event use the RemoveSubscriber method. You can simply create a new delegate that points to the same method you added previously.
            This will also remove delegates which have been marked as persistant
        - To raise an event. Create a new instance of the type of event you want to raise, and set all fields in the event that meed to be set. Then pass it to the
            Raise method. This will notify all delegates which have subscribed to that event. 

    ***NOTE***
    - If you more than one delegate that points to the same method (or the same delegate more than once), that method will be called that many times when an event is raised.
    - If you do the above, and try to remove them, it will only remove one. So you will have to call RemoveSubscriber once for each time you added it.
    - Functionality, and API is still subject to change as this is an early version. Use at your own risk.
    */
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
        delegates = new Dictionary<Type, Delegate>();
        foreach (KeyValuePair<Type, Delegate> kv in persistentDelegates)
        {
            delegates.Add(kv.Key, kv.Value);
        }
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

        if (persistent)
        {
            if (persistentDelegates.TryGetValue(typeof(T), out tempdel))
            {
                persistentDelegates[typeof(T)] = Delegate.Combine(tempdel, listener);
            }
            else
            {
                persistentDelegates[typeof(T)] = listener;
            }
        }

	}

	public void RemoveSubscriber<T>(GameEventDelegate<T> listener) where T : GameEvent
	{
        Delegate tempdel;
        if (delegates.TryGetValue(typeof(T), out tempdel))
        {
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

        if (persistentDelegates.TryGetValue(typeof(T), out tempdel))
        {
            Delegate curDel = Delegate.Remove(tempdel, listener);

            if (curDel == null)
            {
                persistentDelegates.Remove(typeof(T));
            }
            else
            {
                persistentDelegates[typeof(T)] = curDel;
            }
        }
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
