using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace Eu4ModEditor
{
    class InputManagement
    {
        public static void IgnoreKeyPress(object sender, KeyEventArgs e)
        {
            Control c = (Control)sender;
            ComboBox cb = null;
            if (c is ComboBox)
            {
                cb = (ComboBox)c;
                if (cb.DroppedDown)
                    return;
            }
            e.SuppressKeyPress = true;
        }

        public static void __HandleMoveButton(string name)
        {

            Point CameraPositionFirst = GlobalVariables.CameraPosition;

            //TODO
            //Make this smaller for smaller windows
            if (name == "RightButton")
                GlobalVariables.CameraPosition.X += 220;
            else if (name == "LeftButton" && GlobalVariables.CameraPosition.X > 0)
                GlobalVariables.CameraPosition.X -= 220;
            else if (name == "UpButton" && GlobalVariables.CameraPosition.Y > 0)
                GlobalVariables.CameraPosition.Y -= 160;
            else if (name == "DownButton")
                GlobalVariables.CameraPosition.Y += 160;
            if (GlobalVariables.CameraPosition.X + GlobalVariables.MapDrawingWidth >= GlobalVariables.ProvincesMap.Width)
                GlobalVariables.CameraPosition.X = GlobalVariables.ProvincesMap.Width - GlobalVariables.MapDrawingWidth;
            else if (GlobalVariables.CameraPosition.X < 0)
                GlobalVariables.CameraPosition.X = 0;
            if (GlobalVariables.CameraPosition.Y + GlobalVariables.MapDrawingHeight >= GlobalVariables.ProvincesMap.Height)
                GlobalVariables.CameraPosition.Y = GlobalVariables.ProvincesMap.Height - GlobalVariables.MapDrawingHeight;
            else if (GlobalVariables.CameraPosition.Y < 0)
                GlobalVariables.CameraPosition.Y = 0;

            Rectangle toDraw = new Rectangle();

            int XDifference = GlobalVariables.CameraPosition.X - CameraPositionFirst.X;
            int YDifference = GlobalVariables.CameraPosition.Y - CameraPositionFirst.Y;

            if (XDifference < 0)
            {
                toDraw = new Rectangle(GlobalVariables.CameraPosition, new Size(XDifference*-1, GlobalVariables.MapDrawingHeight));
            }
            else if (XDifference > 0)
            {
                toDraw = new Rectangle(CameraPositionFirst.X + GlobalVariables.MapDrawingWidth, GlobalVariables.CameraPosition.Y, XDifference, GlobalVariables.MapDrawingHeight);
            }
            else if (YDifference < 0)
            {
                toDraw = new Rectangle(GlobalVariables.CameraPosition, new Size(GlobalVariables.MapDrawingWidth, YDifference*-1));
            }
            else if (YDifference > 0)
            {
                toDraw = new Rectangle(GlobalVariables.CameraPosition.X, CameraPositionFirst.Y + GlobalVariables.MapDrawingHeight, GlobalVariables.MapDrawingWidth, YDifference);
            }

            MapManagement.DrawPixelsOnMap(new List<Rectangle> { toDraw });
            ModEditor.UpdateMap();
        }

        public static void HandleMoveButton(object sender, EventArgs e)
        {
            Button senderbutton = (Button)sender;
            __HandleMoveButton(senderbutton.Name);                    
        }

        public static void HandleKeyUp(object sender, KeyEventArgs e)
        {
            GlobalVariables.PressedKeys.Remove(e.KeyCode.GetHashCode());
        }

        public static void HandleButton(object sender, KeyEventArgs e)
        {
            GlobalVariables.PressedKeys.Add(e.KeyCode.GetHashCode());
            if (!ModEditor.boxes.Any(x => x.DroppedDown) && !ModEditor.textboxes.Any(x=>x.Focused))
            {
                    switch (e.KeyCode)
                    {
                        case Keys.A:
                            __HandleMoveButton("LeftButton");
                            break;
                        case Keys.D:
                            __HandleMoveButton("RightButton");
                            break;
                        case Keys.W:
                            __HandleMoveButton("UpButton");
                            break;
                        case Keys.S:
                            __HandleMoveButton("DownButton");
                            break;
                        case Keys.Q:
                            break;
                    }                           
            }
            else if(!ModEditor.boxes.Any(x => x.DroppedDown) && ModEditor.textboxes.Any(x => x.Focused))
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                    case Keys.Escape:
                        GlobalVariables.MainForm.ActiveControl = null;             
                        e.SuppressKeyPress = true;
                        break;
                }
            }
        }


    }
}
