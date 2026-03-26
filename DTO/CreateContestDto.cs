namespace ContestSystem.API.DTOs;

public class CreateContestDto
{
    public string Title { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}

public class ContestQuestionsDto
{
    public Guid ContestId { get; set; }

    public string Title { get; set; }

    public List<QuestionDto> Questions { get; set; }
}

public class QuestionDto
{
    public Guid Id { get; set; }

    public string Text { get; set; }

    public List<OptionDto> Options { get; set; }
}

public class OptionDto
{
    public Guid Id { get; set; }

    public string Text { get; set; }
}