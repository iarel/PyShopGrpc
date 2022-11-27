using Application.AppServices;
using Billing;
using Contracts.Requests;
using Grpc.Core;

namespace API.Services;

public class BillingService : Billing.Billing.BillingBase
{
    private readonly IBillingAppService _appService;
    private readonly Dictionary<Contracts.Enums.TransactionStatus, Response.Types.Status> _statusMapper = new ()
    {
        { Contracts.Enums.TransactionStatus.Ok, Response.Types.Status.Ok },
        { Contracts.Enums.TransactionStatus.Failed, Response.Types.Status.Failed },
        { Contracts.Enums.TransactionStatus.Unspecified, Response.Types.Status.Unspecified },
    };

    public BillingService(IBillingAppService appService)
    {
        _appService = appService;
    }

    public async override Task<Response> CoinsEmission(EmissionAmount request, ServerCallContext context)
    {
        var result = await _appService.CoinsEmission(request.Amount);
        return new Response { Status = _statusMapper[result.Status], Comment = result.Comment };
    }

    public async override Task ListUsers(None request, IServerStreamWriter<UserProfile> responseStream, ServerCallContext context)
    {
        foreach(var user in await _appService.ListUsers())
        {
            await responseStream.WriteAsync(new UserProfile { Name = user.Name, Amount = user.Amount });
        }
    }

    public async override Task<Coin> LongestHistoryCoin(None request, ServerCallContext context)
    {
        var result = await _appService.LongestHistoryCoin();
        return new Coin { Id = result.Id, History = result.History };
    }

    public async override Task<Response> MoveCoins(MoveCoinsTransaction request, ServerCallContext context)
    {
        var result = await _appService.MoveCoins(new TransactionRequest(request.SrcUser, request.DstUser, request.Amount));
        return new Response{ Status = _statusMapper[result.Status], Comment = result.Comment };
    }
}