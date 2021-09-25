// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2020/01/21

using System;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Timeline.Core.Plugins
{
    /// <summary>
    /// Contains/executes all possible tween types based on its componentType
    /// </summary>
    public class DOVisualTweenPlugin
    {
        public readonly Type targetType;
        public readonly ITweenPluginData[] pluginDatas; // Always sorted by label in editor windows

        public DOVisualTweenPlugin(Type targetType, params ITweenPluginData[] pluginDatas)
        {
            this.targetType = targetType;
            this.pluginDatas = pluginDatas;
        }

        #region Public Methods

        // Assumes target is not NULL
        public Tweener CreateTween(DOVisualSequenced sequenced, float timeMultiplier)
        {
            ITweenPluginData data = pluginDatas[sequenced.plugDataIndex];
            Tweener t = null;
            float duration = sequenced.duration * timeMultiplier;
            switch (data.propertyType) {
            case DOVisualSequenced.PropertyType.Float: //---------------------------------------------------------
                TweenerCore<float, float, FloatOptions> floatT;
                DOGetter<float> floatGetter = data.FloatGetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1);
                switch (sequenced.toType) {
                case DOVisualSequenced.ToFromType.Dynamic:
                    floatT = DOTween.To(
                        floatGetter, data.FloatSetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1), floatGetter(), duration
                    );
                    break;
                default:
                    floatT = DOTween.To(
                        floatGetter, data.FloatSetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                        sequenced.isRelative ? floatGetter() + sequenced.toFloatVal : sequenced.toFloatVal, duration
                    );
                    break;
                }
                floatT.SetOptions(sequenced.boolOption0);
                if (sequenced.fromType == DOVisualSequenced.ToFromType.Direct) {
                    floatT.From(sequenced.isRelative ? floatGetter() + sequenced.fromFloatVal : sequenced.fromFloatVal);
                }
                t = floatT;
                break;
            case DOVisualSequenced.PropertyType.Int: //---------------------------------------------------------
                TweenerCore<int, int, NoOptions> intT;
                DOGetter<int> intGetter = data.IntGetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1);
                switch (sequenced.toType) {
                case DOVisualSequenced.ToFromType.Dynamic:
                    intT = DOTween.To(
                        intGetter, data.IntSetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1), intGetter(), duration
                        );
                    break;
                default:
                    intT = DOTween.To(
                        intGetter, data.IntSetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                        sequenced.isRelative ? intGetter() + sequenced.toIntVal : sequenced.toIntVal, duration
                    );
                    break;
                }
                if (sequenced.fromType == DOVisualSequenced.ToFromType.Direct) {
                    intT.From(sequenced.isRelative ? intGetter() + sequenced.fromIntVal : sequenced.fromIntVal);
                }
                t = intT;
                break;
            case DOVisualSequenced.PropertyType.Uint: //---------------------------------------------------------
                TweenerCore<uint, uint, UintOptions> uintT;
                DOGetter<uint> uintGetter = data.UintGetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1);
                switch (sequenced.toType) {
                case DOVisualSequenced.ToFromType.Dynamic:
                    uintT = DOTween.To(
                        uintGetter, data.UintSetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1), uintGetter(), duration
                        );
                    break;
                default:
                    uintT = DOTween.To(
                        uintGetter, data.UintSetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                        sequenced.isRelative ? uintGetter() + sequenced.toUintVal : sequenced.toUintVal, duration
                    );
                    break;
                }
                if (sequenced.fromType == DOVisualSequenced.ToFromType.Direct) {
                    uintT.From(sequenced.isRelative ? uintGetter() + sequenced.fromUintVal : sequenced.fromUintVal);
                }
                t = uintT;
                break;
            case DOVisualSequenced.PropertyType.String: //---------------------------------------------------------
                TweenerCore<string, string, StringOptions> stringT;
                DOGetter<string> stringGetter = data.StringGetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1);
                switch (sequenced.toType) {
                case DOVisualSequenced.ToFromType.Dynamic:
                    stringT = DOTween.To(
                        stringGetter, data.StringSetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                        stringGetter(), duration
                    );
                    break;
                default:
                    stringT = DOTween.To(
                        stringGetter, data.StringSetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                        sequenced.isRelative ? stringGetter() + sequenced.toStringVal : sequenced.toStringVal, duration
                    );
                    break;
                }
                stringT.SetOptions(sequenced.boolOption0, (ScrambleMode)sequenced.intOption1, sequenced.stringOption0);
                if (sequenced.fromType == DOVisualSequenced.ToFromType.Direct) {
                    stringT.From(sequenced.isRelative ? stringGetter() + sequenced.fromStringVal : sequenced.fromStringVal);
                }
                t = stringT;
                break;
            case DOVisualSequenced.PropertyType.Vector2: //---------------------------------------------------------
                TweenerCore<Vector2, Vector2, VectorOptions> vector2T;
                DOGetter<Vector2> vector2Getter = data.Vector2Getter(sequenced.target, sequenced.stringOption0, sequenced.intOption1);
                switch (sequenced.toType) {
                case DOVisualSequenced.ToFromType.Dynamic:
                    vector2T = DOTween.To(
                        vector2Getter, data.Vector2Setter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                        vector2Getter(), duration
                    );
                    break;
                default:
                    vector2T = DOTween.To(
                        vector2Getter, data.Vector2Setter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                        sequenced.isRelative ? vector2Getter() + sequenced.toVector2Val : sequenced.toVector2Val, duration
                    );
                    break;
                }
                vector2T.SetOptions(sequenced.axisConstraint, sequenced.boolOption0);
                if (sequenced.fromType == DOVisualSequenced.ToFromType.Direct) {
                    bool noAxisConstraint = sequenced.axisConstraint == AxisConstraint.None;
                    Vector2 fromVector2ValFiltered = new Vector2(
                        noAxisConstraint || sequenced.axisConstraint == AxisConstraint.X ? sequenced.fromVector2Val.x : 0,
                        noAxisConstraint || sequenced.axisConstraint == AxisConstraint.Y ? sequenced.fromVector2Val.y : 0
                    );
                    vector2T.From(sequenced.isRelative ? vector2Getter() + fromVector2ValFiltered : fromVector2ValFiltered);
                }
                t = vector2T;
                break;
            case DOVisualSequenced.PropertyType.Vector3: //---------------------------------------------------------
                switch (data.tweenType) {
                case PluginTweenType.Punch: //---------------
                    TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> punchT;
                    punchT = DOTween.Punch(
                        data.Vector3Getter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                        data.Vector3Setter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                        sequenced.toVector3Val, duration, sequenced.intOption1, sequenced.floatOption0
                    );
                    punchT.SetOptions(sequenced.axisConstraint, sequenced.boolOption0);
                    t = punchT;
                    break;
                case PluginTweenType.Shake: //---------------
                    TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> shakeT;
                    shakeT = DOTween.Shake(
                        data.Vector3Getter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                        data.Vector3Setter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                        duration, sequenced.toVector3Val, sequenced.intOption1, sequenced.floatOption0, sequenced.intOption0 == 1
                    );
                    shakeT.SetOptions(sequenced.axisConstraint, sequenced.boolOption0);
                    t = shakeT;
                    break;
                default: //---------------
                    TweenerCore<Vector3, Vector3, VectorOptions> vector3T;
                    DOGetter<Vector3> vector3Getter = data.Vector3Getter(sequenced.target, sequenced.stringOption0, sequenced.intOption1);
                    switch (sequenced.toType) {
                    case DOVisualSequenced.ToFromType.Dynamic:
                        vector3T = DOTween.To(
                            vector3Getter, data.Vector3Setter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                            vector3Getter(), duration
                        );
                        break;
                    default:
                        vector3T = DOTween.To(
                            vector3Getter, data.Vector3Setter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                            sequenced.isRelative ? vector3Getter() + sequenced.toVector3Val : sequenced.toVector3Val, duration
                        );
                        break;
                    }
                    vector3T.SetOptions(sequenced.axisConstraint, sequenced.boolOption0);
                    if (sequenced.fromType == DOVisualSequenced.ToFromType.Direct) {
                        bool noAxisConstraint = sequenced.axisConstraint == AxisConstraint.None;
                        Vector3 fromVector3ValFiltered = new Vector3(
                            noAxisConstraint || sequenced.axisConstraint == AxisConstraint.X ? sequenced.fromVector3Val.x : 0,
                            noAxisConstraint || sequenced.axisConstraint == AxisConstraint.Y ? sequenced.fromVector3Val.y : 0,
                            noAxisConstraint || sequenced.axisConstraint == AxisConstraint.Z ? sequenced.fromVector3Val.z : 0
                        );
                        vector3T.From(sequenced.isRelative ? vector3Getter() + fromVector3ValFiltered : fromVector3ValFiltered);
                    }
                    t = vector3T;
                    break;
                }
                break;
            case DOVisualSequenced.PropertyType.Vector4: //---------------------------------------------------------
                TweenerCore<Vector4, Vector4, VectorOptions> vector4T;
                DOGetter<Vector4> vector4Getter = data.Vector4Getter(sequenced.target, sequenced.stringOption0, sequenced.intOption1);
                switch (sequenced.toType) {
                case DOVisualSequenced.ToFromType.Dynamic:
                    vector4T = DOTween.To(
                        vector4Getter, data.Vector4Setter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                        vector4Getter(), duration
                    );
                    break;
                default:
                    vector4T = DOTween.To(
                        vector4Getter, data.Vector4Setter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                        sequenced.isRelative ? vector4Getter() + sequenced.toVector4Val : sequenced.toVector4Val, duration
                    );
                    break;
                }
                vector4T.SetOptions(sequenced.axisConstraint, sequenced.boolOption0);
                if (sequenced.fromType == DOVisualSequenced.ToFromType.Direct) {
                    bool noAxisConstraint = sequenced.axisConstraint == AxisConstraint.None;
                    Vector4 fromVector4ValFiltered = new Vector4(
                        noAxisConstraint || sequenced.axisConstraint == AxisConstraint.X ? sequenced.fromVector4Val.x : 0,
                        noAxisConstraint || sequenced.axisConstraint == AxisConstraint.Y ? sequenced.fromVector4Val.y : 0,
                        noAxisConstraint || sequenced.axisConstraint == AxisConstraint.Z ? sequenced.fromVector4Val.z : 0,
                        noAxisConstraint || sequenced.axisConstraint == AxisConstraint.W ? sequenced.fromVector4Val.w : 0
                    );
                    vector4T.From(sequenced.isRelative ? vector4Getter() + fromVector4ValFiltered : fromVector4ValFiltered);
                }
                t = vector4T;
                break;
            case DOVisualSequenced.PropertyType.Quaternion: //---------------------------------------------------------
                TweenerCore<Quaternion, Vector3, QuaternionOptions> quaternionT;
                DOGetter<Quaternion> quaternionGetter = data.QuaternionGetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1);
                switch (sequenced.toType) {
                case DOVisualSequenced.ToFromType.Dynamic:
                    quaternionT = DOTween.To(
                        quaternionGetter, data.QuaternionSetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                        quaternionGetter().eulerAngles, duration
                    );
                    break;
                default:
                    quaternionT = DOTween.To(
                        quaternionGetter, data.QuaternionSetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                        sequenced.isRelative ? quaternionGetter().eulerAngles + sequenced.toVector3Val : sequenced.toVector3Val, duration
                    );
                    break;
                }
                if (sequenced.fromType == DOVisualSequenced.ToFromType.Direct) {
                    quaternionT.From(sequenced.isRelative ? quaternionGetter().eulerAngles + sequenced.fromVector3Val : sequenced.fromVector3Val);
                }
                quaternionT.plugOptions.rotateMode = (RotateMode)sequenced.intOption0;
                t = quaternionT;
                break;
            case DOVisualSequenced.PropertyType.Color: //---------------------------------------------------------
                TweenerCore<Color, Color, ColorOptions> colorT;
                DOGetter<Color> colorGetter = data.ColorGetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1);
                switch (sequenced.toType) {
                case DOVisualSequenced.ToFromType.Dynamic:
                    colorT = sequenced.boolOption0
                        ? DOTween.ToAlpha(
                            colorGetter, data.ColorSetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                            colorGetter().a, duration
                        )
                        : DOTween.To(
                            colorGetter, data.ColorSetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                            colorGetter(), duration
                        );
                    break;
                default:
                    colorT = sequenced.boolOption0
                        ? DOTween.ToAlpha(
                            colorGetter, data.ColorSetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                            sequenced.isRelative ? colorGetter().a + sequenced.toColorVal.a : sequenced.toColorVal.a, duration
                        )
                        : DOTween.To(
                            colorGetter, data.ColorSetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                            sequenced.isRelative ? colorGetter() + sequenced.toColorVal : sequenced.toColorVal, duration
                        );
                    break;
                }
                if (sequenced.fromType == DOVisualSequenced.ToFromType.Direct) {
                    if (sequenced.boolOption0) colorT.From(sequenced.isRelative ? colorGetter().a + sequenced.fromColorVal.a : sequenced.fromColorVal.a);
                    else colorT.From(sequenced.isRelative ? colorGetter() + sequenced.fromColorVal : sequenced.fromColorVal);
                }
                t = colorT;
                break;
            case DOVisualSequenced.PropertyType.Rect: //---------------------------------------------------------
                TweenerCore<Rect, Rect, RectOptions> rectT;
                DOGetter<Rect> rectGetter = data.RectGetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1);
                switch (sequenced.toType) {
                case DOVisualSequenced.ToFromType.Dynamic:
                    rectT = DOTween.To(
                        rectGetter, data.RectSetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1), rectGetter(), duration
                    );
                    break;
                default:
                    rectT = DOTween.To(
                            rectGetter, data.RectSetter(sequenced.target, sequenced.stringOption0, sequenced.intOption1),
                            sequenced.isRelative ? Add(rectGetter(), sequenced.toRectVal) : sequenced.toRectVal, duration
                        );
                    break;
                }
                if (sequenced.fromType == DOVisualSequenced.ToFromType.Direct) {
                    rectT.From(sequenced.isRelative ? Add(rectGetter(), sequenced.fromRectVal) : sequenced.fromRectVal);
                }
                t = rectT;
                break;
            }
            if (t == null) return null;
            if (sequenced.ease == Ease.INTERNAL_Custom) t.SetEase(sequenced.easeCurve);
            else t.SetEase(sequenced.ease, sequenced.overshootOrAmplitude, sequenced.period);
            t.SetLoops(sequenced.loops, sequenced.loopType);
            if (DOVisualSequenceSettings.isApplicationPlaying) {
                if (sequenced.onComplete.GetPersistentEventCount() > 0) t.OnComplete(sequenced.onComplete.Invoke);
                if (sequenced.onStepComplete.GetPersistentEventCount() > 0) t.OnStepComplete(sequenced.onStepComplete.Invoke);
                if (sequenced.onUpdate.GetPersistentEventCount() > 0) t.OnUpdate(sequenced.onUpdate.Invoke);
            }
            return t;
        }

#if UNITY_EDITOR
        GUIContent[] _editor_gcs; // Used to cache GUIContent values
        GUIContent[] _editor_gcs_timeline; // Used to cache GUIContent values
        readonly GUIContent _editor_missingPlugin = new GUIContent("<color=#ff0000>UNSUPPORTED</color>");

        public string Editor_GetShortTypeAndAnimationName(int plugIndex, bool forTimeline)
        {
            return targetType == null
                ? string.Format("<color=#68b3e2>Global</color> → <color=#ffa047>{0}</color>", Editor_GetAnimationName(plugIndex, forTimeline))
                : string.Format("<color=#68b3e2>{0}</color> → <color=#ffa047>{1}</color>", Editor_GetShortTypeName(), Editor_GetAnimationName(plugIndex, forTimeline));
        }

        public GUIContent Editor_GetShortTypeAndAnimationNameGUIContent(int plugIndex)
        {
            if (_editor_gcs == null) _editor_gcs = new GUIContent[pluginDatas.Length];
            if (plugIndex > _editor_gcs.Length) return _editor_missingPlugin; // This can happen if sequenced was created with a now disabled module
            if (_editor_gcs[plugIndex] == null) _editor_gcs[plugIndex] = new GUIContent(Editor_GetShortTypeAndAnimationName(plugIndex, false));
            return _editor_gcs[plugIndex];
        }

        public GUIContent Editor_GetAnimationNameGUIContent(int plugIndex, bool forTimeline)
        {
            if (_editor_gcs_timeline == null) _editor_gcs_timeline = new GUIContent[pluginDatas.Length];
            if (plugIndex > _editor_gcs_timeline.Length) return _editor_missingPlugin; // This can happen if sequenced was created with a now disabled module
            if (_editor_gcs_timeline[plugIndex] == null) _editor_gcs_timeline[plugIndex] = new GUIContent(Editor_GetAnimationName(plugIndex, forTimeline));
            return _editor_gcs_timeline[plugIndex];
        }

        public string Editor_GetShortTypeName(bool richText = false)
        {
            string s = targetType.FullName;
            int dotIndex = s.LastIndexOf('.');
            if (dotIndex != -1) s = s.Substring(dotIndex + 1);
            if (richText) s = string.Format("<color=#68b3e2>{0}</color>", s);
            return s;
        }

        string Editor_GetAnimationName(int plugIndex, bool forTimeline)
        {
            string label = pluginDatas[plugIndex].label;
            int lastSlashIndex = label.LastIndexOf('/');
            if (lastSlashIndex == -1)
                return string.Format(" <color=#ffa047>{0}</color>", label);
            return forTimeline
                ? string.Format("<color=#2e0020>{0}</color>→<color=#ffa047>{1}</color>",
                    label.Substring(0, lastSlashIndex), label.Substring(lastSlashIndex + 1))
                : string.Format("<color=#a8a5ff>{0}</color>→<color=#ffa047>{1}</color>",
                    label.Substring(0, lastSlashIndex), label.Substring(lastSlashIndex + 1));
        }
#endif

        #endregion

        #region Methods

        /// <summary>Adds one rect into another, and returns the resulting a</summary>
        static Rect Add(Rect a, Rect b)
        {
            if (b.xMin < a.xMin) a.xMin = b.xMin;
            if (b.xMax > a.xMax) a.xMax = b.xMax;
            if (b.yMin < a.yMin) a.yMin = b.yMin;
            if (b.yMax > a.yMax) a.yMax = b.yMax;
            return a;
        }

        #endregion
    }
}