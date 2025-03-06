using Application.Dto;
using Application.Interfaces.Services;
using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/interviews")]
public class InterviewController(IInterviewService interviewService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateInterview()
    {
        var result = await interviewService.CreateInterviewAsync();
        return result.ToActionResult();
    }

    [HttpPost]
    [Route("{interviewId:guid}/questions/{questionId:guid}/result")]
    public async Task<IActionResult> SaveQuestionResult(
        [FromRoute] Guid interviewId,
        [FromRoute] Guid questionId,
        SaveQuestionResultRequestDto data)
    {
        var result = await interviewService.SaveQuestionResultAsync(interviewId, questionId, data);
        return result.ToActionResult();
    }
}