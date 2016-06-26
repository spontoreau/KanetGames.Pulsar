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

using Pulsar.Content;
using Pulsar.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pulsar.Core.Culture
{
    /// <summary>
    /// Manage culture
    /// </summary>
    public sealed class CultureManager
    {
        /// <summary>
        /// Private reference to ContentManager
        /// </summary>
        private ContentManager _content;

        /// <summary>
        /// Event handle language changed
        /// </summary>
        public event EventHandler LanguageChanged;
        
        /// <summary>
        /// Get the file extension for Language files
        /// </summary>
        public string LanguageFileExtension { get; private set; }

        /// <summary>
        /// True if culture manager is initialize
        /// </summary>
        public bool IsInit { get; private set; }

        /// <summary>
        /// Current language
        /// </summary>
        public Language Current { get; private set; }

        /// <summary>
        /// Create a new instance of CultureManager. By default the file format to load is Xml.
        /// </summary>
        public CultureManager()
            : this(Pulsar.Toolkit.Resources.Format.Common.XmlFileExtension)
        {

        }

        /// <summary>
        /// Create a new instance of CultureManager, and define the file format to load (.xml for XmlFile, otherwise for binary files)
        /// </summary>
        /// <param name="fileExtension">Define the fileExtension for language</param>
        public CultureManager(string fileExtension)
        {
            this.LanguageFileExtension = fileExtension;
        }

        /// <summary>
        /// Intialize the culture manager
        /// </summary>
        public void Initialize(ConfigurationManager manager, ContentManager content)
        {
            if (!this.IsInit)
            {
                this._content = content;

                if (manager.Game.Culture != null) //game must have a culture. Core can deal with multiple culture, but one have to be a default in the engine
                {
                    try
                    {
                        ChangeLanguage(manager.Game.Culture);
                        this.IsInit = true;
                    }
                    catch (Exception ex)
                    {
                        throw new CoreEngineException(typeof(ConfigurationManager).FullName, string.Format(Pulsar.Resources.Exceptions.Common.InitializeFailed, typeof(CultureManager).FullName), ex);
                    }
                }
                else
                {
                    throw new CoreEngineException(typeof(ConfigurationManager).FullName, Pulsar.Resources.Exceptions.CultureManager.DefaultCultureMissing);
                }
            }
            else
            {
                throw new CoreEngineException(typeof(ConfigurationManager).FullName, string.Format(Pulsar.Resources.Exceptions.Common.InitializeHasDone, typeof(CultureManager).FullName));
            }
        }

        /// <summary>
        /// Get a list of a translation for a function translation key
        /// </summary>
        /// <param name="functionKey">Function translation key</param>
        /// <returns>List of translation</returns>
        public List<Translation> GetFunctionTranslation(string functionKey)
        {
            FunctionTranslation function = (from s in Current.Functions where s.Key == functionKey select s).FirstOrDefault();

            if (function == null)
                throw new CoreEngineException(typeof(CultureManager).FullName, string.Format(Pulsar.Resources.Exceptions.CultureManager.FunctionNotFind, functionKey));

            return function.Translations;
        }

        /// <summary>
        /// Get a translation for a translation key
        /// </summary>
        /// <param name="translationKey"></param>
        /// <returns></returns>
        public Translation GetTranslation(string translationKey)
        {
            Translation translation = null;
            foreach (FunctionTranslation function in Current.Functions)
            {
                translation = (from l in function.Translations where l.Key == translationKey select l).FirstOrDefault();

                if (translation != null)
                    break;
            }

            if (translation == null)
                throw new CoreEngineException(typeof(CultureManager).FullName, string.Format(Pulsar.Resources.Exceptions.CultureManager.TranslationNotFind, translationKey));

            return translation;
        }

        /// <summary>
        /// Change the current language
        /// </summary>
        /// <param name="code">Key of a language</param>
        public void ChangeLanguage(string code)
        {
            //Language are load on demande. There's no caching system for that because this action is not use all the time.
            Language language = this._content.Load<Language>(code + this.LanguageFileExtension);

            if (!language.Equals(this.Current))
            {
                if(this.Current != null)
                    this.Current.Dispose();

                this.Current = language;
                OnLanguageChanged();
            }
        }


        /// <summary>
        /// Raise LanguageChanged event
        /// </summary>
        private void OnLanguageChanged()
        {
            EventHandler tmp = LanguageChanged;

            if (tmp != null)
                LanguageChanged(new object(), EventArgs.Empty);
        }
    }
}