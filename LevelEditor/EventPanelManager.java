import java.awt.*;
import java.util.*;
import java.awt.Event.*;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.awt.event.MouseMotionListener;

import javax.swing.*;
public class EventManagerPanel extends JPanel
{
	//Let's work on having this dynamically add and snap panels in eh
	public static final int Y_PADDING_FROM_TOP = 10, X_PADDING_FROM_RIGHT = 10, PANEL_HEIGHT = 50, PANEL_WIDTH = 250, WIDTH_TO_PANEL = 250;

	private static final double CLICK_TIME = 0.2;
	private ArrayList<EventPanel> panels;
	private int selectedIndex, snapIndex = 0;
	private PtOTimer timer = null;
	//Current panel being dragged
	//private EventPanel draggedPanel;
	//To be used for the panel switching
	private EventPanel selectedPanel;
	public EventManagerPanel()
	{
		//We're putting the actual panel starting at halfway of this panel, as well the length is whatever is left minus the padding
		this.setPreferredSize(new Dimension(500, 500));
		this.panels = new ArrayList<EventPanel>();
		this.addMouseListener(new mouseListener());
		//Let's make it draw two panels, one end and one not
		//Hard code it here, redraw it in paint component
		int theX = 250;
		int theY = Y_PADDING_FROM_TOP;
		int width = (PANEL_WIDTH)- X_PADDING_FROM_RIGHT;
		panels.add(new SpecialEventPanel(theX, theY, width, PANEL_HEIGHT, true, Color.red));
		panels.add(new SpecialEventPanel(theX, theY + PANEL_HEIGHT + Y_PADDING_FROM_TOP, width, PANEL_HEIGHT, false, Color.blue));
	}
	
	
	
	//for now just adds a panel to the current list, I'll make a button out of it soon
	public void addPanel()
	{
		//Gives us the index of where this is being added as this just defaults to the end
		int i = panels.size() - 2;

		EventPanel letsHope = new EventPanel(250, Y_PADDING_FROM_TOP, 250 - X_PADDING_FROM_RIGHT, PANEL_HEIGHT, Color.magenta);
		panels.add(i, letsHope);
	}
	
	public void paintComponent(Graphics g)
	{
		super.paintComponent(g);
		/*
		selectedIndex = -1;
		//Need to find the "selected index" if there is one
		if(selectedPanel != null)
		{
			int i = 0;
			while(i < panels.size() && selectedIndex == -1)
			{
				if(!panels.get(i).equals(selectedPanel))
				{
					int yToParse = selectedPanel.getY();
					if(panels.get(i).yCheck(yToParse + Y_PADDING_FROM_TOP))
					{
						//if(panels.get(i).clickedInside(selectedPanel.getX(), selectedPanel.getY());
						panels.get(i).swap(yToParse, PANEL_HEIGHT + Y_PADDING_FROM_TOP);
						selectedIndex = i;
						snapIndex = selectedIndex;
					}
				}
				i++;
			}
			System.out.println(selectedIndex);
		}
		*/
	
		
		for(int i = 0; i < panels.size(); i++)
		{
			
			panels.get(i).drawAt(g, WIDTH_TO_PANEL, (i * (PANEL_HEIGHT + Y_PADDING_FROM_TOP) + Y_PADDING_FROM_TOP));
		}
		
	}
	
	
	
	
	/**
	 * Delegate class for the timer
	 * 
	 */
	public void timerDidFinish()
	{
		//If the timer is done, simply set the timer to null
		timer = null;
	}
	
	/**
	 * Swap method, takes two panels and then swaps the first one with the second
	 */
	public void swap(EventPanel paneToSwitch, EventPanel toHere)
	{
		
		if(toHere != null && paneToSwitch != null)
		{
			//Pretty straightforward swap
			int index = panels.indexOf(paneToSwitch);
			int index2 = panels.indexOf(toHere);
			panels.set(index, toHere);
			panels.set(index2, paneToSwitch);
		}			
	
	}
	/**
	private class mouseMotionListener implements MouseMotionListener
	{

		@Override
		public void mouseDragged(MouseEvent arg0) {
			// TODO Auto-generated method stub
			//For now designing this to be used with arrow keys not with drag
			
			//Uncomment to have click and drag feature
			if(selectedPanel != null)
			{
				//Just need to pop this out and hey only drags up or down
				//selectedPanel.setX(arg0.getX());
				if(arg0.getY() <= 0)
				{
					selectedPanel.setY(0);
				}
				else
				{
					selectedPanel.setY(arg0.getY());
				}
				repaint();
			}
		}

		@Override
		public void mouseMoved(MouseEvent arg0) {
			// TODO Auto-generated method stub
			
		}
		
	}
	*/
	private class mouseListener implements MouseListener
	{
		private EventPanel lastPanel =  null;

		@Override
		public void mouseClicked(MouseEvent arg0) {

		}

		@Override
		public void mouseEntered(MouseEvent arg0) {
			// TODO Auto-generated method stub
			
		}

		@Override
		public void mouseExited(MouseEvent arg0) {
			// TODO Auto-generated method stub
			
		}

		@Override
		public void mousePressed(MouseEvent arg0) 
		{

			if(selectedPanel != null)
			{
				EventPanel target = null;
				for(EventPanel panel : panels)
				{
					if(panel.wasClicked(arg0.getX(), arg0.getY()))
						target = panel;
				}
				swap(selectedPanel, target);
			}
			else
			{
			//Using the width and x and y of the panel, determine if it was hit, should use a helper method in the panel maybe
				
			for(int i = 0; i < panels.size(); i++)	
			{			
				//No matter what it's an event panel so
				EventPanel testPanel = panels.get(i);
				if(testPanel.wasClicked(arg0.getX(), arg0.getY()))
				{
					PtOPanelTag tag = testPanel.clickedInside(arg0.getX() - testPanel.getX() , arg0.getY() - testPanel.getY());	
					
					//here we need to do a check to see what we clicked
					if(tag == PtOPanelTag.ADD_BUTTON)
					{
						for(int j = 0; j < panels.size(); j++)
						{
							if(panels.get(j).equals(testPanel))
							{
								panels.add(j, new EventPanel(0,0, PANEL_WIDTH, PANEL_HEIGHT, Color.MAGENTA));
								repaint();
								
							}
						}
					}
					else if(tag == PtOPanelTag.EVENT_PANEL)
					{
						//At this point we just need to check to see if we have a double click on a panel
						if(timer != null && lastPanel != null && lastPanel.equals(testPanel))
						{
							timer.pause();
							if(selectedPanel != null)	
							{
								selectedPanel.isSelected(false);	
							}
							selectedPanel = testPanel;
							selectedPanel.isSelected(true);
						
							repaint();
						}
						else
						{
							lastPanel = testPanel;
						}
				
					}
				}
				
			}
				if(timer == null)
				{
					timer =  new PtOTimer(CLICK_TIME, EventManagerPanel.this);
					timer.start();
				}
			}
		}

		@Override
		public void mouseReleased(MouseEvent arg0) {
			//Making a timer here, simple timer that simply waits x time and returns a call to the delegate
		}
		
	}
}
