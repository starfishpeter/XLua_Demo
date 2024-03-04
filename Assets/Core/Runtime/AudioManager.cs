//------------------------------------------------------------
// 脚本名:		AudioManager.cs
// 作者:			海星
// 描述:			音频管理
//------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarFramework.Runtime
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        private string BGMAAKey = "AudioAssets/BGM/";
        private string EffectAAKey = "AudioAssets/Effects/";
        private string SoundKey = "AudioAssets/Sound/";

        public AudioSource BGM;
        public AudioSource EffectLoop;
        public GameObject soundPlayer;

        private string nowPlayBGM;
        private string nowPlayEffect;

        public void BGMOn(string AAKey = "None")
        {
            if (AAKey != "None" && nowPlayBGM != AAKey)
            {
                LoadManager.Instance.LoadAsset<AudioClip>(BGMAAKey + AAKey, (clip) =>
                {
                    nowPlayBGM = AAKey;
                    BGM.clip = clip;
                    BGM.Play();
                });
            }
            else
            {
                if (!BGM.isPlaying)
                {
                    BGM.Play();
                }
            }
        }

        public void BGMOff()
        {
            BGM.Stop();
        }

        public void EffectOn(string AAKey = "None")
        {
            if (AAKey != "None" && nowPlayEffect != AAKey)
            {
                LoadManager.Instance.LoadAsset<AudioClip>(EffectAAKey + AAKey, (clip) =>
                {
                    nowPlayEffect = AAKey;
                    EffectLoop.clip = clip;
                    EffectLoop.Play();
                });
            }
            else
            {
                if (!EffectLoop.isPlaying)
                {
                    EffectLoop.Play();
                }
            }
        }

        public void EffectOff()
        {
            EffectLoop.Stop();
        }

        public void SoundPlay(string AAKey)
        {
            LoadManager.Instance.LoadAsset<AudioClip>(SoundKey + AAKey, (clip) =>
            {
                var obj = GameObjectPoolManager.Instance.GetPoolItem(soundPlayer, transform, clip.length);
                obj.SetActive(true);
                var sound = obj.GetComponent<AudioSource>();
                sound.clip = clip;
                sound.Play();
            });

        }
    }
}
