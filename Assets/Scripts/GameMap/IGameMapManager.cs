using System;

public interface IGameMapManager
{
    void RefreshMap();
    void RefreshMapEntry(Guid entryId);
}
