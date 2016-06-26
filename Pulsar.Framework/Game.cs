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

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Pulsar.Framework
{
    /// <summary>
    /// Provides basic game logic. 
    /// </summary>
    public class Game : IDisposable
    {
        /// <summary>
        /// The Stopwatch for update the GameTime
        /// </summary>
        protected Stopwatch Watch { get; set; }
        
        /// <summary>
        /// Object use for lock operation
        /// </summary>
        protected object _syncRoot = new object();  
    
        /// <summary>
        /// The game loop time
        /// </summary>
        public GameTime GameTime { get; set; }

        /// <summary>
        /// Get or define the collection of IGameComponent which need to be initialize.
        /// </summary>
        protected Collection<IGameComponent> InitWaitingComponents { get; set; }

        /// <summary>
        /// Get or define the collection of IUpdateable which need to be update.
        /// </summary>
        protected Collection<IUpdateable> UpdateableComponents { get; set; }

        /// <summary>
        /// True if the game is currently running.
        /// </summary>
        protected bool IsRunning { get; set; }

        /// <summary>
        /// True if the game is curretly exiting
        /// </summary>
        protected bool IsExiting { get; set; }

        /// <summary>
        /// Gets the collection of GameComponents owned by the game.
        /// </summary>
        public GameComponentCollection Components { get; private set; }

        /// <summary>
        /// Initializes a new instance of this class, which provides basic game logic and a game loop.
        /// </summary>
        public Game()
        {
            Components = new GameComponentCollection();
            Components.ComponentAdded += new EventHandler<GameComponentCollectionEventArgs>(GameComponentAdded);
            Components.ComponentRemoved += new EventHandler<GameComponentCollectionEventArgs>(GameComponentRemoved);

            InitWaitingComponents = new Collection<IGameComponent>();
            UpdateableComponents = new Collection<IUpdateable>();

            GameTime = new GameTime();
        }

        /// <summary>
        /// Call this method to initialize the game, begin running the game loop, and start processing events for the game. 
        /// </summary>
        public void Run()
        {
            if (this.IsRunning)
                throw new InvalidOperationException("Game is running");

            try
            {
                this.IsExiting = false;
                this.BeginRun();
                this.BeginInitialize();
                this.Initialize();
                this.EndInitialize();
                this.LoadContent();
                this.IsRunning = true;                
                while (this.IsRunning && !this.IsExiting)
                {
                    Tick();
                }
                this.UnloadContent(); 
                this.EndRun();                             
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Exits the game.
        /// </summary>
        public void Exit()
        {
            this.IsExiting = true;
        }

        /// <summary>
        /// Allows a Game to attempt to free resources and perform other cleanup operations before garbage collection reclaims the Game. 
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Updates the game's clock and calls Update and Draw.
        /// </summary>
        protected virtual void Tick()
        {
            if (this.IsExiting)
                return;

            BeginUpdate();
            Update(this.GameTime);
            EndUpdate();
        }

        /// <summary>
        /// Called when the game has determined that game logic needs to be processed.
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Update.</param>
        protected virtual void Update(GameTime gameTime)
        {
            foreach (IUpdateable updateable in this.UpdateableComponents)
            {
                if (updateable.Enabled)
                    updateable.Update(gameTime);
            }
        }

        /// <summary>
        /// Allows a Game to attempt to free resources and perform other cleanup operations before garbage collection reclaims the Game. 
        /// </summary>
        ~Game()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isDisposing"></param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                lock (this._syncRoot)
                {
                    //Clear the GameComponant collection will raise an event for each component remove.
                    this.Components.Clear();
                }
            }
        }

        /// <summary>
        /// Update the GameTime
        /// </summary>
        protected void UpdateGameTime()
        {
            this.GameTime.ElapsedGameTime = this.Watch.Elapsed;
            this.GameTime.TotalGameTime += this.Watch.Elapsed;
        }

        /// <summary>
        /// Called after all components are initialized but before the first update in the game loop. 
        /// </summary>
        protected virtual void BeginRun()
        {

        }

        /// <summary>
        /// Called after the game loop has stopped running before exiting.
        /// </summary>
        protected virtual void EndRun()
        {
            Environment.Exit(1);//Allways exit the application
        }

        /// <summary>
        /// Starts the update. This method is followed by calls to Update and EndUpdate. 
        /// </summary>
        protected virtual void BeginUpdate()
        {
            this.Watch.Restart();
        }

        /// <summary>
        /// Ends the update. This method is preceeded by calls to Update and EndUpdate. 
        /// </summary>
        protected virtual void EndUpdate()
        {
            UpdateGameTime();
        }

        /// <summary>
        /// Starts the initialize of the game. This method is followed by calls to Initialize and EndInitialize. 
        /// </summary>
        protected virtual void BeginInitialize()
        {

        }

        /// <summary>
        /// End the initialize of the game. This method is preceeded calls to Initialize and BeginInitialize. 
        /// </summary>
        protected virtual void EndInitialize()
        {

        }

        /// <summary>
        /// Called after the Game and GraphicsDevice are created, but before LoadContent. 
        /// </summary>
        protected virtual void Initialize()
        {
            this.Watch = new Stopwatch();

            foreach (IGameComponent component in this.InitWaitingComponents)
                component.Initialize();

            InitWaitingComponents.Clear();
        }

        /// <summary>
        /// Called when graphics resources need to be loaded.
        /// </summary>
        protected virtual void LoadContent()
        {

        }

        /// <summary>
        /// Called when graphics resources need to be unloaded. Override this method to unload any game-specific graphics resources.
        /// </summary>
        protected virtual void UnloadContent()
        {

        }

        /// <summary>
        /// Handle the ComponentAdded event raise by the GameComponentCollection
        /// </summary>
        /// <param name="sender">The GameComponent collection</param>
        /// <param name="eventArgs">Arguments of the ComponentAdded event.</param>
        protected virtual void GameComponentAdded(object sender, GameComponentCollectionEventArgs eventArgs)
        {
            if (this.IsRunning)
                eventArgs.Component.Initialize();
            else //if the game is not running stack in the initialize waiting list
                this.InitWaitingComponents.Add(eventArgs.Component);

            if (eventArgs.Component is IUpdateable)
            {
                IUpdateable updateable = (IUpdateable)eventArgs.Component;
                InsertUpdateable(updateable);
                updateable.UpdateOrderChanged += new EventHandler<EventArgs>(UpdateOrderChanged);
            }
        }

        /// <summary>
        /// Handle the ComponentRemoved event raise by the GameComponentCollection
        /// </summary>
        /// <param name="sender">The GameComponent collection</param>
        /// <param name="eventArgs">Arguments of the ComponentRemoved event.</param>
        protected virtual void GameComponentRemoved(object sender, GameComponentCollectionEventArgs eventArgs)
        {
            IGameComponent component = eventArgs.Component;
            if (!this.IsRunning) //if the game is not running, initialize and loadcontent is'nt make, so remove the component from the InitWaitingGameComponent collection
                this.InitWaitingComponents.Remove(component);

            if (component is IUpdateable)
            {
                IUpdateable updateable = (IUpdateable)component;
                this.UpdateableComponents.Remove(updateable);
                updateable.UpdateOrderChanged -= new EventHandler<EventArgs>(UpdateOrderChanged);                
            }
        }

        /// <summary>
        /// Handle the UpdateOrderChanged raise by a IUpdateable
        /// </summary>
        /// <param name="sender">The IUpdateable</param>
        /// <param name="eventArgs">Arguments of the UpdateOrderChanged event.</param>
        protected void UpdateOrderChanged(object sender, EventArgs eventArgs)
        {
            IUpdateable updateable = (IUpdateable)sender;
            this.UpdateableComponents.Remove(updateable);
            InsertUpdateable(updateable);            
        }

        /// <summary>
        /// Insert updateable at the right index in the UpdateableComponents List.
        /// </summary>
        /// <param name="updateable">The updateable component to insert</param>
        protected void InsertUpdateable(IUpdateable updateable)
        {
            //find the follower and insert the updateable at the index of the follower
            IUpdateable follower = (from u in UpdateableComponents where u.UpdateOrder >= updateable.UpdateOrder select u).FirstOrDefault();
            //if follower is null, updateable is the last in the collection, otherwise take the index of the follower
            int index = (follower == null) ? this.UpdateableComponents.Count : this.UpdateableComponents.IndexOf(follower); 
            this.UpdateableComponents.Insert(index, updateable);
        }
    }
}
