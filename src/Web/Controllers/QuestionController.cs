using Application.Interfaces.Services;
using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("/api/questions")]
public class QuestionController(IQuestionService questionService) : ControllerBase
{
    [HttpGet]
    [Route("{questionId:guid}")]
    public async Task<IActionResult> GetCurrentQuestion([FromRoute] Guid questionId)
    {
        var result = await questionService.GetQuestionByPublicIdAsync(questionId);
        return result.ToActionResult();
    }
}