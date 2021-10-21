using System;
using SQLite;

namespace DartsPractice.Models
{
    public class RVBScore
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime Time { get; set; }
        public int Twenties{get;set;}
        public int Nineteens { get; set; }
        public int Eighteens { get; set; }
        public int Seventeens { get; set; }
        public int Sixteens { get; set; }
        public int Fifteens { get; set; }
        public int Bulls { get; set; }
        public int ScoreTotal { get; set; }

        public RVBScore()
        {
        }
    }
}
