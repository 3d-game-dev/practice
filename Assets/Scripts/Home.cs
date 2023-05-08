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
using static UnityEngine.Mathf;
using UniRx;
using UniRx.Triggers;

using static GameDev.Env;

namespace GameDev {
    /// <summary>
    /// home class
    /// </summary>
    /// <author>h.adachi (STUDIO MeowToon)</author>
    public class Home : MonoBehaviour {
#nullable enable

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // References [bool => is+adjective, has+past participle, can+verb prototype, triad verb]

        [SerializeField] bool _move = false;

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // Fields [noun, adjectives]

        GameSystem _game_system;

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // public Events [verb, verb phrase]

        public event Action? OnCameBack;

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // update Methods

        // Awake is called when the script instance is being loaded.
        void Awake() {
            _game_system = Find(name: GAME_SYSTEM).Get<GameSystem>();
        }

        // Start is called before the first frame update
        void Start() {
            float original_position = transform.position.y;
            this.UpdateAsObservable().Where(predicate: x => _move).Subscribe(onNext: x => {
                const int SPEED = 5; const float DISTANCE = 0.3f; 
                transform.position = new Vector3(
                    transform.position.x, original_position + PingPong(Time.time / SPEED, DISTANCE), transform.position.z
                );
            }).AddTo(this);

            /// <summary>
            /// when being touched player.
            /// </summary>
            this.OnCollisionEnterAsObservable().Where(predicate: x => x.Like(PLAYER_TYPE)).Subscribe(onNext: x => {
                OnCameBack?.Invoke();
            }).AddTo(this);

            /// <summary>
            /// when being touched player.
            /// </summary>
            this.OnTriggerEnterAsObservable().Where(predicate: x => x.Like(PLAYER_TYPE)).Subscribe(onNext: x => {
                OnCameBack?.Invoke();
            }).AddTo(this);
        }
    }
}
