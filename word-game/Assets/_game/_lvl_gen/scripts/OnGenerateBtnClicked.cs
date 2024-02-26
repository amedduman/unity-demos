using System;
using UnityEngine;

namespace WordGame
{
    [CreateAssetMenu(menuName = "_game/events/OnGenerateBtnClicked")]
    public class OnGenerateBtnClicked : EventSO<OnGenerateBtnClicked.Data>
    {
        int rows;
        int columns;

        void OnEnable()
        {
            rows = 0;
            columns = 0;
        }

        public void CallTheEvent()
        {
            Invoke(new Data(rows, columns));
        }

        public void SetRowsValue(string txt)
        {
            int.TryParse(txt, out rows);
        }

        public void SetColumnsValue(string txt)
        {
            int.TryParse(txt, out columns);
        }
        
        public struct Data
        {
            public int rows { get; private set; }
            public int columns { get; private set; }

            public Data(int rows, int columns)
            {
                this.rows = rows;
                this.columns = columns;
            }
        }
    }
}
