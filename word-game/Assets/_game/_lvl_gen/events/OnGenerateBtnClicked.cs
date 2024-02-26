using UnityEngine;

namespace WordGame
{
    [CreateAssetMenu(menuName = "_game/events/OnGenerateBtnClicked")]
    public class OnGenerateBtnClicked : EventSO<OnGenerateBtnClicked.Data>
    {
        public int rows;
        public int columns;
        
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
