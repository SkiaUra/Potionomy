// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2020/01/16

using System;
using DG.Tweening.Timeline.Core;
using UnityEngine;

namespace DG.Tweening.Timeline
{
    public class DOVisualSequencer : MonoBehaviour
    {
        #region Serialized
#pragma warning disable 0649

        public DOVisualSequence[] sequences = new []{ new DOVisualSequence(Guid.NewGuid().ToString()) };

#pragma warning restore 0649
        #endregion

        #region Unity

        // Edit: Use OnEnable instead of Start so that the auto-play animations replays on enable.
        // To avoid this behaviour on have one time animation, they should not be destroyed on complete.
        void OnEnable()
        {
            int len = sequences.Length;
            if (DOVisualSequenceSettings.I.debugLogs) {
                DOLog.Normal(string.Format("DOVisualSequencer <color=#d568e3>{0}</color> : Startup, iterating through {1} DOVisualSequences", this.name, len), this);
            }
            for (int i = 0; i < len; ++i) {
                if (sequences[i].isActive) sequences[i].GenerateTween();
            }
        }

        #endregion

        #region Public Methods

        public DOVisualSequence FindSequenceByGuid(string guid)
        {
            int len = sequences.Length;
            for (int i = 0; i < len; ++i) {
                if (sequences[i].guid == guid) return sequences[i];
            }
            return null;
        }

        #endregion
    }
}