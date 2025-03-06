namespace Application.Dto;

public record AnswerResponseDto(Guid Guid, string Text);

public record QuestionResponseDto(string Text, IEnumerable<AnswerResponseDto> Answers);