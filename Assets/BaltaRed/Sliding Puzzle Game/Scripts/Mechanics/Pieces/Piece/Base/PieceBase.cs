using UnityEngine;
using System.Collections.Generic;

namespace BaltaRed.SlidingPuzzle.GameCore
{
    public class PieceBase : MonoBehaviour
    {
        protected int _row = 0;
        protected int _collumn = 0;

        protected int _index = -1;

        protected Dictionary<int, int> _dictRowCollum = new Dictionary<int, int>();

        public int Row => _row;
        public int Collumn => _collumn;
        public Dictionary<int, int> DictRowCollum => _dictRowCollum;

        public virtual int Index
        {
            get => _index;
            set
            {
                _index = value;
            }
        }

        public void SetRowCollumn(int row, int collumn)
        {
            _dictRowCollum.Clear();

            _row = row;
            _collumn = collumn;

            _dictRowCollum.Add(row, collumn);
        }
    }
}