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

    public async Task<Result<TicketDiscountDto>> GetCustomersDiscountAsync(Guid ticketDiscountId)
    {
        if (!await _ticketDiscountRepository.HasUserAccessToTheTicketDiscountAsync(ticketDiscountId,
                _userAccessor.GetUserId()))
        {
            return Result<TicketDiscountDto>.Failure("You have no access to this data");
        }
    
        var ticketDiscount = await _ticketDiscountRepository.GetOneAsync(ticketDiscountId);
    
        TicketDiscountDto ticketDiscountDto = new TicketDiscountDto();
    
        _mapper.Map(ticketDiscount, ticketDiscountDto);
    
        return Result<TicketDiscountDto>.Success(ticketDiscountDto);
    }

    public async Task<Result<string>> CreateSellersOneAsync(TicketDiscountDto ticketDiscountDto)
    {
        var ticketDiscount = new Domain.Models.Catalogues.TicketDiscount();
        _mapper.Map(ticketDiscountDto, ticketDiscount);

        ticketDiscount.UserId = _userAccessor.GetUserId();

        var result = await _ticketDiscountRepository.AddAsync(ticketDiscount) > 0;

        if (!result) return Result<string>.Failure("Failed to create TicketDiscount");

        return Result<string>.Success("Successfully");
    }

    public async Task<Result<string>> EditCustomersOneAsync(TicketDiscountDto ticketDiscountDto)
    {
        var ticketDiscount = await _ticketDiscountRepository.GetOneAsync(ticketDiscountDto.Id);
    
        if (ticketDiscount == null) return null;
    
        var userId = _userAccessor.GetUserId();
    
        if (!await _ticketDiscountRepository.HasUserAccessToTheTicketDiscountAsync(ticketDiscountDto.Id.Value, userId))
        {
            return Result<string>.Failure("You have no access to this data");
        }
    
        ticketDiscountDto.UserId ??= userId;
        _mapper.Map(ticketDiscountDto, ticketDiscount);
    
        var result = await _ticketDiscountRepository.SaveAsync(ticketDiscount) > 0;
    
        if (!result) return Result<string>.Failure("Failed to update TicketDiscount");
    
        return Result<string>.Success("Successfully");
    }
    
    public async Task<Result<string>> DeleteCustomersOneAsync(Guid ticketDiscountId)
    {
        var ticketDiscount = await _ticketDiscountRepository.GetOneAsync(ticketDiscountId);
    
        if (ticketDiscount == null) return null;
    
        if (!await _ticketDiscountRepository.HasUserAccessToTheTicketDiscountAsync(ticketDiscountId, _userAccessor.GetUserId()))
        {
            return Result<string>.Failure("You have no access to this data");
        }
    
        var result = await _ticketDiscountRepository.RemoveAsync(ticketDiscount) > 0;
    
        if (!result) return Result<string>.Failure("Failed to delete TicketDiscount");
    
        return Result<string>.Success("Successfully");
    }
    
    public async Task<Result<Domain.Models.Catalogues.TicketDiscount>> ActiveDiscount(string discountCode)
    {
        var ticketDisount = await _ticketDiscountRepository.GetDiscountByCodeAsync(discountCode);
            
        if (ticketDisount == null) return Result<Domain.Models.Catalogues.TicketDiscount>.Failure("Invalid code");
        if (ticketDisount.isActivated) 
            return Result<Domain.Models.Catalogues.TicketDiscount>.Failure("The code is already activated");
            
        // mark discount as activated
        ticketDisount.isActivated = true;
            
        var result = await _ticketDiscountRepository.SaveAsync(ticketDisount) > 0;
        if (!result) return Result<Domain.Models.Catalogues.TicketDiscount>.Failure("Failed to activate the code");

        return Result<Domain.Models.Catalogues.TicketDiscount>.Success(ticketDisount);
    }
    
    public async Task<Result<Domain.Models.Catalogues.TicketDiscount>> DeactivateDiscount(Guid discountId)
    {
        var ticketDisount = await _ticketDiscountRepository.GetDiscountByIdAsync(discountId);
            
        if (ticketDisount == null) return Result<Domain.Models.Catalogues.TicketDiscount>.Failure("Invalid code");
        if (!ticketDisount.isActivated) 
            Result<Domain.Models.Catalogues.TicketDiscount>.Success(ticketDisount);
            
        // mark discount as deactivate
        ticketDisount.isActivated = false;
            
        var result = await _ticketDiscountRepository.SaveAsync(ticketDisount) > 0;
        if (!result) return Result<Domain.Models.Catalogues.TicketDiscount>.Failure("Failed to (de)activate discount");

        return Result<Domain.Models.Catalogues.TicketDiscount>.Success(ticketDisount);
    }
}