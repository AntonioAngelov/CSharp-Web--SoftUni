namespace CarDealer.Services.Models.Logs
{
    using System;
    using Domain;

    public class LogModel
    {
        public int Id { get; set; }

        public string User { get; set; }

        public LogOperation Operation { get; set; }

        public string ModifiedTable { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}
