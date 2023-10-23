// using Tuwaiq.RoomManagement.Application.Interfaces;
// using Tuwaiq.RoomManagement.Domain.Events;
// using Tuwaiq.RoomManagement.Shared.Domain;
// using MediatR;
// using Microsoft.Extensions.Logging;
//
// namespace Tuwaiq.RoomManagement.Application.Handlers.Events;
//
// public class UpdateAssetTypeStockEventHandler : IRequestHandler<DomainEventRequest<UpdateAssetTypeStockEvent>>
// {
//     private readonly ILogger<UpdateAssetTypeStockEventHandler> _log;
//     private readonly IUnitOfWork _unitOfWork;
//
//     public UpdateAssetTypeStockEventHandler(ILogger<UpdateAssetTypeStockEventHandler> log,IUnitOfWork unitOfWork  )
//     {
//         _log = log;
//         _unitOfWork = unitOfWork;
//     }
//
//
//     public async Task<Unit> Handle(DomainEventRequest<UpdateAssetTypeStockEvent> notification, CancellationToken cancellationToken)
//     {
//         var domainEvent = notification.DomainEvent;
//         try
//         {
//             _unitOfWork.AssetTypeEmployees!.Update(notification.DomainEvent.Item);
//             await _unitOfWork.SaveChangesAsync();
//         }
//         catch (Exception exc)
//         {
//             _log.LogError(exc, "Error handling UpdateCity event {domainEvent}", domainEvent.GetType());
//             throw;
//         }
//
//         return default;
//     }
// }

using Application.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Domain;

namespace Application.Handlers.Events;

public class AssetTransferredEventHandler : IRequestHandler<DomainEventRequest<AssetTransferredEvent>>
{
    private readonly ILogger<AssetTransferredEventHandler> _log;
    private readonly IUnitOfWork _unitOfWork;

    public AssetTransferredEventHandler(ILogger<AssetTransferredEventHandler> log, IUnitOfWork unitOfWork)
    {
        _log = log;
        _unitOfWork = unitOfWork;
    }

    //
    // public void Handle(DomainEventRequest<AssetTransferredEvent> notification, CancellationToken cancellationToken)
    // {
    //     
    // }
    public Task Handle(DomainEventRequest<AssetTransferredEvent> request, CancellationToken cancellationToken)
    {
        var domainEvent = request.DomainEvent;
        try
        {
            //send notification to the user
            
            _log.LogInformation("Asset Transferred from {fromRoomId} to {toRoomId}", domainEvent.FromRoomId, domainEvent.ToRoomId);
        }
        catch (Exception exc)
        {
            _log.LogError(exc, "Error handling UpdateCity event {domainEvent}", domainEvent.GetType());
            throw;
        }
        
        return Task.CompletedTask;
    }
}