// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2020/02/01

using UnityEngine;

namespace DG.Tweening.Timeline.Core.Plugins
{
    /// <summary>
    /// Contains/executes all possible action types
    /// </summary>
    public class DOVisualActionPlugin
    {
        public readonly PlugDataAction[] pluginDatas; // Always sorted by label in editor windows

        public DOVisualActionPlugin(PlugDataAction[] pluginDatas)
        {
            this.pluginDatas = pluginDatas;
        }

        #region Public Methods

#if UNITY_EDITOR
        GUIContent[] _editor_gcs; // Used to cache GUIContent values
        GUIContent[] _editor_gcs_timeline; // Used to cache GUIContent values

        public GUIContent Editor_GetSequencedHeaderLabelGUIContent(int plugIndex, bool forTimeline)
        {
            if (forTimeline && _editor_gcs == null) _editor_gcs = new GUIContent[pluginDatas.Length];
            else if (!forTimeline && _editor_gcs_timeline == null) _editor_gcs_timeline = new GUIContent[pluginDatas.Length];
            GUIContent[] list = forTimeline ? _editor_gcs_timeline : _editor_gcs;
            if (list == null) list = new GUIContent[pluginDatas.Length];
            if (list[plugIndex] == null) list[plugIndex] = new GUIContent(Editor_GetSequencedHeaderLabel(plugIndex, forTimeline));
            return list[plugIndex];
        }

        string Editor_GetSequencedHeaderLabel(int plugIndex, bool forTimeline)
        {
            string label = pluginDatas[plugIndex].label;
            int lastSlashIndex = label.LastIndexOf('/');
            if (lastSlashIndex == -1)
                return forTimeline
                    ? string.Format(" <color=#ffa047>{0}</color>", label)
                    : string.Format("<color=#ffa047>{0}</color>", label);
            return forTimeline
                ? string.Format(" <color=#2e0020>{0}</color>→<color=#ffa047>{1}</color>",
                    label.Substring(0, lastSlashIndex), label.Substring(lastSlashIndex + 1)
                )
                : string.Format("<color=#d00093>{0}</color>→<color=#ffa047>{1}</color>",
                    label.Substring(0, lastSlashIndex), label.Substring(lastSlashIndex + 1)
                );
        }
#endif

        #endregion
    }
}