using Microsoft.AspNetCore.Mvc;
using WebAPI_PIS_6sem.Services;

namespace WebAPI_PIS_6sem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RulesController : ControllerBase
{
    private readonly ServiceRule _serviceRule;

    public RulesController(ServiceRule serviceRule)
    {
        _serviceRule = serviceRule;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateRuleRequest request)
    {
        // Преобразуем DTO в параметры сервиса
        var rule = _serviceRule.CreateRule(
            request.RuleName,
            request.TargetDocs,
            request.GuidanceDescription,
            request.Refusal,
            request.OrgNames,
            request.OrgAddresses,
            request.DaysList,
            request.PurposeNamesList,
            request.CitizenshipNamesList,
            request.PropertyNames,
            request.PropertyValues);

        return Ok(rule);
    }
}

// DTO для приёма JSON от клиента
public class CreateRuleRequest
{
    public string RuleName { get; set; } = "";
    public List<string> TargetDocs { get; set; } = new();
    public string GuidanceDescription { get; set; } = "";
    public string Refusal { get; set; } = "";
    public List<string> OrgNames { get; set; } = new();
    public List<string> OrgAddresses { get; set; } = new();
    public List<int> DaysList { get; set; } = new();
    public List<List<string>> PurposeNamesList { get; set; } = new();
    public List<List<string>> CitizenshipNamesList { get; set; } = new();
    public List<List<string>> PropertyNames { get; set; } = new();
    public List<List<string>> PropertyValues { get; set; } = new();
}