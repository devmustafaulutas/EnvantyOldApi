﻿namespace envantyefe.Entityframeworks
{
    public class DailyNews
    {

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Type { get; set; }
        public string? ImagePath { get; set; }
        public DateTime ShareDate { get; set; }
        public bool Status { get; set; }

    }
}
