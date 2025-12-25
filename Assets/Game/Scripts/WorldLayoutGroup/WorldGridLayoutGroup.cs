using Sirenix.OdinInspector;
using UnityEngine;

namespace WorldLayoutGroup
{
    public class WorldGridLayoutGroup : MonoBehaviour
    {
        [SerializeField, MinValue(0f)] private Vector2 _spacing;
        [SerializeField, MinValue(0f)] private Vector2 _cellSize;
        [SerializeField, MinValue(1)] private int _columns;
        [SerializeField] private bool _isUpdateOnValidate;

        public int Columns
        {
            get => _columns;
            set
            {
                if (value > 0)
                {
                    _columns = value;
                }
            }
        }

        private void OnValidate()
        {
            if (_isUpdateOnValidate)
            {
                UpdateLayout();
            }
        }

        public void UpdateLayout()
        {
            Transform[] children = GetChildren();
            int totalChildren = children.Length;

            if (_columns <= 0 || totalChildren <= 0)
            {
                return;
            }

            int totalRows = Mathf.CeilToInt((float)totalChildren / _columns);

            float horizontalStep = _cellSize.x + _spacing.x;
            float verticalStep = _cellSize.y + _spacing.y;

            float totalWidth = (_columns - 1) * horizontalStep;
            float totalHeight = (totalRows - 1) * verticalStep;

            float startHorizontal = -totalWidth / 2f;
            float startVertical = totalHeight / 2f;

            for (int i = 0; i < totalChildren; i++)
            {
                int rowIndex = i / _columns;
                int columnIndex = i % _columns;

                float horizontalPosition = startHorizontal + columnIndex * horizontalStep;
                float verticalPosition = startVertical - rowIndex * verticalStep;

                children[i].localPosition = ConvertToAxisSpace(horizontalPosition, verticalPosition);
            }
        }

        private Vector3 ConvertToAxisSpace(float horizontalPosition, float verticalPosition)
        {
            //axis -> YZ
            return new Vector3(0f, verticalPosition, horizontalPosition);
        }

        private Transform[] GetChildren()
        {
            Transform[] children = new Transform[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                children[i] = transform.GetChild(i);
            }

            return children;
        }
    }
}