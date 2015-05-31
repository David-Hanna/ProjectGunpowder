
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
public class EventPanel extends JPanel
{
	private int x, y, width, height;
	private int count = 0;
	private final int TOOLBAR_RATIO = 12;
	private JButton testButton;
	private Color testColor;
	public EventPanel(int x, int y, int width, int height)
	{
		testColor = Color.blue;
		
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
		testButton = new JButton("Drag me!");
		testButton.addActionListener(new actionListener());
		this.setPreferredSize(new Dimension(100, 300));
		//testButton.setAlignmentX(100);
		//this.add(testButton);
	}
	
	public int getToolbarSize()
	{
		return height / TOOLBAR_RATIO;
	}
	
	public void draw(Graphics g)
	{
		g.setColor(testColor);
		//Should draw from a given x and y point, though for now just doing individual panels right
		g.fillRect(x, y, width, height);
		//Making it dark grey for now
		
		//Drawing the tool bar
		g.setColor(Color.DARK_GRAY);
		g.fillRect(x, y, width, height / TOOLBAR_RATIO );
		
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
	
	public boolean wasClicked(int otherX, int otherY)
	{
		//Determines if this component got clicked
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
	public void clickedInside(int x, int y)
	{
		System.out.println("Much success");
		count++;
		if(count % 2 == 0)
		{
			testColor = Color.red;
		}
		else
		{
			testColor = Color.blue;
		}
	}
	
	
	
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
}
