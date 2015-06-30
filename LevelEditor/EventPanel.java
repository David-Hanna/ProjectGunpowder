
/**
 * Represents a single event node, will be extended to be used in all nodes, whatever extends it should overrite the "wasClicked" method with
 * the method of their choosing, deciding which panel is to receive it is the managers job
 * @author Patty
 *
 */
import java.awt.*;

import javax.swing.*;

import java.awt.event.*;
import java.awt.Event.*;
import java.util.*;
public class EventPanel extends JPanel
{
	private ArrayList<PtOButton> buttons;
	public static final PtOPanelTag tag = PtOPanelTag.EVENT_PANEL;
	private UUID panelID;
	private int x, y, width, height;
	private int count = 0;
	private final int TOOLBAR_RATIO = 12, HIGHLIGHTWIDTH = 5;
	private JButton testButton;
	//Color to be displayed while selected, will be an outline
	private Color testColor, highlightColor = Color.CYAN;
	private boolean selected = false;
	public EventPanel(int x, int y, int width, int height, Color color)
	{
		
		testColor = color;
		buttons = new ArrayList<PtOButton>();
		buttons.add(new PtOButton(x + 10, y + 10, PtOButton.STANDARD_WIDTH, PtOButton.STANDARD_HEIGHT, this));
		//Now we just have this get a random id, will be used in the equals method
		
		panelID = UUID.randomUUID();
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
		
		this.setPreferredSize(new Dimension(100, 300));
		//testButton.setAlignmentX(100);
		//this.add(testButton);
	}
	
	public void isSelected(boolean selected)
	{
		this.selected = selected;
	}
	
	public int getToolbarSize()
	{
		return height / TOOLBAR_RATIO;
	}
	
	/**
	 * Swap function, what it does is take the y position of whatever initiated the swap, along wth the swap distance.  
	 * It then decides to swap up or down
	 * @param g
	 */
	public void swap(int y, int swapDistance)
	{
		if(y < this.y)
		{
			//Then we swap up
			this.y -= swapDistance;
		}
		else
		{
			//Then we swap down
			this.y += swapDistance;
		}
	}
	
	public void draw(Graphics g)
	{

		//Drawing the highlight maybe
		if(selected)
		{
			g.setColor(highlightColor);
			g.fillRect(x - HIGHLIGHTWIDTH, y - HIGHLIGHTWIDTH, width + HIGHLIGHTWIDTH * 2, height + HIGHLIGHTWIDTH * 2);
		}
		
		g.setColor(testColor);
		//Should draw from a given x and y point, though for now just doing individual panels right
		System.out.println("X is " + x + "and y is" + y);
		g.fillRect(x, y, width, height);
		//Making it dark grey for now
		
		//Drawing the tool bar
		g.setColor(Color.DARK_GRAY);
		g.fillRect(x, y, width, height / TOOLBAR_RATIO );
		
		//Draw the buttons
		for(PtOButton button : buttons)
		{
			button.draw(g);
		}
		
		

	}
	
	
	
	
	
	@Override
	public boolean equals(Object otherPanel)
	{
		if(((EventPanel)otherPanel).getId().equals(panelID))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	//Function as moving it as well as setting the position to be the one pcified
	public void drawAt(Graphics g, int x, int y)
	{
		this.x = x;
		this.y = y;
		g.setColor(Color.GRAY);
		//Should draw from a given x and y point, though for now just doing individual panels right
		g.fillRect(x, y, width, height);
		//Making it dark grey for now
		
		//Drawing the tool bar
		g.setColor(Color.DARK_GRAY);
		g.fillRect(x, y, width, height / TOOLBAR_RATIO );
	}
	
	private class actionListener implements ActionListener
	{
		public void actionPerformed(ActionEvent event)
		{
			System.out.println("Something");
		}
	}
	
	
	
	/**
	 * Less load intensive collision detection for just y
	 * 
	 */
	public boolean yCheck(int otherY)
	{
		if(otherY <= y + height && otherY >= y)
		{
			return true;
		}
		return false;
	}
	
	/**
	 * Can use this for a coliision detection
	 * @param otherX
	 * @param otherY
	 * @return
	 */
	public boolean wasClicked(int otherX, int otherY)
	{

		if(otherX > x && otherX < x + width)
		{
			if(otherY > y && otherY < y + height)
			{
				return true;
			}
		}
		return false;
	}
	
	//This is a general method that simply will receive the x and y coordinate relative to the component which received the click
	public PtOPanelTag clickedInside(int x, int y)
	{
		count++;
		PtOButton clickedButton = null;
		//Determines if this component got clicked
		for(PtOButton button : buttons)
		{
			if(button.wasClicked(x, y))
			{
				clickedButton = button;
			}
		}
		//IF we have clicked a button, we don't react to the click the button does
		if(clickedButton != null)
		{
			return clickedButton.tag;
		}
		else
		{
			if(count % 2 == 0)
			{
				testColor = Color.red;
			}
			else
			{
				testColor = Color.blue;
			}
			return tag;
		}
	}
	
	
	
	
	//Will be useful for telling which events are at the end or beginning, could just use indexes actually...right
	/**
	 * Getters and setters galore 
	 */
	public int getY()
	{
		return y;
	}
	public void setY(int y)
	{
		this.y = y;
	}
	public int getX()
	{
		return x;
	}
	public void setX(int x)
	{
		this.x = x;
	}
	public int getWidth()
	{
		return width;
	}
	public void setWidth(int width)
	{
		this.width = width;
	}
	
	public int getHeight()
	{
		return height;
	}
	public void setHeight(int height)
	{
		this.height = height;
	}
	public UUID getId()
	{
		return panelID;
	}
	
}
