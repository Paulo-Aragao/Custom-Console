#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class CustomConsoleWindow : OdinEditorWindow
{
    private class Entry
    {
        public Logging.LogLevel Level;
        public string Message;
        public string StackTrace;
    }

    private static readonly List<Entry> entries = new List<Entry>();
    private Vector2 scrollPosition;
    private int selectedIndex = -1;

    [PropertyOrder(-2)]
    [HorizontalGroup("Toolbar", 0.2f)]
    [Button("Clear")]
    private void ClearConsole()
    {
        entries.Clear();
        selectedIndex = -1;
    }

    [PropertyOrder(-1)]
    [HorizontalGroup("Toolbar", 0.1f), ToggleLeft, LabelText("Debug")]
    public bool ShowDebug = true;
    [HorizontalGroup("Toolbar", 0.1f), ToggleLeft, LabelText("Gameplay")]
    public bool ShowGameplay = true;
    [HorizontalGroup("Toolbar", 0.1f), ToggleLeft, LabelText("Warn")]
    public bool ShowWarning = true;
    [HorizontalGroup("Toolbar", 0.1f), ToggleLeft, LabelText("Error")]
    public bool ShowError = true;
    [HorizontalGroup("Toolbar", 0.1f), ToggleLeft, LabelText("Lua")]
    public bool ShowLua = true;
    [HorizontalGroup("Toolbar", 0.1f), ToggleLeft, LabelText("Services")]
    public bool ShowServives = true;

    [MenuItem("Window/Custom Logging Console")]
    private static void Open()
    {
        GetWindow<CustomConsoleWindow>("MyConsole").Show();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Logging.OnLogEmitted += HandleLog;
    }

    protected override void OnDisable()
    {
        Logging.OnLogEmitted -= HandleLog;
        base.OnDisable();
    }

    private void HandleLog(string message, Logging.LogLevel level, string stackTrace)
    {
        entries.Add(new Entry
        {
            Level      = level,
            Message    = message,
            StackTrace = stackTrace
        });
        Repaint();
    }

    [OnInspectorGUI]
    private void DrawConsole()
    {
        GUILayout.Space(5);

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        for (int i = 0; i < entries.Count; i++)
        {
            var e = entries[i];

            if ((e.Level == Logging.LogLevel.Debug   && !ShowDebug)   ||
                (e.Level == Logging.LogLevel.Gameplay    && !ShowGameplay)    ||
                (e.Level == Logging.LogLevel.Warning && !ShowWarning) ||
                (e.Level == Logging.LogLevel.Error   && !ShowError) ||
                (e.Level == Logging.LogLevel.Lua     && !ShowLua) ||
                (e.Level == Logging.LogLevel.Services && !ShowServives))
            {
                continue;
            }
            
            var style = new GUIStyle(EditorStyles.label)
            {
                richText = true,
                normal   = { textColor = GetColorForLevel(e.Level) }
            };
            
            if (i == selectedIndex)
            {
                var highlight = new Color(0.2f, 0.4f, 0.6f, 0.3f);
                EditorGUILayout.BeginVertical(GUI.skin.box);
                GUI.backgroundColor = highlight;
            }
            
            if (GUILayout.Button($"[{e.Level}] {e.Message}", style))
            {
                selectedIndex = i;
            }

            if (i == selectedIndex)
            {
                GUI.backgroundColor = Color.white;
                EditorGUILayout.EndVertical();
            }
        }
        EditorGUILayout.EndScrollView();
        
        if (selectedIndex >= 0 && selectedIndex < entries.Count)
        {
            GUILayout.Space(8);
            GUILayout.Label("Details:", EditorStyles.boldLabel);

            EditorGUILayout.SelectableLabel(
                entries[selectedIndex].Message + "\n\n" + entries[selectedIndex].StackTrace,
                EditorStyles.textArea,
                GUILayout.Height(120),
                GUILayout.ExpandWidth(true)
            );
        }
    }

    private Color GetColorForLevel(Logging.LogLevel level)
    {
        switch (level)
        {
            case Logging.LogLevel.Debug:   return new Color(0.5f, 0.5f, 1f);
            case Logging.LogLevel.Gameplay:    return new Color(0.76f, 0.76f, 0.76f);
            case Logging.LogLevel.Services:    return new Color(0.65f, 0.4f, 0.05f);
            case Logging.LogLevel.System:     return new Color(0.16f, 0.5f, 0.16f);
            case Logging.LogLevel.Warning: return Color.yellow;
            case Logging.LogLevel.Error:   return Color.red;
            case Logging.LogLevel.Lua:     return new Color(0.93f, 0.65f, 1f);
            default:                       return Color.white;
        }
    }
}
#endif
