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
using System.Collections;
using System.Collections.Generic;

namespace Pulsar.Graphics.Gui.Controls
{
    /// <summary>
    /// Abstract control for deal with list of item
    /// </summary>
    public abstract class ListControl : Control
    {
        private List<ListItem> _items = new List<ListItem>();
        private int _index;

        /// <summary>
        /// Event raised when the LinkLabl is clicked.
        /// </summary>
        public event EventHandler SelectIndexChanged;

        /// <summary>
        /// Method for raising the select index changed event.
        /// </summary>
        protected internal virtual void OnSelectIndexChanged()
        {
            if (SelectIndexChanged != null)
                SelectIndexChanged(this, new EventArgs());
        }

        /// <summary>
        /// Current index selected
        /// </summary>
        public int SelectedIndex
        {
            get { return _index; }
            protected set
            {
                this._index = value;
                OnSelectIndexChanged();
            }
        }

        /// <summary>
        /// Current selected item
        /// </summary>
        public ListItem SelectedItem
        {
            get
            {
                return this._items[this._index];
            }
        }

        /// <summary>
        /// Current selected value
        /// </summary>
        public object SelectedValue
        {
            get
            {
                return this._items[this._index].Value;
            }
        }

        /// <summary>
        /// DataSource object (must implement IList)
        /// </summary>
        public object DataSource { get; set; }

        /// <summary>
        /// Object member to set in a ListItem text property
        /// </summary>
        public string DisplayMember { get; set; }

        /// <summary>
        /// Object member to set in a ListItem value property
        /// </summary>
        public string ValueMember { get; set; }

        /// <summary>
        /// ListItems
        /// </summary>
        public List<ListItem> Items
        {
            get { return _items; }
        }

        /// <summary>
        /// Create a new instance of a ListControl
        /// </summary>
        public ListControl()
            : base()
        {

        }

        /// <summary>
        /// Data bound the Datasource set in the control
        /// </summary>
        public void DataBind()
        {
            this._items.Clear();

            if (DataSource != null || DataSource is IList)
            {
                IList list = (IList)DataSource;

                foreach (object o in list)
                {
                    string text = (string.IsNullOrEmpty(DisplayMember)) ? o.ToString() : o.GetType().GetProperty(DisplayMember).GetValue(o, null).ToString();
                    object value = (string.IsNullOrEmpty(ValueMember)) ? o : o.GetType().GetProperty(ValueMember).GetValue(o, null).ToString();

                    this._items.Add(new ListItem(text, value));
                }
            }

            SelectedIndex = 0;
        }
    }
}
