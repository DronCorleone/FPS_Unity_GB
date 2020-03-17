using System;

namespace Geekbrains
{
    [Serializable]
    public class AudioSettings
    {
        public float Master { get; set; }
        public float Music { get; set; }
        public float Effects { get; set; }
    }
}
