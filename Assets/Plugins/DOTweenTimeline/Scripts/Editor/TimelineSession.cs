// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2020/08/07

using DG.Tweening.Timeline.Core;
using UnityEditor;

namespace DG.Tweening.TimelineEditor
{
    /// <summary>
    /// Session-only editor data
    /// </summary>
    internal static class TimelineSession
    {
        public static bool isDevDebugMode {
            get { return SessionState.GetBool("DOTweenTimelineDeveloperDebugMode", false); }
            set {
                if (value == isDevDebugMode) return;
                DOLog.DebugDev("Developer Debug Mode " + (value ? "<color=#00ff00>ACTIVATED</color>" : "<color=#ff0000>DEACTIVATED</color>"));
                SessionState.SetBool("DOTweenTimelineDeveloperDebugMode", value);
            }
        }
    }
}