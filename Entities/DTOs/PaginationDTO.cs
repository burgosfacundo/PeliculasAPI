namespace PeliculasAPI.Entities.DTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;

        public int recordsPerPage = 10;
        public readonly int maxRecordsPerPage = 50;

        public int RecordsPerPage
        {
            get => recordsPerPage;
            set => recordsPerPage = (value > recordsPerPage) ? recordsPerPage : value;
        }
    }
}
