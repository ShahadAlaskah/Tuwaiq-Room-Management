// using Tuwaiq.RoomManagement.Domain.Domains;
// using Tuwaiq.RoomManagement.Shared.Domain;
//
// namespace Tuwaiq.RoomManagement.Domain.Events;
//
// public class UpdateAssetTypeStockEvent : IDomainEvent
// {
//     public AssetTypeStock Item { get; }
//
//     public UpdateAssetTypeStockEvent(AssetTypeStock item)
//     {
//         Item = item;
//     }
//
// }

using Shared.Domain;
using Shared.Ids;

namespace Domain.Events;

public class AssetTransferredEvent : IDomainEvent
{
    public RoomId FromRoomId { get; }
    public RoomId ToRoomId { get; }

    public AssetTransferredEvent(RoomId fromRoomId, RoomId toRoomId)
    {
        FromRoomId = fromRoomId;
        ToRoomId = toRoomId;
    }
}