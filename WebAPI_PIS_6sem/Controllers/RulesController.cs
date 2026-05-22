using Microsoft.AspNetCore.Mvc;
using WebAPI_PIS_6sem.Data;
using WebAPI_PIS_6sem.Models;
using WebAPI_PIS_6sem.Services;

namespace WebAPI_PIS_6sem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RulesController : ControllerBase
    {
        private readonly ServiceRule _serviceRule;
        private readonly IUnitOfWork _unitOfWork;

        public RulesController(ServiceRule serviceRule, IUnitOfWork unitOfWork)
        {
            _serviceRule = serviceRule;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateRuleRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.RuleName))
                return BadRequest("Название правила не может быть пустым.");

            if (request.DaysList.Count != request.PurposeNamesList.Count ||
                request.DaysList.Count != request.CitizenshipNamesList.Count ||
                request.DaysList.Count != request.PropertyNames.Count ||
                request.DaysList.Count != request.PropertyValues.Count)
            {
                return BadRequest("Количество элементов в списках профилей не совпадает.");
            }

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

            return CreatedAtAction(nameof(GetById), new { id = rule.Id }, rule);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var rule = _unitOfWork.Rules.GetById(id);

            if (rule == null)
                return NotFound($"Правило с ID={id} не найдено.");

            return Ok(rule);
        }
    }
}