using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraTrack))]
[CanEditMultipleObjects]
public class CameraTrackEditor : Editor
{
    /*CameraTrack camera;
    Editor splineEditor;

    public override void OnInspectorGUI()
    {
        using(var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();

            //if (check.changed)

        }

        DrawSettingsEditor(camera.splineSettings, camera.OnSplineUpdated, ref camera.splineSettingsFoldout, ref splineEditor);
    }

    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
    {
        if (settings == null) return;

        foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);

        using(var check = new EditorGUI.ChangeCheckScope())
        {
            if (!foldout) return;

            CreateCachedEditor(settings, null, ref editor);
            editor.OnInspectorGUI();

            if (check.changed && onSettingsUpdated != null)
                onSettingsUpdated();
        }
    }*/
}
