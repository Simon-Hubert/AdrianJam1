using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


[CustomEditor(typeof(ArticleShape)), CanEditMultipleObjects]
public class ArticleShapeEditor : Editor
{
    private ArticleShape Instance => target as ArticleShape;

    public override void OnInspectorGUI() {
        serializedObject.Update();

        ClearShapeButton();
        EditorGUILayout.Space();
        DrawColumnsInputFields();
        EditorGUILayout.Space();

        if (Instance. Columns > 0 && Instance.Rows > 0) {
            DrawShapeTable();
        }
        serializedObject.ApplyModifiedProperties();

        if (GUI.changed) {
            EditorUtility.SetDirty(Instance);
        }
    }

    private void ClearShapeButton() {
        if (GUILayout.Button("Clear Shape")) {
            Instance.Clear();
        }
    }

    private void DrawColumnsInputFields() {
        int columnTemp = Instance.Columns;
        int rowTemp = Instance.Rows;

        Instance.Columns = EditorGUILayout.IntField("Rows", Instance.Columns);
        Instance.Rows = EditorGUILayout.IntField("Columns", Instance.Rows);

        if ((Instance.Columns != columnTemp || Instance.Rows != rowTemp) &&
            Instance.Columns > 0 && Instance.Rows > 0) {
            Instance.CreateNewShape();
        }
    }

    private void DrawShapeTable() {
        GUIStyle tableStyle = new GUIStyle("box") {
            padding = new RectOffset(10, 10, 10, 10),
            margin = {
                left = 32
            }
        };

        GUIStyle headerColumnStyle = new GUIStyle {
            fixedWidth = 65,
            alignment = TextAnchor.MiddleCenter
        };

        GUIStyle rowStyle = new GUIStyle {
            fixedHeight = 25,
            alignment = TextAnchor.MiddleCenter
        };

        GUIStyle dataFieldStyle = new GUIStyle {
            normal = {
                background = Texture2D.grayTexture
            },
            onNormal = {
                background = Texture2D.whiteTexture
            }
        };

        for (int column = 0; column < Instance.Columns; column++) {
            EditorGUILayout.BeginHorizontal(headerColumnStyle);

            for (int row = 0; row < Instance.Rows; row++) {
                EditorGUILayout.BeginHorizontal(rowStyle);
                bool data = EditorGUILayout.Toggle(Instance[row, column], dataFieldStyle);
                Instance[row, column] = data;
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
