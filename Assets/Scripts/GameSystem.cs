/*
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 2 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using UnityEngine;
using static UnityEngine.GameObject;

using static GameDev.Env;
using static GameDev.Utils;

namespace GameDev {
    /// <summary>
    /// game system
    /// </summary>
    /// <author>h.adachi (STUDIO MeowToon)</author>
    public class GameSystem : MonoBehaviour {
#nullable enable

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // Properties [noun, adjectives] 

        /// <summary>
        /// game mode.
        /// </summary>
        public string mode { get => Status.mode; set => Status.mode = value; }

        /// <summary>
        /// whether came back home.
        /// </summary>
        public bool home { get; set; }

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // public Events [verb, verb phrase]

        public event Action? OnPauseOn;

        public event Action? OnPauseOff;

        public event Action? OnStartLevel;

        public event Action? OnCameBackHome;

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // public Methods [verb]

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // update Methods

        // Awake is called when the script instance is being loaded.
        void Awake() {
            Application.targetFrameRate = FPS;

            if (HasLevel()) {
                // get level.
                Level level = Find(name: LEVEL_TYPE).Get<Level>();

                /// <summary>
                /// level pause on.
                /// </summary>
                level.OnPauseOn += () => { OnPauseOn?.Invoke(); };

                /// <summary>
                /// level pause off.
                /// </summary>
                level.OnPauseOff += () => { OnPauseOff?.Invoke(); };

                /// <summary>
                /// level start.
                /// </summary>
                level.OnStart += () => { OnStartLevel?.Invoke(); };
            }

            if (HasHome()) {
                // get home.
                Home home = Find(name: HOME_TYPE).Get<Home>();

                /// <summary>
                /// home came back.
                /// </summary>
                this.home = false;
                home.OnCameBack += () => {
                    this.home = true;
                    OnCameBackHome?.Invoke(); 
                };
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // inner Classes

        #region Status

        static class Status {
#nullable enable

            ///////////////////////////////////////////////////////////////////////////////////////////
            // static Fields [nouns, noun phrases]

            static string _mode;

            ///////////////////////////////////////////////////////////////////////////////////////////
            // static Constructor

            static Status() {
                _mode = MODE_NORMAL;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////
            // public static Properties [noun, noun phrase, adjective]

            public static string mode {
                get => _mode; set => _mode = value;
            }
        }

        #endregion
    }
}