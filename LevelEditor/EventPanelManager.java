import java.awt.*;
import java.awt.Event.*;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.awt.event.MouseMotionListener;

import javax.swing.*;
public class EventManagerPanel extends JPanel
{
	private final int Y_PADDING_FROM_TOP = 30, X_PADDING_FROM_RIGHT = 10;
	private EventPanel testPanel;
	//Current panel being dragged
	private EventPanel draggedPanel;
	public EventManagerPanel()
	{
		this.setPreferredSize(new Dimension(500, 500));
		testPanel = new EventPanel(0, 0, 300, 100);
		this.addMouseListener(new mouseListener());
		this.addMouseMotionListener(new mouseMotionListener());
	}
	
	
	public void paintComponent(Graphics g)
	{
		super.paintComponent(g);
		//Draw the background for the right panel
		//Need to find an appropriate size, should be easy itll  be half the panel width
		int width = (this.getWidth() / 2) - X_PADDING_FROM_RIGHT;
		int height = this.getHeight() - (2 * Y_PADDING_FROM_TOP);
		int x = this.getWidth() / 2;
		int y = 0 + Y_PADDING_FROM_TOP;
		//Now we have the dimensions color it in
		
		testPanel.draw(g);
		
	}
	
	
	private class mouseMotionListener implements MouseMotionListener
	{

		@Override
		public void mouseDragged(MouseEvent arg0) {
			// TODO Auto-generated method stub
			//For now designing this to be used with arrow keys not with drag
			if(draggedPanel != null)
			{
				draggedPanel.setX(arg0.getX());
				draggedPanel.setY(arg0.getY());
				System.out.println("Not here?");
				repaint();
			}
		}

		@Override
		public void mouseMoved(MouseEvent arg0) {
			// TODO Auto-generated method stub
			
		}
		
	}
	private class mouseListener implements MouseListener
	{

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
		public void mousePressed(MouseEvent arg0) {
			// TODO Auto-generated method stub
			// TODO Auto-generated method stub
			System.out.println("It was clicked here " + (arg0.getX()));
			
			//Using the width and x and y of the panel, determine if it was hit, should use a helper method in the panel maybe
			if(testPanel.wasClicked(arg0.getX(), arg0.getY()))
			{
				testPanel.clickedInside(arg0.getX() - testPanel.getX() , arg0.getY() - testPanel.getY());
				draggedPanel = testPanel;
			}
		}

		@Override
		public void mouseReleased(MouseEvent arg0) {
			// TODO Auto-generated method stub
			
		}
		
	}
}
