namespace ContestApi.DTOs
{
    public class SubmitAnswerDto
    {
        public Guid UserId { get; set; }
        public Guid ContestId { get; set; }

        public List<AnswerDto> Answers { get; set; }
    }
    public class AnswerDto
    {
        public Guid QuestionId { get; set; }

        public Guid OptionId { get; set; }
    }
}
