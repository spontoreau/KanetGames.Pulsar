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

using Pulsar.Director.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pulsar.Director
{
    /// <summary>
    /// Scene Manager.
    /// </summary>
    public class SceneManager : DrawableGameComponent
    {
        /// <summary>
        /// The synchronize object
        /// </summary>
        private readonly object _syncRoot = new object();

        /// <summary>
        /// List of shot loaded
        /// </summary>
        private List<Shot> ShotLoaded { get; set; }
        
        /// <summary>
        /// True if the SceneManage is currently loading a Scene
        /// </summary>
        public bool IsLoading { get; private set; }

        /// <summary>
        /// Event Raise when the Scene Manager finish the load of a scene.
        /// </summary>
        public event EventHandler SceneLoaded;

        /// <summary>
        /// Event raise when the SceneManager launch a load of a scene.
        /// </summary>
        public event EventHandler<LoadStartedEventArgs> LoadStarted;

        /// <summary>
        /// Event raise when the SceneManager finish to load one shot and made a progression in the complete loading process.
        /// </summary>
        public event EventHandler<LoadProgressEventArgs> LoadProgress;

        /// <summary>
        /// Get or set the Dictionary of Scenes.
        /// </summary>
        public Dictionary<string, Scene> Scenes { get; set; }

        /// <summary>
        /// Current Scene in the Scene Manager.
        /// </summary>
        public Scene Current { get; private set; }

        /// <summary>
        /// The Shot Manager.
        /// </summary>
        public ShotManager ShotManager { get; private set; }

        /// <summary>
        /// Create a new instance of SceneManager.
        /// </summary>
        /// <param name="game">The renderable game.</param>
        /// <param name="shotManager">The shot manager.</param>
        public SceneManager(RenderableGame game, ShotManager shotManager)
            : base(game)
        {
            this.ShotManager = shotManager;
            this.Scenes = new Dictionary<string, Scene>();
            this.ShotLoaded = new List<Shot>();
        }

        /// <summary>
        /// Change the Scene in the SceneManager.
        /// </summary>
        /// <param name="sceneKey">Key of the scene</param>
        public void Change(string sceneKey)
        {
            if (this.IsLoading)
            {
                if (Scenes.ContainsKey(sceneKey))
                {
                    Scene scene;

                    if (this.Scenes.TryGetValue(sceneKey, out scene))
                    {
                        IsLoading = true;

                        foreach (Shot shot in this.ShotManager.Shots)
                            shot.UnloadContent();

                        this.ShotManager.Shots.Clear();
                        OnLoadStarted(scene.ShotTypes.Count, scene.IsHeavyLoad);
                        List<Shot> shots = scene.GetShots();
                        this.Current = scene;
                        Task.Factory.StartNew(() => Parallel.ForEach<Shot>(shots, shot => LoadAsync(shot)));
                    }
                    else
                    {
                        throw new DirectorEngineException(typeof(SceneManager).FullName, Pulsar.Resources.Exceptions.Director.SceneValueNotDefine);
                    }
                }
                else
                {
                    throw new DirectorEngineException(typeof(SceneManager).FullName, Pulsar.Resources.Exceptions.Director.SceneKeyNotDefine);
                }
            }
            else
            {
                throw new DirectorEngineException(typeof(SceneManager).FullName, Pulsar.Resources.Exceptions.Director.SceneManagerLoading);
            }
        }

        /// <summary>
        /// Load a shot in a async process
        /// </summary>
        /// <param name="shot"></param>
        private void LoadAsync(Shot shot)
        {
            lock (this._syncRoot)
            {
                RenderableGame game = (RenderableGame)this.Game;
                shot.LoadContent(game.Content);
                this.ShotLoaded.Add(shot);
                OnLoadProgress(this.ShotLoaded.Count);

                if (this.ShotLoaded.Count == this.Current.ShotTypes.Count)
                    OnSceneLoaded();
            }
        }

        /// <summary>
        /// Raise the LoadStarted Event.
        /// </summary>
        /// <param name="shotNumber">Number of shot to load.</param>
        /// <param name="isHeavyLoad">True if the Scene is heavy to load.</param>
        private void OnLoadStarted(int shotNumber, bool isHeavyLoad)
        {
            EventHandler<LoadStartedEventArgs> tmp = LoadStarted;

            if (tmp != null)
                LoadStarted(this, new LoadStartedEventArgs(shotNumber, isHeavyLoad));
        }

        /// <summary>
        /// Raise the LoadProgress event.
        /// </summary>
        /// <param name="shotLoaded">Number of shot loaded.</param>
        private void OnLoadProgress(int shotLoaded)
        {
            EventHandler<LoadProgressEventArgs> tmp = LoadProgress;

            if(tmp != null)
                LoadProgress(this, new LoadProgressEventArgs(shotLoaded));
        }

        /// <summary>
        /// Raise the Scene Loaded event.
        /// </summary>
        private void OnSceneLoaded()
        {
            //All shots are loaded, we have to add them to the ShotManager in the order define in the Type list of the Scene object
            foreach (Type type in this.Current.ShotTypes)
            {
                Shot shot = (from s in this.ShotLoaded where s.GetType().Equals(type) select s).Single();
                this.ShotManager.Shots.Add(shot);
            }
            this.ShotLoaded.Clear();
            this.IsLoading = false;

            EventHandler tmp = SceneLoaded;

            if (tmp != null)
                SceneLoaded(this, EventArgs.Empty);
        }
    }
}
