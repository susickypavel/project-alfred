using System;

namespace project_alfred.models
{
    public class SongRecord
    {
        public string url { get; set; }
        /**
         * Discord User ID
         */
        public ulong user { get; set; }

        public DateTime publishedAt { get; set; }
    }
}