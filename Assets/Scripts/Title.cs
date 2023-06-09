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

using static UnityEngine.GameObject;
using static UnityEngine.SceneManagement.SceneManager;
using UniRx;
using UniRx.Triggers;

using static GameDev.Env;

namespace GameDev {
    /// <summary>
    /// title scene
    /// </summary>
    /// <author>h.adachi (STUDIO MeowToon)</author>
    public class Title : InputMaper {
#nullable enable

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // Fields [noun, adjectives]

        GameSystem _game_system;

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // update Methods

        // Awake is called when the script instance is being loaded.
        void Awake() {
            _game_system = Find(name: GAME_SYSTEM).Get<GameSystem>();
        }

        // Start is called before the first frame update
        new void Start() {
            base.Start();

            /// <summary>
            /// open select.
            /// </summary>
            this.UpdateAsObservable().Where(predicate: _ => _select_button.wasPressedThisFrame).Subscribe(onNext: _ => {
                LoadScene(sceneName: SCENE_SELECT);
            }).AddTo(this);

            /// <summary>
            /// start level.
            /// </summary>
            this.UpdateAsObservable().Where(predicate: _ => (_start_button.wasPressedThisFrame || _a_button.wasPressedThisFrame)).Subscribe(onNext: _ => {
                LoadScene(sceneName: SCENE_LEVEL_1);
            }).AddTo(this);
        }
    }
}
