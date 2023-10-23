using Application.Commands.AuditViews.Commands;
using Application.Interfaces;
using Domain.Base;
using Shared.Ids;
using Shared.Base;
using Shared.Interfaces;

namespace Application.Commands.AuditViews.CommandHandlers;

public class CreateAuditViewCommandHandler : ICommandHandler<CreateAuditViewCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAuditViewCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateAuditViewCommand request, CancellationToken cancellationToken)
    {

        var userId = request.Userid;
        var entity = new AuditView
        {
            Args = request.Args,
            Description = request.Description,
            Type = request.Type,
            DateTime = DateTime.Now,
            Tables = request.Tables
        };

        if (userId != Guid.Empty)
            if (userId != null)
                entity.UserId = new UserId(userId.Value.ToString());
        await _unitOfWork.AuditViews?.CreateAsync(entity, cancellationToken)!;
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}