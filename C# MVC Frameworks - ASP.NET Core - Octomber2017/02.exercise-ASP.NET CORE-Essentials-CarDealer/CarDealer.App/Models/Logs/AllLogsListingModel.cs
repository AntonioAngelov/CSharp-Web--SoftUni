namespace CarDealer.App.Models.Logs
{
    using System.Collections.Generic;
    using Services.Models.Logs;

    public class AllLogsListingModel
    {
        public string Query { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public int PreviousPage
            => this.CurrentPage - 1 > 0 ? this.CurrentPage - 1 : this.CurrentPage;

        public int NextPage
            => this.CurrentPage + 1 <= this.TotalPages ? this.CurrentPage + 1 : this.CurrentPage;

        public IEnumerable<LogModel> Logs { get; set; }
    }
}
