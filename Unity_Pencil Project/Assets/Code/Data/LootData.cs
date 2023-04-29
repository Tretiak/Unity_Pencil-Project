using System;

namespace Code.Data
{
    [Serializable]
    public class LootData
    {
        public int Collected;
        public Action Changed;

        public LootPieceDataDictionary LootPiecesOnScene = new LootPieceDataDictionary();
        public void Collect(Loot loot)
        {
            Collected += loot.Value;
            Changed?.Invoke();
        }
    }
    
    
}