using Microsoft.AspNetCore.Mvc;
using SubTrack.Application.DTOs.Subscriptions;
using SubTrack.Application.Interfaces;

namespace SubTrack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionsController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpPost]
    public async Task<ActionResult<CreateSubscriptionResponse>> CreateSubscription(
        CreateSubscriptionRequest request)
    {
        var response = await _subscriptionService.CreateSubscriptionAsync(request);

        return CreatedAtAction(
            nameof(GetSubscriptionById),
            new { id = response.Id },
            response);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<CreateSubscriptionResponse>> GetSubscriptionById(long id)
    {
        var response = await _subscriptionService.GetSubscriptionByIdAsync(id);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CreateSubscriptionResponse>>> GetAllSubscriptions()
    {
        var response = await _subscriptionService.GetAllSubscriptionsAsync();

        return Ok(response);
    }
}