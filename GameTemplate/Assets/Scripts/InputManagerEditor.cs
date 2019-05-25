using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(InputManager))]
public class InputManagerEditor : Editor
{
    private class InputMap
    {
        public string mapName;

        public string newAxis;
        public string newButton;

        public List<string> mapAxes;
        public List<string> mapButtons;

        public InputMap()
        {
            newAxis = string.Empty;
            newButton = string.Empty;

            mapAxes = new List<string>();
            mapButtons = new List<string>();
        }

        public void AddNewAxis()
        {
            if (newAxis != string.Empty && mapAxes.Contains(newAxis) == false)
            {
                mapAxes.Add(newAxis);
                newAxis = string.Empty;
            }
        }

        public void AddNewButton()
        {
            if (newButton != string.Empty && mapButtons.Contains(newButton) == false)
            {
                mapButtons.Add(newButton);  
                newButton = string.Empty;
            }
        }

        public void MoveAxisUp(int index)
        {
            var action = mapAxes[index];
            mapAxes.RemoveAt(index);
            mapAxes.Insert(Mathf.Max(0, index - 1), action);
        }

        public void MoveAxisDown(int index)
        {
            var action = mapAxes[index];
            mapAxes.RemoveAt(index);
            mapAxes.Insert(Mathf.Min(mapAxes.Count, index + 1), action);
        }

        public void MoveButtonUp(int index)
        {
            var action = mapButtons[index];
            mapButtons.RemoveAt(index);
            mapButtons.Insert(Mathf.Max(0, index - 1), action);
        }

        public void MoveButtonDown(int index)
        {
            var action = mapButtons[index];
            mapButtons.RemoveAt(index);
            mapButtons.Insert(Mathf.Min(mapButtons.Count, index + 1), action);
        }
    }

    bool mapFoldout;
    bool mapCreationFoldout;

    InputMap newMap = new InputMap();

    private static GUIContent moveUpButton = new GUIContent("\x2191", "Move Up");
    private static GUIContent moveDownButton = new GUIContent("\x2193", "Move Down");
    private static GUIContent removeButton = new GUIContent("Remove", "Remove item");

    private int miniButtonWidth = 20;

    public override void OnInspectorGUI()
    {
        mapFoldout = EditorGUILayout.Foldout(mapFoldout, "Input Maps");

        if (mapFoldout)
        {
            EditorGUI.indentLevel++;

            mapCreationFoldout = EditorGUILayout.Foldout(mapCreationFoldout, "Create New Map");

            if(mapCreationFoldout)
            {
                EditorGUI.indentLevel++;

                // Axis and Button list titles
                EditorGUILayout.BeginHorizontal();
                GUIStyle style = new GUIStyle() { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold};
                EditorGUILayout.LabelField("Axis Actions", style);
                EditorGUILayout.LabelField("Button Actions", style);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();

                for(int i = 0; i < Mathf.Max(newMap.mapAxes.Count, newMap.mapButtons.Count); i++)
                {
                    EditorGUILayout.BeginHorizontal();

                    EditorGUIUtility.labelWidth = (EditorGUIUtility.currentViewWidth / 2) - (EditorGUI.indentLevel * 10) - (2 * miniButtonWidth) - GUI.skin.button.CalcSize(removeButton).x;
                    EditorGUILayout.PrefixLabel(i < newMap.mapAxes.Count ? newMap.mapAxes[i] : " ");
                    if (i < newMap.mapAxes.Count)
                    {
                        if (GUILayout.Button(moveUpButton, EditorStyles.miniButtonLeft, GUILayout.Width(20)))
                            newMap.MoveAxisUp(i);
                        if (GUILayout.Button(moveDownButton, EditorStyles.miniButtonMid, GUILayout.Width(20)))
                            newMap.MoveAxisDown(i);
                        if (GUILayout.Button(removeButton, EditorStyles.miniButtonRight, GUILayout.MaxWidth(GUI.skin.button.CalcSize(removeButton).x)))
                            newMap.mapAxes.RemoveAt(i);
                    }
                    else
                    {
                        GUILayout.Button("", new GUIStyle(GUI.skin.label), GUILayout.Width((2 * miniButtonWidth) + GUI.skin.button.CalcSize(removeButton).x));
                    }

                    EditorGUIUtility.labelWidth += 20;
                    EditorGUILayout.PrefixLabel(i < newMap.mapButtons.Count ? newMap.mapButtons[i] : " ");
                    if (i < newMap.mapButtons.Count)
                    {
                        if (GUILayout.Button(moveUpButton, EditorStyles.miniButtonLeft, GUILayout.Width(20)))
                            newMap.MoveButtonUp(i);
                        if (GUILayout.Button(moveDownButton, EditorStyles.miniButtonMid, GUILayout.Width(20)))
                            newMap.MoveButtonDown(i);
                        if (GUILayout.Button(removeButton, EditorStyles.miniButtonRight, GUILayout.MaxWidth(GUI.skin.button.CalcSize(removeButton).x)))
                            newMap.mapButtons.RemoveAt(i);
                    }

                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                    EditorGUIUtility.labelWidth = GUI.skin.label.CalcSize(new GUIContent("New Axis")).x + ((EditorGUI.indentLevel + 1) * 10);
                    newMap.newAxis = EditorGUILayout.TextField("New Axis", newMap.newAxis);
                    EditorGUIUtility.labelWidth = GUI.skin.label.CalcSize(new GUIContent("New Button")).x + ((EditorGUI.indentLevel + 1) * 10);
                    newMap.newButton = EditorGUILayout.TextField("New Button", newMap.newButton);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(EditorGUI.indentLevel * 15);
                    if(GUILayout.Button("Add New Axis"))
                    {
                        newMap.AddNewAxis();
                        Repaint();
                    }

                    GUILayout.Space(EditorGUI.indentLevel * 15);
                    if (GUILayout.Button("Add New Button"))
                    {
                        newMap.AddNewButton();
                        Repaint();
                    }
                EditorGUILayout.EndHorizontal();
            }
        }


    }
}
