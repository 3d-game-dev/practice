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
using UnityEngine.UI;
using static UnityEngine.GameObject;
using static UnityEngine.SceneManagement.SceneManager;
using UniRx;
using UniRx.Triggers;

using static GameDev.Env;
using static GameDev.Utils;

namespace GameDev {
    /// <summary>
    /// status system
    /// </summary>
    /// <author>h.adachi (STUDIO MeowToon)</author>
    public class NoticeSystem : MonoBehaviour {
#nullable enable

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // References [bool => is+adjective, has+past participle, can+verb prototype, triad verb]

        [SerializeField] Text _message_text, _targets_text, _points_text, _mode_text;

        /// <remarks>
        /// for development.
        /// </remarks>
        [SerializeField] Text _energy_text, _power_text, _fps_text;

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // Fields [noun, adjectives] 

        GameSystem _game_system;

        int _frame_count;

        float _elapsed_time;

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // update Methods

        // Awake is called when the script instance is being loaded.
        void Awake() {
            _game_system = Find(name: GAME_SYSTEM).Get<GameSystem>();

            /// <summary>
            /// game system pause on.
            /// </summary>
            _game_system.OnPauseOn += () => { if (!_game_system.home) { _message_text.text = MESSAGE_GAME_PAUSE; }};

            /// <summary>
            /// game system pause off.
            /// </summary>
            _game_system.OnPauseOff += () => { _message_text.text = string.Empty; };

            /// <summary>
            /// game system came back.
            /// </summary>
            _game_system.OnCameBackHome += () => { _message_text.text = MESSAGE_LEVEL_CLEAR; };

            /// <summary>
            /// game system start a level.
            /// </summary>
            _game_system.OnStartLevel += () => {
                switch (GetActiveScene().name) {
                    case SCENE_LEVEL_1:
                        _message_text.text = SCENE_LEVEL_1;
                        break;
                    case SCENE_LEVEL_2:
                        _message_text.text = SCENE_LEVEL_2;
                        break;
                    case SCENE_LEVEL_3:
                        _message_text.text = SCENE_LEVEL_3;
                        break;
                }
                Observable.Timer(TimeSpan.FromSeconds(1.5)).Subscribe(onNext: _ => {
                    _message_text.text = MESSAGE_LEVEL_START;
                }).AddTo(this);
                Observable.Timer(TimeSpan.FromSeconds(3.0)).Subscribe(onNext: _ => {
                    _message_text.text = string.Empty;
                }).AddTo(this);
            };
        }

        // Start is called before the first frame update
        void Start() {
            // update text ui.
            this.UpdateAsObservable().Subscribe(onNext: _ => {
                updateGameStatus();
                updateVehicleStatus();
                updateFpsStatus();
            }).AddTo(this);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // private Methods [verb]

        /// <summary>
        /// update game status
        /// </summary>
        void updateGameStatus() {
            //_targets_text.text = string.Format("TGT {0}/{1}", _game_system.targetTotal - _game_system.targetRemain, _game_system.targetTotal);
            //_points_text.text = string.Format("POINT {0}", _game_system.pointTotal);
            _mode_text.text = string.Format("Mode: {0}", _game_system.mode);
            switch (_game_system.mode) {
                case MODE_EASY: _mode_text.color = yellow; break;
                case MODE_NORMAL: _mode_text.color = green; break;
                case MODE_HARD: _mode_text.color = purple; break;
            }
        }

        /// <summary>
        /// update vehicle status
        /// </summary>
        void updateVehicleStatus() {
        }

        /// <summary>
        /// update fps status
        /// </summary>
        void updateFpsStatus() {
            _frame_count++;
            _elapsed_time += Time.deltaTime;
            if (_elapsed_time >= 1.0f) {
                float fps = 1.0f * _frame_count / _elapsed_time;
                string fps_rate = $"FPS {fps.ToString(format: "F2")}";
                _fps_text.text = fps_rate;
                _frame_count = 0;
                _elapsed_time = 0f;
            }
        }
    }
}