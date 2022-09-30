using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public class MonoGameControl : MonoGame.Forms.Controls.InvalidationControl
    {
        public void UpdateTexture()
        {
            try
            {
                GlobalVariables.MainTexture = GetTexture(this.GraphicsDevice, GlobalVariables.DrawingMain.source);
            }
            catch { }
        }

        protected override void Initialize()
        {
            base.Initialize();
            UpdateTexture();
        }

        protected override void Draw()
        {
            //base.Draw();
            Editor.spriteBatch.Begin();         
            Editor.spriteBatch.Draw(GlobalVariables.MainTexture, position:new Vector2(0,0), sourceRectangle:new Rectangle(GlobalVariables.CameraPosition.X,GlobalVariables.CameraPosition.Y, GlobalVariables.MapDrawingWidth, GlobalVariables.MapDrawingHeight));
            Editor.spriteBatch.End();
        }

        public static unsafe Texture2D GetTexture(GraphicsDevice dev, System.Drawing.Bitmap bmp)
        {
            int[] imgData = new int[bmp.Width * bmp.Height];
            Texture2D texture = new Texture2D(dev, bmp.Width, bmp.Height);

            unsafe
            {
                // lock bitmap
                System.Drawing.Imaging.BitmapData origdata =
                    bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);

                uint* byteData = (uint*)origdata.Scan0;

                // Switch bgra -> rgba
                for (int i = 0; i < imgData.Length; i++)
                {
                    byteData[i] = (byteData[i] & 0x000000ff) << 16 | (byteData[i] & 0x0000FF00) | (byteData[i] & 0x00FF0000) >> 16 | (byteData[i] & 0xFF000000);
                }

                // copy data
                System.Runtime.InteropServices.Marshal.Copy(origdata.Scan0, imgData, 0, bmp.Width * bmp.Height);

                byteData = null;

                // unlock bitmap
                bmp.UnlockBits(origdata);
            }

            texture.SetData(imgData);

            return texture;
        }
    }
}
