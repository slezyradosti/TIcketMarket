using Application.Core;
using Application.DTOs.Catalogues;
using Application.DTOs.Users.HTTP;
using AutoMapper;
using Domain.Repositories.Repos.Interfaces.Catalogues;

namespace Application.Handlers.Catalogues.TicketDiscount;

public class TicketDiscountHandler : ITicketDiscountHandler
{
    private readonly IMapper _mapper;
    private readonly ITicketDiscountRepository _ticketDiscountRepository;
    private readonly IUserAccessor _userAccessor;

    public TicketDiscountHandler(ITicketDiscountRepository ticketDiscountRepository, IMapper mapper,
        IUserAccessor userAccessor)
    {
        _ticketDiscountRepository = ticketDiscountRepository;
        _mapper = mapper;
        _userAccessor = userAccessor;
    }

    public async Task<Result<List<TicketDiscountDto>>> GetCustomersTicketDiscountListAsync()
    {
        var userId = _userAccessor.GetUserId();

        var ticketDiscounts = await _ticketDiscountRepository.GetSelletsDiscountListSortedAsync(userId);

        var ticketDiscountDtos = new List<TicketDiscountDto>();

        _mapper.Map(ticketDiscounts, ticketDiscountDtos);

        return Result<List<TicketDiscountDto>>.Success(ticketDiscountDtos);
    }

    // public async Task<Result<TicketDto>> GetCustomersTicketAsync(Guid ticketId)
    // {
    //     if (!await _ticketOrderRepository.HasUserAccessToTheTicketOrderAsync(ticketId,
    //             _userAccessor.GetUserId()))
    //     {
    //         return Result<TicketDto>.Failure("You have no access to this data");
    //     }
    //
    //     var ticket = await _ticketRepository.GetOneAsync(ticketId);
    //
    //     TicketDto ticketDto = new TicketDto();
    //
    //     _mapper.Map(ticket, ticketDto);
    //
    //     return Result<TicketDto>.Success(ticketDto);
    // }

    public async Task<Result<string>> CreateSellersOneAsync(TicketDiscountDto ticketDiscountDto)
    {
        var ticketDiscount = new Domain.Models.Catalogues.TicketDiscount();
        _mapper.Map(ticketDiscountDto, ticketDiscount);

        var result = await _ticketDiscountRepository.AddAsync(ticketDiscount) > 0;

        if (!result) return Result<string>.Failure("Failed to create TicketDiscount");

        return Result<string>.Success("Successfully");
    }

    // public async Task<Result<string>> EditCustomersOneAsync(TicketDto ticketDto)
    // {
    //     var ticket = await _ticketRepository.GetOneAsync(ticketDto.Id);
    //
    //     if (ticket == null) return null;
    //
    //     var userId = _userAccessor.GetUserId();
    //
    //     if (!await _ticketOrderRepository.HasUserAccessToTheTicketOrderAsync(ticketDto.Id.Value, userId))
    //     {
    //         return Result<string>.Failure("You have no access to this data");
    //     }
    //
    //     _mapper.Map(ticketDto, ticket);
    //
    //     var result = await _ticketRepository.SaveAsync(ticket) > 0;
    //
    //     if (!result) return Result<string>.Failure("Failed to update Ticket");
    //
    //     return Result<string>.Success("Successfully");
    // }
    //
    // public async Task<Result<string>> DeleteCustomersOneAsync(Guid ticketId)
    // {
    //     var ticket = await _ticketRepository.GetOneAsync(ticketId);
    //
    //     if (ticket == null) return null;
    //
    //     if (!await _ticketOrderRepository.HasUserAccessToTheTicketOrderAsync(ticketId, _userAccessor.GetUserId()))
    //     {
    //         return Result<string>.Failure("You have no access to this data");
    //     }
    //
    //     var result = await _ticketRepository.RemoveAsync(ticket) > 0;
    //
    //     if (!result) return Result<string>.Failure("Failed to delete Ticket");
    //
    //     return Result<string>.Success("Successfully");
    // }
}