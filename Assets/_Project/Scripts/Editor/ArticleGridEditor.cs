using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ArticleGrid))]
public class ArticleGridEditor : Editor
{
    private ArticleGrid Instance => target as ArticleGrid;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (Application.isPlaying) {
            EditorGUILayout.Space();
            DrawShapeTable();
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

        for (int column = 0; column < Instance.GridHalfSize.y * 2; column++) {
            EditorGUILayout.BeginHorizontal(headerColumnStyle);
            for (int row = 0; row < Instance.GridHalfSize.x * 2; row++) {
                EditorGUILayout.BeginHorizontal(rowStyle);
                EditorGUILayout.Toggle(Instance[row, column].Occupied, dataFieldStyle);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
