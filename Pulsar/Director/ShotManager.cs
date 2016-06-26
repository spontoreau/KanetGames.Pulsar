/* License
 * 
 * The MIT License (MIT)
 *
 * Copyright (c) 2013, Kanet Games (contact@kanetgames.com / www.kanetgames.com)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using Pulsar.Framework;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pulsar.Director
{
    /// <summary>
    /// Manage the shot in the engine
    /// </summary>
    public sealed class ShotManager : DrawableGameComponent
    {
        /// <summary>
        /// Last view use to draw a Shot.
        /// </summary>
        private View LastView { get; set; }

        /// <summary>
        /// List of shots
        /// </summary>
        internal List<Shot> Shots { get; set; }

        /// <summary>
        /// Define the Texture to use for the Dialog Background drawing
        /// </summary>
        private Texture Blank { get; set; }

        /// <summary>
        /// Get the backgound color to draw when a Dialog Shot is Active
        /// </summary>
        public SFML.Graphics.Color DialogBackgroundColor { get; private set; }

        /// <summary>
        /// Create a new instance of ShotManager
        /// </summary>
        /// <param name="game">RenderableGame</param>
        public ShotManager(RenderableGame game)
            : base(game)
        {
            this.Shots = new List<Shot>();
        }

        /// <summary>
        /// Load content
        /// </summary>
        protected override void LoadContent()
        {
            Image image = new Image(1u, 1u);
            image.SetPixel(1u, 1u, SFML.Graphics.Color.White);
            this.Blank = new Texture(image);
        }

        /// <summary>
        /// Unload content
        /// </summary>
        protected override void UnloadContent()
        {
            foreach (Shot shot in this.Shots)
                shot.UnloadContent();

            this.Blank.Dispose();
        }

        /// <summary>
        /// Update the ScreenManager
        /// </summary>
        /// <param name="gameTime">GameTime object</param>
        public override void Update(GameTime gameTime)
        {
            //first check if there is Dialog Shots into the ScreenManager. You can have only one dialog in the shot manager (to prevent abuse and lags !!!)
            int dialogCount = (from s in this.Shots where s.IsDialogShot select s).Count();

            if(dialogCount > 1)
            {
                throw new DirectorEngineException(typeof(ShotManager).FullName, Pulsar.Resources.Exceptions.Director.OnlyOneDialogShotCapable);
            }
            else
            {
                Shot dialogWithHandleInput = (dialogCount == 1) ? (from s in this.Shots where s.IsDialogShot select s).Single() : null;

                foreach(Shot shot in this.Shots)
                {
                    if (shot.State == ShotStateEnum.Active) //only check if a shot need to handle input when his state is active
                    {
                        if (dialogWithHandleInput == null)//if there is no dialog handle the input for all shots
                        {
                            shot.HandleInput();
                        }
                        else
                        {
                            if(shot.Equals(dialogWithHandleInput))//if there is a dialog and the current shot is this dialog, only Handle Input for it !
                            {
                                shot.HandleInput();
                            }
                        }
                    }

                    shot.Update(gameTime);
                }
          
                List<Shot> shotToRemove = (from s in Shots where s.State == ShotStateEnum.TransitionOff && s.TransitionPosition <= 0 select s).ToList(); //we need to remove the Shot that have a transition off state and Transition position to zero

                foreach (Shot shot in shotToRemove)
                {
                    shot.UnloadContent();
                    this.Shots.Remove(shot);
                }
            }
        }

        /// <summary>
        /// Update a shot in the ShotManager
        /// </summary>
        /// <param name="shot">The shot to update</param>
        private void Update(Shot shot, GameTime gameTime)
        {
            float delta = 
                (shot.TransitionTime == TimeSpan.Zero) ? 
                1 : 
                (float)(gameTime.ElapsedGameTime.TotalMilliseconds / shot.TransitionTime.TotalMilliseconds);

            shot.TransitionPosition += delta * ((shot.IsLeaving) ? -1 : 1);

            if (shot.IsLeaving)
            {
                shot.State = ShotStateEnum.TransitionOff;
            }
            else
            {
                if (shot.TransitionPosition >= 1)
                {
                    shot.TransitionPosition = MathHelper.Clamp(shot.TransitionPosition, 0, 1);
                    shot.State = ShotStateEnum.Active;
                }
                else
                {
                    shot.State = ShotStateEnum.TransitionOn;
                }
            }
        }        

        /// <summary>
        /// Draw all the Shot into the ShotManager.
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach(Shot shot in Shots)
            {
                if(shot.State != ShotStateEnum.Hidden)
                {
                    //first of all we have to check the view of the shot and if it isn't the same as the last view, set this view to the render window.
                    if(!shot.View.Equals(this.LastView))
                        WindowContext.Window.SetView(shot.View);

                    //If shot is a dialog, draw the dialog background before draw the shot
                    if (shot.IsDialogShot)
                        DrawDialogBackground(shot.DialogBackgroundAlpha, shot.View);

                    shot.Draw(gameTime);
                }
            }
        }

        /// <summary>
        /// Draw the Dialog background
        /// </summary>
        /// <param name="alpha">Opacity of the background in color A byte</param>
        /// <param name="view">View to apply in the draw operation</param>
        public void DrawDialogBackground(byte alpha, View view)
        {
            Sprite sprite = new Sprite(this.Blank, new IntRect((int)view.Viewport.Left, (int)view.Viewport.Top, (int)view.Viewport.Width, (int)view.Viewport.Height));
            SFML.Graphics.Color color = this.DialogBackgroundColor;
            color.A = alpha;
            sprite.Color = color;
            WindowContext.Window.Draw(sprite);
        }
    }
}
