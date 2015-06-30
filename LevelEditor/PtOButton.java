 import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.*;

import javax.imageio.ImageIO;
import javax.swing.*;


//Custom buttom, to allow more easy customizability
public class PtOButton 
{
	public static PtOPanelTag tag = PtOPanelTag.ADD_BUTTON;
	public static final int STANDARD_WIDTH = 50, STANDARD_HEIGHT = 50;
	private BufferedImage icon;
	private JPanel parent;
	private File image;
	private int currentX, currentY, width, height;
	//Position of button
	public PtOButton(int x, int y, int width, int height, JPanel parent)
	{
	
		//new BufferedImage(50, 50, BufferedImage.TYPE_INT_RGB);
		try
		{
			image = new File(getClass().getResource("plus_icon.png").toURI());
			this.icon = ImageIO.read(image);
		}
		catch(IOException e)
		{
			System.out.println("Shit has hit the fan" + e.getMessage());
		}
		catch(Exception e)
		{
			System.out.println("Weird ass one");
		}
		this.parent = parent;
		currentX = x;
		currentY = y;
		this.width = width;
		this.height = height;
	}
	
	public void draw(Graphics g)
	{
		//g.setColor(Color.DARK_GRAY);
		System.out.println(icon.toString());
		//g.fillRect(parent.getX() +  currentX, parent.getY() + currentY, width, height);
		g.drawImage(icon, currentX, currentY, null);
		
	}
	
	
	public void clicked()
	{
		
	}
	
	
	/*
	 * Checks to see if it was clicked, THIS IS IN COMPARISON TO THE PARENT
	 */
	public boolean wasClicked(int xClick, int yClick)
	{
		if( xClick >= currentX && currentX + width <= xClick)
		{
			if(yClick >= currentY && yClick <= currentY + height)
			{
				return true;
			}
		}
		return false;
	}

}
