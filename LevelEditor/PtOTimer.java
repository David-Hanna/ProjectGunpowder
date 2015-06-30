


public class PtOTimer extends Thread
{
	private EventManagerPanel managerDelegate;
	private double time;
	private boolean running  = true;
	//BAsic timer class ust is a thread that slseeps for x time, not very load intensive and since it doesnt have to be perfect we can just sleep
	//Long time is in ahm
	//Seconds
	public PtOTimer(double time, EventManagerPanel delegate)
	{
		managerDelegate = delegate;
		this.time = time;
	}
	
	/**
	 * Should be used to not create a new thread,should only ever be one thread running
	 * @param time
	 */
	public void reset(long time)
	{
		this.time = time;
	}
	
	public void run()
	{
		while(time >= 0 && running)
		{
			time -= 0.1;
			try
			{
				sleep(100);
			}
			catch(InterruptedException e)
			{
				System.out.println("Hrm");
			}
		}
		System.out.println("Done");
		managerDelegate.timerDidFinish();
	}
	
	/**
	 * Pause 
	 */
	public void pause()
	{
		running = false;
	}
}
