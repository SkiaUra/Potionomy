// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2020/01/31

using System;
using UnityEngine;
#if false // TEXTMESHPRO_MARKER
using TMPro;
#endif
#if true // UI_MARKER
using UnityEngine.UI;
#endif

#pragma warning disable CS1522 // Empty switch block (can be caused by disabling of DOTween modules
namespace DG.Tweening.Timeline.Core.Plugins
{
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
#endif
    static class DefaultTweenPlugins
    {
#if UNITY_EDITOR
        static DefaultTweenPlugins()
        {
            // Used only to register plugins to be displayed in editor's timeline (runtime uses Register method directly)
            if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode) Register();
        }
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Register()
        {
            DOVisualPluginsManager.RegisterGlobalTweenPlugins(GetGlobalTweenPlugin, "DOTweenDefaults");
            DOVisualPluginsManager.RegisterTweenPlugins(GetTweenPlugin);
        }

        static DOVisualTweenPlugin GetGlobalTweenPlugin(string id)
        {
            switch (id) {
            case "DOTweenDefaults":
                return DOVisualPluginsManager.CacheAndReturnGlobal(id,
                    new PlugDataGlobalTween("Time/Time Scale", ()=> Time.timeScale, x => Time.timeScale = x),
                    new PlugDataGlobalTween("Time/Fixed Delta Time", ()=> Time.fixedDeltaTime, x => Time.fixedDeltaTime = x)
                );
            }
            return null;
        }

        static DOVisualTweenPlugin GetTweenPlugin(Type targetType, string targetTypeFullName)
        {
            switch (targetTypeFullName) {
#if true // AUDIO_MARKER
            case "UnityEngine.AudioSource":
                return DOVisualPluginsManager.CacheAndReturn(targetType,
                    new PlugDataTween("Volume", (c,s,i) => ()=> ((AudioSource)c).volume, (c,s,i) => x => ((AudioSource)c).volume = x),
                    new PlugDataTween("Pitch", (c,s,i) => ()=> ((AudioSource)c).pitch, (c,s,i) => x => ((AudioSource)c).pitch = x),
                    new PlugDataTween("Doppler Level", (c,s,i) => ()=> ((AudioSource)c).dopplerLevel, (c,s,i) => x => ((AudioSource)c).dopplerLevel = x),
                    new PlugDataTween("Time", (c,s,i) => ()=> ((AudioSource)c).time, (c,s,i) => x => ((AudioSource)c).time = x)
                );
#endif
            case "UnityEngine.Camera":
                return DOVisualPluginsManager.CacheAndReturn(targetType,
                    new PlugDataTween("Background Color", (c,s,i) => ()=> ((Camera)c).backgroundColor, (c,s,i) => x => ((Camera)c).backgroundColor = x),
                    new PlugDataTween("Field of View", (c,s,i) => ()=> ((Camera)c).fieldOfView, (c,s,i) => x => ((Camera)c).fieldOfView = x),
                    new PlugDataTween("Rect", (c,s,i) => ()=> ((Camera)c).rect, (c,s,i) => x => ((Camera)c).rect = x),
                    new PlugDataTween("Pixel Rect", (c,s,i) => ()=> ((Camera)c).pixelRect, (c,s,i) => x => ((Camera)c).pixelRect = x),
                    new PlugDataTween("Far Clip Plane", (c,s,i) => ()=> ((Camera)c).farClipPlane, (c,s,i) => x => ((Camera)c).farClipPlane = x),
                    new PlugDataTween("Near Clip Plane", (c,s,i) => ()=> ((Camera)c).nearClipPlane, (c,s,i) => x => ((Camera)c).nearClipPlane = x),
                    new PlugDataTween("Orthographic Size", (c,s,i) => ()=> ((Camera)c).orthographicSize, (c,s,i) => x => ((Camera)c).orthographicSize = x),
                    new PlugDataTween("Focal Length", (c,s,i) => ()=> ((Camera)c).focalLength, (c,s,i) => x => ((Camera)c).focalLength = x),
                    new PlugDataTween("Lens Shift", (c,s,i) => ()=> ((Camera)c).lensShift, (c,s,i) => x => ((Camera)c).lensShift = x)
                );
            case "UnityEngine.Light":
                return DOVisualPluginsManager.CacheAndReturn(targetType,
                    new PlugDataTween("Color", (c,s,i) => ()=> ((Light)c).color, (c,s,i) => x => ((Light)c).color = x),
                    new PlugDataTween("Color Temperature", (c,s,i) => ()=> ((Light)c).colorTemperature, (c,s,i) => x => ((Light)c).colorTemperature = x),
                    new PlugDataTween("Intensity", (c,s,i) => ()=> ((Light)c).intensity, (c,s,i) => x => ((Light)c).intensity = x),
                    new PlugDataTween("Range", (c,s,i) => ()=> ((Light)c).range, (c,s,i) => x => ((Light)c).range = x),
                    new PlugDataTween("Spot Angle", (c,s,i) => ()=> ((Light)c).spotAngle, (c,s,i) => x => ((Light)c).spotAngle = x),
                    new PlugDataTween("Shadow Strength", (c,s,i) => ()=> ((Light)c).shadowStrength, (c,s,i) => x => ((Light)c).shadowStrength = x)
                );
            case "UnityEngine.MeshRenderer":
                return DOVisualPluginsManager.CacheAndReturn(targetType,
                    new PlugDataTween("Instance/Main Color", (c,s,i) => ()=> ((MeshRenderer)c).material.color, (c,s,i) => x => ((MeshRenderer)c).material.color = x),
                    new PlugDataTween("Instance/Color Property (string ID)",
                        (c,s,i) => ()=> ((MeshRenderer)c).material.GetColor(s),
                        (c,s,i) => x => ((MeshRenderer)c).material.SetColor(s, x), PluginTweenType.StringOption, "ID"),
                    new PlugDataTween("Instance/Color Property (int ID)",
                        (c,s,i) => ()=> ((MeshRenderer)c).material.GetColor(i),
                        (c,s,i) => x => ((MeshRenderer)c).material.SetColor(i, x), PluginTweenType.IntOption, null, "ID"),
                    new PlugDataTween("Instance/Main Offset", (c,s,i) => ()=> ((MeshRenderer)c).material.mainTextureOffset, (c,s,i) => x => ((MeshRenderer)c).material.mainTextureOffset = x),
                    new PlugDataTween("Instance/Offset Property (string ID)",
                        (c,s,i) => ()=> ((MeshRenderer)c).material.GetTextureOffset(s),
                        (c,s,i) => x => ((MeshRenderer)c).material.SetTextureOffset(s, x), PluginTweenType.StringOption, "ID"),
                    new PlugDataTween("Instance/Offset Property (int ID)",
                        (c,s,i) => ()=> ((MeshRenderer)c).material.GetTextureOffset(i),
                        (c,s,i) => x => ((MeshRenderer)c).material.SetTextureOffset(i, x), PluginTweenType.IntOption, null, "ID"),
                    new PlugDataTween("Instance/Main Tiling", (c,s,i) => ()=> ((MeshRenderer)c).material.mainTextureScale, (c,s,i) => x => ((MeshRenderer)c).material.mainTextureScale = x),
                    new PlugDataTween("Instance/Tiling Property (string ID)",
                        (c,s,i) => ()=> ((MeshRenderer)c).material.GetTextureScale(s),
                        (c,s,i) => x => ((MeshRenderer)c).material.SetTextureScale(s, x), PluginTweenType.StringOption, "ID"),
                    new PlugDataTween("Instance/Tiling Property (int ID)",
                        (c,s,i) => ()=> ((MeshRenderer)c).material.GetTextureScale(i),
                        (c,s,i) => x => ((MeshRenderer)c).material.SetTextureScale(i, x), PluginTweenType.IntOption, null, "ID"),
                    new PlugDataTween("Instance/Float Property (string ID)",
                        (c,s,i) => ()=> ((MeshRenderer)c).material.GetFloat(s),
                        (c,s,i) => x => ((MeshRenderer)c).material.SetFloat(s, x), PluginTweenType.StringOption, "ID"),
                    new PlugDataTween("Instance/Float Property (int ID)",
                        (c,s,i) => ()=> ((MeshRenderer)c).material.GetFloat(i),
                        (c,s,i) => x => ((MeshRenderer)c).material.SetFloat(i, x), PluginTweenType.IntOption, null, "ID"),
                    new PlugDataTween("Instance/Vector Property (string ID)",
                        (c,s,i) => ()=> ((MeshRenderer)c).material.GetVector(s),
                        (c,s,i) => x => ((MeshRenderer)c).material.SetVector(s, x), PluginTweenType.StringOption, "ID"),
                    new PlugDataTween("Instance/Vector Property (int ID)",
                        (c,s,i) => ()=> ((MeshRenderer)c).material.GetVector(i),
                        (c,s,i) => x => ((MeshRenderer)c).material.SetVector(i, x), PluginTweenType.IntOption, null, "ID"),
                    new PlugDataTween("Shared/Main Color", (c,s,i) => ()=> ((MeshRenderer)c).sharedMaterial.color, (c,s,i) => x => ((MeshRenderer)c).sharedMaterial.color = x),
                    new PlugDataTween("Shared/Color Property (string ID)",
                        (c,s,i) => ()=> ((MeshRenderer)c).sharedMaterial.GetColor(s),
                        (c,s,i) => x => ((MeshRenderer)c).sharedMaterial.SetColor(s, x), PluginTweenType.StringOption, "ID"),
                    new PlugDataTween("Shared/Color Property (int ID)",
                        (c,s,i) => ()=> ((MeshRenderer)c).sharedMaterial.GetColor(i),
                        (c,s,i) => x => ((MeshRenderer)c).sharedMaterial.SetColor(i, x), PluginTweenType.IntOption, null, "ID"),
                    new PlugDataTween("Shared/Main Offset", (c,s,i) => ()=> ((MeshRenderer)c).sharedMaterial.mainTextureOffset, (c,s,i) => x => ((MeshRenderer)c).sharedMaterial.mainTextureOffset = x),
                    new PlugDataTween("Shared/Offset Property (string ID)",
                        (c,s,i) => ()=> ((MeshRenderer)c).sharedMaterial.GetTextureOffset(s),
                        (c,s,i) => x => ((MeshRenderer)c).sharedMaterial.SetTextureOffset(s, x), PluginTweenType.StringOption, "ID"),
                    new PlugDataTween("Shared/Offset Property (int ID)",
                        (c,s,i) => ()=> ((MeshRenderer)c).sharedMaterial.GetTextureOffset(i),
                        (c,s,i) => x => ((MeshRenderer)c).sharedMaterial.SetTextureOffset(i, x), PluginTweenType.IntOption, null, "ID"),
                    new PlugDataTween("Shared/Main Tiling", (c,s,i) => ()=> ((MeshRenderer)c).sharedMaterial.mainTextureScale, (c,s,i) => x => ((MeshRenderer)c).sharedMaterial.mainTextureScale = x),
                    new PlugDataTween("Shared/Tiling Property (string ID)",
                        (c,s,i) => ()=> ((MeshRenderer)c).sharedMaterial.GetTextureScale(s),
                        (c,s,i) => x => ((MeshRenderer)c).sharedMaterial.SetTextureScale(s, x), PluginTweenType.StringOption, "ID"),
                    new PlugDataTween("Shared/Tiling Property (int ID)",
                        (c,s,i) => ()=> ((MeshRenderer)c).sharedMaterial.GetTextureScale(i),
                        (c,s,i) => x => ((MeshRenderer)c).sharedMaterial.SetTextureScale(i, x), PluginTweenType.IntOption, null, "ID"),
                    new PlugDataTween("Shared/Float Property (string ID)",
                        (c,s,i) => ()=> ((MeshRenderer)c).sharedMaterial.GetFloat(s),
                        (c,s,i) => x => ((MeshRenderer)c).sharedMaterial.SetFloat(s, x), PluginTweenType.StringOption, "ID"),
                    new PlugDataTween("Shared/Float Property (int ID)",
                        (c,s,i) => ()=> ((MeshRenderer)c).sharedMaterial.GetFloat(i),
                        (c,s,i) => x => ((MeshRenderer)c).sharedMaterial.SetFloat(i, x), PluginTweenType.IntOption, null, "ID"),
                    new PlugDataTween("Shared/Vector Property (string ID)",
                        (c,s,i) => ()=> ((MeshRenderer)c).sharedMaterial.GetVector(s),
                        (c,s,i) => x => ((MeshRenderer)c).sharedMaterial.SetVector(s, x), PluginTweenType.StringOption, "ID"),
                    new PlugDataTween("Shared/Vector Property (int ID)",
                        (c,s,i) => ()=> ((MeshRenderer)c).sharedMaterial.GetVector(i),
                        (c,s,i) => x => ((MeshRenderer)c).sharedMaterial.SetVector(i, x), PluginTweenType.IntOption, null, "ID")
                );
            case "UnityEngine.ParticleSystem":
                return DOVisualPluginsManager.CacheAndReturn(targetType,
                    new PlugDataTween("Time", (c,s,i) => ()=> ((ParticleSystem)c).time, (c,s,i) => x => ((ParticleSystem)c).time = x),
                    new PlugDataTween("Main/Simulation Speed",
                        (c,s,i) => ()=> { ParticleSystem ps = (ParticleSystem)c; var module = ps.main; return module.simulationSpeed; },
                        (c,s,i) => x => { ParticleSystem ps = (ParticleSystem)c; var module = ps.main; module.simulationSpeed = x; }),
                    new PlugDataTween("Main/Color",
                        (c,s,i) => ()=> { ParticleSystem ps = (ParticleSystem)c; var module = ps.main; return module.startColor.color; },
                        (c,s,i) => x => { ParticleSystem ps = (ParticleSystem)c; var module = ps.main; module.startColor = x; })
                );
#if true // UI_MARKER
            case "UnityEngine.RectTransform":
                return DOVisualPluginsManager.CacheAndReturn(targetType,
                    new PlugDataTween("AnchoredPosition", (c,s,i) => ()=> ((RectTransform)c).anchoredPosition, (c,s,i) => x => ((RectTransform)c).anchoredPosition = x),
                    new PlugDataTween("AnchoredPosition 3D", (c,s,i) => ()=> ((RectTransform)c).anchoredPosition3D, (c,s,i) => x => ((RectTransform)c).anchoredPosition3D = x),
                    new PlugDataTween("SizeDelta", (c,s,i) => ()=> ((RectTransform)c).sizeDelta, (c,s,i) => x => ((RectTransform)c).sizeDelta = x),
                    new PlugDataTween("AnchorMax", (c,s,i) => ()=> ((RectTransform)c).anchorMax, (c,s,i) => x => ((RectTransform)c).anchorMax = x),
                    new PlugDataTween("AnchorMin", (c,s,i) => ()=> ((RectTransform)c).anchorMin, (c,s,i) => x => ((RectTransform)c).anchorMin = x),
                    new PlugDataTween("Pivot", (c,s,i) => ()=> ((RectTransform)c).pivot, (c,s,i) => x => ((RectTransform)c).pivot = x),
                    new PlugDataTween("Punch/AnchoredPosition", (c,s,i) => ()=> ((RectTransform)c).anchoredPosition, (c,s,i) => x => ((RectTransform)c).anchoredPosition = x, PluginTweenType.Punch),
                    new PlugDataTween("Punch/AnchoredPosition 3D", (c,s,i) => ()=> ((RectTransform)c).anchoredPosition3D, (c,s,i) => x => ((RectTransform)c).anchoredPosition3D = x, PluginTweenType.Punch),
                    new PlugDataTween("Shake/AnchoredPosition", (c,s,i) => ()=> ((RectTransform)c).anchoredPosition, (c,s,i) => x => ((RectTransform)c).anchoredPosition = x, PluginTweenType.Shake),
                    new PlugDataTween("Shake/AnchoredPosition 3D", (c,s,i) => ()=> ((RectTransform)c).anchoredPosition3D, (c,s,i) => x => ((RectTransform)c).anchoredPosition3D = x, PluginTweenType.Shake),
                    // Transform-based
                    new PlugDataTween("Rotation", (c,s,i) => ()=> ((Transform)c).rotation, (c,s,i) => x => ((Transform)c).rotation = x),
                    new PlugDataTween("Local Rotation", (c,s,i) => ()=> ((Transform)c).localRotation, (c,s,i) => x => ((Transform)c).localRotation = x),
                    new PlugDataTween("Scale", (c,s,i) => ()=> ((Transform)c).localScale, (c,s,i) => x => ((Transform)c).localScale = x),
                    new PlugDataTween("Punch/Rotation", (c,s,i) => ()=> ((Transform)c).eulerAngles, (c,s,i) => x => ((Transform)c).eulerAngles = x, PluginTweenType.Punch),
                    new PlugDataTween("Punch/Local Rotation", (c,s,i) => ()=> ((Transform)c).localEulerAngles, (c,s,i) => x => ((Transform)c).localEulerAngles = x, PluginTweenType.Punch),
                    new PlugDataTween("Punch/Scale", (c,s,i) => ()=> ((Transform)c).localScale, (c,s,i) => x => ((Transform)c).localScale = x, PluginTweenType.Punch),
                    new PlugDataTween("Shake/Rotation", (c,s,i) => ()=> ((Transform)c).eulerAngles, (c,s,i) => x => ((Transform)c).eulerAngles = x, PluginTweenType.Shake),
                    new PlugDataTween("Shake/Local Rotation", (c,s,i) => ()=> ((Transform)c).localEulerAngles, (c,s,i) => x => ((Transform)c).localEulerAngles = x, PluginTweenType.Shake),
                    new PlugDataTween("Shake/Scale", (c,s,i) => ()=> ((Transform)c).localScale, (c,s,i) => x => ((Transform)c).localScale = x, PluginTweenType.Shake)
                );
#endif
            case "UnityEngine.TrailRenderer":
                return DOVisualPluginsManager.CacheAndReturn(targetType,
                    new PlugDataTween("Time", (c,s,i) => ()=> ((TrailRenderer)c).time, (c,s,i) => x => ((TrailRenderer)c).time = x)
                );
            case "UnityEngine.Transform":
                return DOVisualPluginsManager.CacheAndReturn(targetType,
                    new PlugDataTween("Position", (c,s,i) => ()=> ((Transform)c).position, (c,s,i) => x => ((Transform)c).position = x),
                    new PlugDataTween("Local Position", (c,s,i) => ()=> ((Transform)c).localPosition, (c,s,i) => x => ((Transform)c).localPosition = x),
                    new PlugDataTween("Rotation", (c,s,i) => ()=> ((Transform)c).rotation, (c,s,i) => x => ((Transform)c).rotation = x),
                    new PlugDataTween("Local Rotation", (c,s,i) => ()=> ((Transform)c).localRotation, (c,s,i) => x => ((Transform)c).localRotation = x),
                    new PlugDataTween("Scale", (c,s,i) => ()=> ((Transform)c).localScale, (c,s,i) => x => ((Transform)c).localScale = x),
                    new PlugDataTween("Punch/Position", (c,s,i) => ()=> ((Transform)c).position, (c,s,i) => x => ((Transform)c).position = x, PluginTweenType.Punch),
                    new PlugDataTween("Punch/Local Position", (c,s,i) => ()=> ((Transform)c).localPosition, (c,s,i) => x => ((Transform)c).localPosition = x, PluginTweenType.Punch),
                    new PlugDataTween("Punch/Rotation", (c,s,i) => ()=> ((Transform)c).eulerAngles, (c,s,i) => x => ((Transform)c).eulerAngles = x, PluginTweenType.Punch),
                    new PlugDataTween("Punch/Local Rotation", (c,s,i) => ()=> ((Transform)c).localEulerAngles, (c,s,i) => x => ((Transform)c).localEulerAngles = x, PluginTweenType.Punch),
                    new PlugDataTween("Punch/Scale", (c,s,i) => ()=> ((Transform)c).localScale, (c,s,i) => x => ((Transform)c).localScale = x, PluginTweenType.Punch),
                    new PlugDataTween("Shake/Position", (c,s,i) => ()=> ((Transform)c).position, (c,s,i) => x => ((Transform)c).position = x, PluginTweenType.Shake),
                    new PlugDataTween("Shake/Local Position", (c,s,i) => ()=> ((Transform)c).localPosition, (c,s,i) => x => ((Transform)c).localPosition = x, PluginTweenType.Shake),
                    new PlugDataTween("Shake/Rotation", (c,s,i) => ()=> ((Transform)c).eulerAngles, (c,s,i) => x => ((Transform)c).eulerAngles = x, PluginTweenType.Shake),
                    new PlugDataTween("Shake/Local Rotation", (c,s,i) => ()=> ((Transform)c).localEulerAngles, (c,s,i) => x => ((Transform)c).localEulerAngles = x, PluginTweenType.Shake),
                    new PlugDataTween("Shake/Scale", (c,s,i) => ()=> ((Transform)c).localScale, (c,s,i) => x => ((Transform)c).localScale = x, PluginTweenType.Shake)
                );
#if true // PHYSICS_MARKER
            // Physics ------------------------------
            case "UnityEngine.Rigidbody":
                return DOVisualPluginsManager.CacheAndReturn(targetType,
                    new PlugDataTween("Position", (c,s,i) => ()=> ((Rigidbody)c).position, (c,s,i) => x => ((Rigidbody)c).MovePosition(x)),
                    new PlugDataTween("Rotation", (c,s,i) => ()=> ((Rigidbody)c).rotation, (c,s,i) => x => ((Rigidbody)c).MoveRotation(x))
                );
#endif
#if true // PHYSICS2D_MARKER
            // Physics2D ----------------------------
            case "UnityEngine.Rigidbody2D":
                return DOVisualPluginsManager.CacheAndReturn(targetType,
                    new PlugDataTween("Position", (c,s,i) => ()=> ((Rigidbody2D)c).position, (c,s,i) => x => ((Rigidbody2D)c).MovePosition(x)),
                    new PlugDataTween("Rotation", (c,s,i) => ()=> ((Rigidbody2D)c).rotation, (c,s,i) => x => ((Rigidbody2D)c).MoveRotation(x))
                );
#endif
#if true // SPRITE_MARKER
            // Sprite -------------------------------
            case "UnityEngine.SpriteRenderer":
                return DOVisualPluginsManager.CacheAndReturn(targetType,
                    new PlugDataTween("Color", (c,s,i) => ()=> ((SpriteRenderer)c).color, (c,s,i) => x => ((SpriteRenderer)c).color = x),
                    new PlugDataTween("Size", (c,s,i) => ()=> ((SpriteRenderer)c).size, (c,s,i) => x => ((SpriteRenderer)c).size = x)
                );
#endif
#if true // UI_MARKER
            // UI -----------------------------------
            case "UnityEngine.CanvasGroup":
                return DOVisualPluginsManager.CacheAndReturn(targetType,
                    new PlugDataTween("Alpha", (c,s,i) => ()=> ((CanvasGroup)c).alpha, (c,s,i) => x => ((CanvasGroup)c).alpha = x)
                );
            case "UnityEngine.UI.Image":
                return DOVisualPluginsManager.CacheAndReturn(targetType,
                    new PlugDataTween("Color", (c,s,i) => ()=> ((Image)c).color, (c,s,i) => x => ((Image)c).color = x),
                    new PlugDataTween("Fill Amount", (c,s,i) => ()=> ((Image)c).fillAmount, (c,s,i) => x => ((Image)c).fillAmount = x)
                );
            case "UnityEngine.UI.LayoutElement":
                return DOVisualPluginsManager.CacheAndReturn(targetType,
                    new PlugDataTween("Flexible Width", (c,s,i) => ()=> ((LayoutElement)c).flexibleWidth, (c,s,i) => x => ((LayoutElement)c).flexibleWidth = x),
                    new PlugDataTween("Flexible Height", (c,s,i) => ()=> ((LayoutElement)c).flexibleHeight, (c,s,i) => x => ((LayoutElement)c).flexibleHeight = x),
                    new PlugDataTween("Min Width", (c,s,i) => ()=> ((LayoutElement)c).minWidth, (c,s,i) => x => ((LayoutElement)c).minWidth = x),
                    new PlugDataTween("Min Height", (c,s,i) => ()=> ((LayoutElement)c).minHeight, (c,s,i) => x => ((LayoutElement)c).minHeight = x),
                    new PlugDataTween("Preferred Width", (c,s,i) => ()=> ((LayoutElement)c).preferredWidth, (c,s,i) => x => ((LayoutElement)c).preferredWidth = x),
                    new PlugDataTween("Preferred Height", (c,s,i) => ()=> ((LayoutElement)c).preferredHeight, (c,s,i) => x => ((LayoutElement)c).preferredHeight = x)
                );
            case "UnityEngine.UI.Slider":
                return DOVisualPluginsManager.CacheAndReturn(targetType,
                    new PlugDataTween("Value", (c,s,i) => ()=> ((Slider)c).value, (c,s,i) => x => ((Slider)c).value = x)
                );
            case "UnityEngine.UI.Text":
                return DOVisualPluginsManager.CacheAndReturn(targetType,
                    new PlugDataTween("Color", (c,s,i) => ()=> ((Text)c).color, (c,s,i) => x => ((Text)c).color = x),
                    new PlugDataTween("Text", (c,s,i) => ()=> ((Text)c).text, (c,s,i) => x => ((Text)c).text = x),
                    new PlugDataTween("Font Size", (c,s,i) => ()=> ((Text)c).fontSize, (c,s,i) => x => ((Text)c).fontSize = x)
                );
#endif
#if false // TEXTMESHPRO_MARKER
            // EXTRA - TextMesh Pro -----------------
            case "TMPro.TMP_Text":
            case "TMPro.TextMeshPro":
            case "TMPro.TextMeshProUGUI":
                return DOVisualPluginsManager.CacheAndReturn(targetType,
                    new PlugDataTween("Color", (c,s,i) => ()=> ((TMP_Text)c).color, (c,s,i) => x => ((TMP_Text)c).color = x),
                    new PlugDataTween("Face Color", (c,s,i) => ()=> ((TMP_Text)c).faceColor, (c,s,i) => x => ((TMP_Text)c).faceColor = x),
                    new PlugDataTween("Outline Color", (c,s,i) => ()=> ((TMP_Text)c).outlineColor, (c,s,i) => x => ((TMP_Text)c).outlineColor = x),
                    new PlugDataTween("Text", (c,s,i) => ()=> ((TMP_Text)c).text, (c,s,i) => x => ((TMP_Text)c).text = x),
                    new PlugDataTween("Font Size", (c,s,i) => ()=> ((TMP_Text)c).fontSize, (c,s,i) => x => ((TMP_Text)c).fontSize = x),
                    new PlugDataTween("Max Visible Chars", (c,s,i) => ()=> ((TMP_Text)c).maxVisibleCharacters, (c,s,i) => x => ((TMP_Text)c).maxVisibleCharacters = x),
                    new PlugDataTween("Max Visible Words", (c,s,i) => ()=> ((TMP_Text)c).maxVisibleWords, (c,s,i) => x => ((TMP_Text)c).maxVisibleWords = x)
                );
#endif
            }
            // Special cases where we want to look also by parent class
            if (targetType.IsSubclassOf(typeof(Graphic))) {
                return DOVisualPluginsManager.CacheAndReturn(targetType,
                    new PlugDataTween("Color", (c,s,i) => ()=> ((Graphic)c).color, (c,s,i) => x => ((Graphic)c).color = x)
                );
            }
            return null;
        }
    }
}
