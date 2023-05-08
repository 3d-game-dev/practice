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

namespace GameDev {
    /// <summary>
    /// sound system
    /// <author>h.adachi (STUDIO MeowToon)</author>
    /// </summary>
    public class SoundSystem : MonoBehaviour {
#nullable enable

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // References [bool => is+adjective, has+past participle, can+verb prototype, triad verb]

        [SerializeField] AudioClip _se_item_clip;

        [SerializeField] AudioClip _se_jump_clip;

        [SerializeField] AudioClip _se_climb_clip;

        [SerializeField] AudioClip _se_walk_clip;

        [SerializeField] AudioClip _se_run_clip;

        [SerializeField] AudioClip _se_grounded_clip;

        [SerializeField] AudioClip _bgm_beat_level_clip;

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // Fields [noun, adjectives] 

        string _now_playing_clip_se1;

        AudioSource _audio_source_se1;

        AudioSource _audio_source_bgm;

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // public Methods [verb]

        public void PlayItemClip() {
            if (_audio_source_se1.isPlaying) {
                _audio_source_se1.Stop();
            }
            _audio_source_se1.PlayOneShot(_se_item_clip, 2.5f);
            _now_playing_clip_se1 = "se_item_clip";
        }

        public void PlayJumpClip() {
            if (_audio_source_se1.isPlaying) {
                _audio_source_se1.Stop();
            }
            _audio_source_se1.PlayOneShot(_se_jump_clip);
            _now_playing_clip_se1 = "se_jump_clip";
        }

        public void PlayGroundedClip() {
            if (_audio_source_se1.isPlaying) {
                _audio_source_se1.Stop();
            }
            _audio_source_se1.PlayOneShot(_se_grounded_clip, 0.65f);
            _now_playing_clip_se1 = "se_grounded_clip";
        }

        public void PlayWalkClip() {
            if (_now_playing_clip_se1 != "se_walkClip" && _now_playing_clip_se1 != "se_grounded_clip" && _now_playing_clip_se1 != "se_item_clip") {
                _audio_source_se1.Stop();
            }
            if (!_audio_source_se1.isPlaying) {
                _audio_source_se1.clip = _se_walk_clip;
                _audio_source_se1.Play();
                _now_playing_clip_se1 = "se_walkClip";
            }
        }

        public void PlayRunClip() {
            if (_now_playing_clip_se1 != "se_run_clip" && _now_playing_clip_se1 != "se_grounded_clip" && _now_playing_clip_se1 != "se_item_clip") {
                _audio_source_se1.Stop();
            }
            if (!_audio_source_se1.isPlaying) {
                _audio_source_se1.clip = _se_run_clip;
                _audio_source_se1.Play();
                _now_playing_clip_se1 = "se_run_clip";
            }
        }

        public void PlayClimbClip() {
            if (_now_playing_clip_se1 != "se_climb_clip") {
                _audio_source_se1.Stop();
            }
            if (!_audio_source_se1.isPlaying) {
                _audio_source_se1.clip = _se_climb_clip;
                _audio_source_se1.Play();
                _now_playing_clip_se1 = "se_climb_clip";
            }
        }

        public void StopClip() {
            if (_now_playing_clip_se1 != "se_grounded_clip" && _now_playing_clip_se1 != "se_item_clip") {
                _audio_source_se1.Stop();
            }
        }

        public void PlayBeatLevelClip() {
            _audio_source_bgm.Stop();
            _audio_source_bgm.clip = _bgm_beat_level_clip;
            _audio_source_bgm.Play();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // update Methods

        // Start is called before the first frame update
        void Start() {
            _audio_source_se1 = GetComponents<AudioSource>()[0]; // SE
            _audio_source_bgm = GetComponents<AudioSource>()[1]; // BGM
        }

        // Update is called once per frame
        void Update() {
        }
    }
}