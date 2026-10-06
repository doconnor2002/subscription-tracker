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

        return StatusCode(StatusCodes.Status201Created, response);
    }
}