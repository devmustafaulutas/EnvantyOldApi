namespace envantyService.Models
{
    public class SurveyAnswer
    {
        public int Id { get; set; }
        public string ?SurveyAnswerMetaData { get; set; }
        public DateTime ?CreateDate { get; set; }
        public string ?UserName { get; set; }
        public string ?UserId { get; set; }
        public bool ?Status { get; set; }

        public string? SurveyNo { get; set; }
    }
}
