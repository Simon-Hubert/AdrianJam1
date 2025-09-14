using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ArticleData", menuName = "ArticleData")]
public class ArticleShape : ScriptableObject
{
    #region Shape
    [Serializable]
    public struct Row
    {
        public bool[] column;
        private int _size;

        public Row(int size) : this() {
            CreateRow(size);
        }

        private void CreateRow(int size) {
            _size = size;
            column = new bool[_size];
            ClearRow();
        }

        public void ClearRow() {
            for (int i = 0; i < _size; i++) {
                column[i] = false;
            }
        }
    }

    [SerializeField] private int _columns = 0;
    [SerializeField] private int _rows = 0;
    [SerializeField] private Row[] _shape = new Row[]{};

    public int Rows {
        get => _rows;
        set => _rows = value;
    }

    public int Columns {
        get => _columns;
        set => _columns = value;
    }

    public void Clear() {
        for (int i = 0; i < Rows; i++) {
            _shape[i].ClearRow();
        }
    }

    public void CreateNewShape() {
        _shape = new Row[Rows];

        for (int i = 0; i < Rows; i++) {
            _shape[i] = new Row(Columns);
        }
    }

    public bool this[int i, int j] {
        get => _shape[i].column[j];
        set => _shape[i].column[j] = value;
    }
    #endregion
}
