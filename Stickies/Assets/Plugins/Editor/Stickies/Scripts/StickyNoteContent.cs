﻿#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

namespace DeadMosquito.Stickies
{
    public class StickyNoteContent : PopupWindowContent
    {
        const float DefaultSize = 320f;
        const float HeaderSize = 32f;

        Vector2 _scroll = Vector2.zero;

        const float ColorPickerHeight = 48f;

        string _guid;

        string _text = string.Empty;

        Vector3[] m_RectVertices = new Vector3[4];

        GUIStyle m_MiddleCenterStyle;

        public StickyNoteContent(string guid)
        {
            Debug.Log("GUID: " + guid);
            _guid = guid;

            m_MiddleCenterStyle = new GUIStyle(EditorStyles.miniLabel) {alignment = TextAnchor.MiddleCenter};
        }

        public override Vector2 GetWindowSize()
        {
            return new Vector2(DefaultSize, DefaultSize);
        }

        public override void OnGUI(Rect rect)
        {
            var c = Colors.ColorById(NoteColor.Lemon);
            StickiesGUI.DrawRectNote(rect, c.main, c.header);

//            DrawHeader(rect);
//            DrawColorPicker(new Rect(rect.x, rect.y, rect.width, ColorPickerHeight));
            DrawNoteText(rect);
            editorWindow.Repaint();
            GUI.skin = null;
        }

        void DrawNoteText(Rect rect)
        {
            var textAreaRect = new Rect(rect.x, rect.y + HeaderSize, rect.width, rect.height - HeaderSize);
            GUILayout.BeginArea(textAreaRect);
            EditorGUILayout.BeginVertical();

            _scroll = EditorGUILayout.BeginScrollView(_scroll, GUIStyle.none, StickiesStyles.VerticalScrollbar);
            _text = EditorGUILayout.TextArea(_text, StickiesStyles.TextArea);
            EditorGUILayout.EndScrollView();

            EditorGUILayout.EndVertical();
            GUILayout.EndArea();
        }

        void DrawHeader(Rect whole)
        {
            Handles.DrawSolidRectangleWithOutline(new Rect(whole.x, whole.y, whole.width, HeaderSize), Color.red, Color.clear);
        }

        void DrawColorPicker(Rect rect)
        {
            var colors = Colors.Values;
            for (int i = 0; i < colors.Length; i++)
            {
                var noteColors = Colors.ColorById(colors[i]);
                if (StickiesGUI.ColorButton(new Rect(15 + i * 32, rect.y, 32, 32), noteColors.main,
                    noteColors.chooserOutline))
                {
                    Debug.Log("Color click");
                }
            }
        }

        public override void OnOpen()
        {
        }

        public override void OnClose()
        {
        }
    }
}
#endif