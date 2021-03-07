namespace project_alfred.models
{
    public class SongRecord
    {
        public int ID { get; set; }
        public string url { get; set; }
        /**
         * Discord User ID
         */
        public ulong user { get; set; }
    }
}