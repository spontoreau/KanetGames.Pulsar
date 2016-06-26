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

using Pulsar.Audio;
using Pulsar.Content;
using Pulsar.Core.Culture;
using Pulsar.Director;
using Pulsar.Framework;
using Pulsar.Graphics.Gui.Containers;
using Pulsar.Graphics.Gui.Controls;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pulsar.Graphics.Gui
{
    public class GuiShot : Shot
    {
        /// <summary>
        /// Last press position
        /// </summary>
        private Vector _pressPosition;

        /// <summary>
        /// Audio manager
        /// </summary>
        private AudioManager _audioManager;

        /// <summary>
        /// List of containers in the shot
        /// </summary>
        private List<Container> _containers;

        /// <summary>
        /// List of translation corresponding to the shot
        /// </summary>
        private List<Translation> _translations;

        /// <summary>
        /// List of containers in the shot
        /// </summary>
        public List<Container> Containers
        {
            get { return _containers; }
        }

        /// <summary>
        /// Key to translate the Shot controls
        /// </summary>
        public string TranslationKey { get; private set; }

        /// <summary>
        /// Create a new instance of GuiScreen and keep in mind the culture
        /// </summary>
        /// <param name="translationKey">Translation key</param>
        public GuiShot(string translationKey, CultureManager cultureManager, AudioManager audioManager, TimeSpan transitionTime)
            : base(transitionTime)
        {
            this._audioManager = audioManager;
            this.TranslationKey = translationKey;
            this._translations = cultureManager.GetFunctionTranslation(this.TranslationKey);
            cultureManager.LanguageChanged += LanguageChanged;
            WindowContext.Window.MouseButtonReleased += MouseButtonReleased;
            WindowContext.Window.MouseMoved += MouseMoved;
            WindowContext.Window.TextEntered += TextEntered;
            WindowContext.Window.MouseButtonPressed += MouseButtonPressed;
        }

        /// <summary>
        /// Handle mouse press
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            //Persist coordinates only if it left mouse button
            if(e.Button == Mouse.Button.Left)
            {
                //if null it's the first time we press, so persist this time 
                //only because we need to compare this position to the future 
                //release position to know if it's a click on a gui control :)
                if (this._pressPosition == null)
                {
                    this._pressPosition = new Vector(e.X, e.Y);
                }
            }
        }

        /// <summary>
        /// Handle text entered
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void TextEntered(object sender, SFML.Window.TextEventArgs e)
        {
            if (GuiManager.CurrentFocus is TextBox)
            {
                TextBox tControl = (TextBox)GuiManager.CurrentFocus;
                tControl.Add(e.Unicode);
            }
        }

        /// <summary>
        /// Handle mouse moved
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void MouseMoved(object sender, SFML.Window.MouseMoveEventArgs e)
        {
            Vector position = new Vector(e.X, e.Y);
            foreach (Container container in this._containers)
            {
                if (container.Bounds.Contains(position))
                {
                    foreach (Control control in container.Controls)
                    {
                        if (control.Bounds.Contains(position))
                        {
                            if (!control._isMouseOver)//if control wasn't in mouse over state it's an OnEnter.
                            {
                                if (control is IAudioable)//hey we enter, play my sound pls !
                                {
                                    IAudioable audio = (IAudioable)control;

                                    if (!string.IsNullOrEmpty(audio.OverSoundKey))
                                        this._audioManager.PlaySound(audio.OverSoundKey);

                                    GuiManager.OverControl = control;
                                    control._isMouseOver = true;
                                    control.OnEnter();                                    
                                }
                            }
                            else//no change for me it's over !
                            {
                                control.OnOver();
                            }
                        }
                        else
                        {
                            if (control._isMouseOver)
                            {
                                if (GuiManager.OverControl == control)//hey it's me and i leave so put out my pointer pls !
                                    GuiManager.OverControl = null;

                                control._isMouseOver = false;
                                control.OnLeave();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handle mouse released
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void MouseButtonReleased(object sender, SFML.Window.MouseButtonEventArgs e)
        {
            //nice it's the right button ! 
            //Or mabe the right left button ^^ 
            //(in fact left is the right button to check !)
            //
            //OverControl can't be null, we can click on null !
            if (e.Button == Mouse.Button.Left && GuiManager.OverControl != null)
            {
                //to perform a click, last press position must be in the control bounds !!
                if (GuiManager.OverControl.Bounds.Contains(this._pressPosition))
                {
                    //hey we click play my sound pls !
                    if (GuiManager.OverControl is IAudioable)
                    {
                        IAudioable audio = (IAudioable)GuiManager.OverControl;
                        this._audioManager.PlaySound(audio.ClickSoundKey);
                    }

                    GuiManager.OverControl.OnClick(e);
                }

                //ok release is finish so switch last press position to null :)
                this._pressPosition = null;
            }
        }

        /// <summary>
        /// Handle language changed
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void LanguageChanged(object sender, EventArgs e)
        {
            CultureManager cultureManager = (CultureManager)sender;
            this._translations = cultureManager.GetFunctionTranslation(this.TranslationKey);

            foreach (Container container in this._containers)
            {
                ApplyTranslation(container);
            }
        }

        /// <summary>
        /// Apply translation to a container
        /// </summary>
        /// <param name="container"></param>
        /// <param name="tranlations"></param>
        private void ApplyTranslation(Container container, List<Translation> tranlations = null)
        {
            if (tranlations != null)
            {
                foreach (Control c in container.Controls)
                {
                    if (c is ITranslatable)
                    {
                        ITranslatable tControl = (ITranslatable)c;

                        if (!string.IsNullOrEmpty(tControl.TranslateKey))
                        {
                            if (c is ITextRenderable)
                            {
                                ITextRenderable textControl = (ITextRenderable)c;
                                textControl.Text = (from t in tranlations where t.Key == tControl.TranslateKey select t).Single().Value;
                            }
                        }
                    }
                }
            }
            else
            {
                throw new GuiEngineException(typeof(GuiShot).Name, "Obligé d'avoir un liste de traduction !!!!");//todo add in ressources
            }
        }

        /// <summary>
        /// Add container to the shot
        /// </summary>
        /// <param name="container">Container to add</param>
        public void Add(Container container)
        {
            ApplyTranslation(container, this._translations);
        }

        /// <summary>
        /// Update the Shot
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Update(GameTime gameTime)
        {
            foreach (Container c in this._containers)
                c.Update(gameTime);
        }

        /// <summary>
        /// Load graphics content for the shot.
        /// <param name="contentManager">The content manager</param>
        /// </summary>
        public override void LoadContent(ContentManager contentManager)
        {
            
        }

        /// <summary>
        /// Unload content for the shot.
        /// </summary>
        public override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Draw the Shot
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            //TODO render
        }

        public override void HandleInput()
        {
            //TODO handle input
            throw new NotImplementedException();
        }
    }
}
