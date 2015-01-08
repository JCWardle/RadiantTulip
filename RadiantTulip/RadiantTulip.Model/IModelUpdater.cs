using System;

namespace RadiantTulip.Model
{
    public interface IModelUpdater
    {
        void Update();
        Game Game { get; }
        DateTime Time { set; get; }
        TimeSpan Increment { get; }
    }
}
