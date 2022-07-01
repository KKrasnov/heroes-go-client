using System;

namespace General
{
    public interface IIdentifiable 
    {
        Guid ID
        {
            get;
            set;
        }
    }
}