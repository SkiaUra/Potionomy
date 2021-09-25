// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2020/02/21

using UnityEngine;
#if false // DEUNITYEXTENDED_MARKER
using Demigiant.DemiTools.DeUnityExtended.Components;
#endif
#if false // DEAUDIO_MARKER
using DG.DeAudio;
#endif

#pragma warning disable CS1522 // Empty switch block (can be caused by disabling of DOTween modules
namespace DG.Tweening.Timeline.Core.Plugins
{
    /// <summary>
    /// Plugins that are activated via DOTween Setup (TODO implement)
    /// and that depend on other installed plugins
    /// </summary>
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
#endif
    static class OptionalPlugins
    {
#if UNITY_EDITOR
        static OptionalPlugins()
        {
            // Used only to register plugins to be displayed in editor's timeline (runtime uses Register method directly)
            if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode) Register();
        }
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Register()
        {
            DOVisualPluginsManager.RegisterActionPlugins(GetActionPlugin,
                "DOTweenOptional_DeAudio",
                "DOTweenOptional_DeUnityExtended"
            );
        }

        static DOVisualActionPlugin GetActionPlugin(string id)
        {
            switch (id) {
#if false // DEAUDIO_MARKER
            case "DOTweenOptional_DeAudio":
                return DOVisualPluginsManager.CacheAndReturnAction(id,
                    new PlugDataAction("DeAudio/Stop All", null,
                        (t,b,s,f0,f1,f2,f3,i) => DeAudioManager.Stop(),
                        null, "AudioClip"),
                    new PlugDataAction("DeAudio/Stop All in Group", null,
                        (t,b,s,f0,f1,f2,f3,i) => DeAudioManager.Stop((DeAudioGroupId)i),
                        null, "AudioClip", intOptionLabel: "Group", intOptionAsEnumType:typeof(DeAudioGroupId)),
                    new PlugDataAction("DeAudio/Pause All", null,
                        (t,b,s,f0,f1,f2,f3,i) => DeAudioManager.Pause(),
                        null, "AudioClip"),
                    new PlugDataAction("DeAudio/Pause All in Group", null,
                        (t,b,s,f0,f1,f2,f3,i) => DeAudioManager.Pause((DeAudioGroupId)i),
                        null, "AudioClip", intOptionLabel: "Group", intOptionAsEnumType:typeof(DeAudioGroupId)),
                    new PlugDataAction("DeAudio/Resume All", null,
                        (t,b,s,f0,f1,f2,f3,i) => DeAudioManager.Resume(),
                        null, "AudioClip"),
                    new PlugDataAction("DeAudio/Resume All in Group", null,
                        (t,b,s,f0,f1,f2,f3,i) => DeAudioManager.Resume((DeAudioGroupId)i),
                        null, "AudioClip", intOptionLabel: "Group", intOptionAsEnumType:typeof(DeAudioGroupId)),
                    new PlugDataAction("DeAudio/Play Clip", typeof(AudioClip),
                        (t,b,s,f0,f1,f2,f3,i) => DeAudioManager.PlayFrom((DeAudioGroupId)i, (AudioClip)t, f0, f1, f2, b),
                        null, "AudioClip", intOptionLabel:"Group", intOptionAsEnumType:typeof(DeAudioGroupId), boolOptionLabel:"Loop", defBoolValue:false,
                        float0OptionLabel:"From Time", float1OptionLabel:"Volume", defFloat1Value:1, float2OptionLabel:"Pitch", defFloat2Value:1),
                    new PlugDataAction("DeAudio/Stop Clip", typeof(AudioClip),
                        (t,b,s,f0,f1,f2,f3,i) => DeAudioManager.Stop((AudioClip)t),
                        null, "AudioClip"),
                    new PlugDataAction("DeAudio/FadeIn Clip", typeof(AudioClip),
                        (t,b,s,f0,f1,f2,f3,i) => DeAudioManager.PlayFrom((DeAudioGroupId)i, (AudioClip)t, f0, f1, f2, b).FadeFrom(0, f3),
                        null, "AudioClip", float3OptionLabel:"Duration", defFloat3Value:1.5f,
                        intOptionLabel: "Group", intOptionAsEnumType: typeof(DeAudioGroupId), boolOptionLabel:"Loop", defBoolValue:false,
                        float0OptionLabel:"From Time", float1OptionLabel:"Volume", defFloat1Value:1, float2OptionLabel:"Pitch", defFloat2Value:1),
                    new PlugDataAction("DeAudio/FadeOut Clip", typeof(AudioClip),
                        (t,b,s,f0,f1,f2,f3,i) => DeAudioManager.FadeOut((AudioClip)t, f0),
                        null, "AudioClip", float0OptionLabel:"Duration", defFloat0Value:1.5f)
                );
#endif
#if false // DEUNITYEXTENDED_MARKER
            case "DOTweenOptional_DeUnityExtended":
                return DOVisualPluginsManager.CacheAndReturnAction(id,
                    new PlugDataAction("ParticleSystemController/Deactivate", typeof(ParticleSystemController),
                        (t,b,s,f0,f1,f2,f3,i) => {
                            if (b) return;
                            ParticleSystemController psc = (ParticleSystemController)t;
                            psc.StopEmitting(true, true);
                            psc.Clear(true, true);
                        },
                        onCreation:(t,b,s,f0,f1,f2,f3,i) => {
                            if (!b) return;
                            ParticleSystemController psc = (ParticleSystemController)t;
                            psc.StopEmitting(true, true);
                            psc.Clear(true, true);
                        },
                        "PSC", boolOptionLabel:"Deactivate On Creation", defBoolValue:false),
                    new PlugDataAction("ParticleSystemController/Start Emitting", typeof(ParticleSystemController),
                        (t,b,s,f0,f1,f2,f3,i) => ((ParticleSystemController)t).StartEmitting(),
                        null, "PSC"),
                    new PlugDataAction("ParticleSystemController/Stop Emitting", typeof(ParticleSystemController),
                        (t,b,s,f0,f1,f2,f3,i) => ((ParticleSystemController)t).StopEmitting(true, b),
                        null, "PSC", boolOptionLabel:"And Clear", defBoolValue:false),
                    new PlugDataAction("ParticleSystemController/Set Emission Multiplier", typeof(ParticleSystemController),
                        (t,b,s,f0,f1,f2,f3,i) => ((ParticleSystemController)t).SetEmissionOverTimeMultiplier(f0),
                        null, "PSC", float0OptionLabel:"Multiplier", defFloat0Value:1),
                    new PlugDataAction("ParticleSystemController/Restart", typeof(ParticleSystemController),
                        (t,b,s,f0,f1,f2,f3,i) => ((ParticleSystemController)t).Restart(),
                        null, "PSC"),
                    new PlugDataAction("ParticleSystemController/Toggle Modules/ForceOverLifetime", typeof(ParticleSystemController),
                        (t, b, s, f0, f1, f2, f3, i) => {
                            ParticleSystemController psc = (ParticleSystemController)t;
                            ParticleSystem.ForceOverLifetimeModule module = psc.sys.forceOverLifetime;
                            module.enabled = b;
                        }, null, "PSC", boolOptionLabel:"Enabled", defBoolValue:true),
                    new PlugDataAction("ParticleSystemController/Toggle Modules/VelocityOverLifetime", typeof(ParticleSystemController),
                        (t, b, s, f0, f1, f2, f3, i) => {
                            ParticleSystemController psc = (ParticleSystemController)t;
                            ParticleSystem.VelocityOverLifetimeModule module = psc.sys.velocityOverLifetime;
                            module.enabled = b;
                        }, null, "PSC", boolOptionLabel:"Enabled", defBoolValue:true)
                );
#endif
            }
            return null;
        }
    }
}
