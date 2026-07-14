namespace Game.SaveSystem
{
    /// <summary>
    /// Interface that any savable component (Inventory, Equipment, BuildingPiece) must implement.
    /// </summary>
    public interface ISaveable
    {
        string GetSaveID(); // Unique ID for this component (e.g. "InventoryContainer")
        
        object SaveState(); // Returns a struct/class representing the state
        
        void LoadState(object state); // Receives the struct/class and restores state
    }
}
