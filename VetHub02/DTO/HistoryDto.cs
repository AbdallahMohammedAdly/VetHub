using VetHub02.Core.Entities;

namespace VetHub02.DTO
{
    public class HistoryDto
    {
        public int? Id { get; set; }
        public string ImageUrl { get; set; }

        public string PredictedName { get; set; }

        public string Cause { get; set; }

        public string Symptoms { get; set; }

        public string Transmission { get; set; }

        public string Prevention { get; set; }

        public string Treatment { get; set; }

        public DateTime DateOfDetect { get;} = DateTime.Now;

        public int UserId { get; set; }

    }
}
