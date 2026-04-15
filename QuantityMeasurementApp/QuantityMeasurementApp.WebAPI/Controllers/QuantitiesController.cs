using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementApp.Service.Interfaces;
using QuantityMeasurementApp.Model.DTOs;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

[ApiController]
[Route("api/[controller]")]
public class QuantitiesController : ControllerBase
{
    private readonly IQuantityService _service;

    public QuantitiesController(IQuantityService service)
    {
        _service = service;
    }

    [HttpPost("convert")]
    public IActionResult Convert(ConvertRequest req)
    {
        var result = _service.Convert(req.Source, req.TargetUnit);
        return Ok(result);
    }

    [HttpPost("add")]
    public IActionResult Add(ArithmeticRequest req)
    {
        var result = _service.Add(req.Q1, req.Q2, req.TargetUnit);
        return Ok(result);
    }

    [HttpPost("subtract")]
    public IActionResult Subtract(ArithmeticRequest req)
    {
        var result = _service.Subtract(req.Q1, req.Q2, req.TargetUnit);
        return Ok(result);
    }

    [HttpPost("divide")]
    public IActionResult Divide(ArithmeticRequest req)
    {
        return Ok(_service.Divide(req.Q1, req.Q2));
    }

    [HttpGet("history")]
    public IActionResult GetHistory()
    {
        return Ok(_service.GetHistory());
    }

    [HttpGet("HistoryCount")]
    public IActionResult GetHistoryCount()
    {
        return Ok(_service.GetHistoryCount());
    }
}