// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2020/03/15

using System;
using System.IO;
using DG.DemiEditor;
using UnityEngine;

namespace DG.Tweening.TimelineEditor
{
    public class TimelineAssetModificationProcessor : UnityEditor.AssetModificationProcessor
    {
        static string[] OnWillSaveAssets(string[] paths)
        {
            // Exit eventual recording or preview
            if (DOTimelineRecorder.isRecording) DOTimelineRecorder.ExitRecordMode(true);
            else if (DOTimelinePreviewManager.isPlayingOrPreviewing) DOTimelinePreviewManager.StopPreview();

            return paths;
        }
    }
    /// <summary>
    /// USed to
    /// </summary>
    class TimelineAssetPostProcessor : UnityEditor.AssetPostprocessor
    {
        // If this file is present the file list processing will be ignored
        const string _ADBIgnoreFileListProcessingFilePath = "Assets/-DOTweenTimelinePostProcessingSkipper.txt";

        static bool IgnoreFileListProcessing()
        {
            return File.Exists(DeEditorFileUtils.ADBPathToFullPath(_ADBIgnoreFileListProcessingFilePath));
        }

        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            string adbDOTweenTimelineDirPath = null;
            bool importedDOTweenTimeline = false;
            for (int i = 0; i < importedAssets.Length; ++i) {
                if (!importedAssets[i].Contains(DOTimelineSettings.DOTweenTimelineDirName)) continue;
                if (adbDOTweenTimelineDirPath == null) adbDOTweenTimelineDirPath = TimelineEditorUtils.FindADBDOTweenTimelineDirPath();
                if (!importedAssets[i].StartsWith(adbDOTweenTimelineDirPath)) continue;
                importedDOTweenTimeline = true;
                break;
            }
            if (!importedDOTweenTimeline) return;

            // Remove extra files
            bool simulate = IgnoreFileListProcessing();
            string adbListFilePath = adbDOTweenTimelineDirPath + "/" + DOTimelineSettings.DOTweenTimelineFileListFile;
            DeEditorPackageManager.ParseListAndRemoveExtraFiles("DOTweenTimeline File Parsing", adbListFilePath, adbDOTweenTimelineDirPath, simulate);
        }
    }
}