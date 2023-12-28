using System.Transactions;
using Application.Core;
using Application.DTOs.Requests;
using Application.DTOs.Tables;
using Application.DTOs.Users.HTTP;
using Application.Handlers.Catalogues.TicketDiscount;
using AutoMapper;
using Domain.Models.Catalogues;
using Domain.Repositories.DTOs;
using Domain.Repositories.Repos.Catalogues;
using Domain.Repositories.Repos.Interfaces.Catalogues;
using Domain.Repositories.Repos.Tables;
using Domain.Repositories.Repos.Interfaces.Tables;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Handlers.Tables.Ticket
{
    public class TicketHandler : ITicketHandler
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        private readonly ITicketOrderRepository _ticketOrderRepository;
        private readonly ITicketDiscountRepository _ticketDiscountRepository;
        private readonly ITicketTypeRepository _ticketTypeRepository;
        private readonly ITicketDiscountHandler _ticketDiscountHandler;
        private readonly IEventRepository _eventRepository;

        public TicketHandler(ITicketRepository ticketRepository, IMapper mapper, IUserAccessor userAccessor,
            ITicketOrderRepository ticketOrderRepository, ITicketDiscountRepository ticketDiscountRepository, 
            ITicketTypeRepository ticketTypeRepository, ITicketDiscountHandler ticketDiscountHandler, 
            IEventRepository eventRepository)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _userAccessor = userAccessor;
            _ticketOrderRepository = ticketOrderRepository;
            _ticketDiscountRepository = ticketDiscountRepository;
            _ticketTypeRepository = ticketTypeRepository;
            _ticketDiscountHandler = ticketDiscountHandler;
            _eventRepository = eventRepository;
        }

        public async Task<Result<Domain.Models.Tables.Ticket>> GetCustomersTicketAsync(Guid ticketId)
        {
            if (!await _ticketOrderRepository.HasUserAccessToTheTicketOrderAsync(ticketId,
                    _userAccessor.GetUserId()))
            {
                return Result<Domain.Models.Tables.Ticket>.Failure("You have no access to this data");
            }

            var ticket = await _ticketRepository.GetOneDetailedAsync(ticketId);

            // TicketDto ticketDto = new TicketDto();
            //
            // _mapper.Map(ticket, ticketDto);

            return Result<Domain.Models.Tables.Ticket>.Success(ticket);
        }
        
        public async Task<Result<List<Domain.Models.Tables.Ticket>>> GetAvailableTicketListAsync(Guid eventId)
        {
            var tickets = await _ticketRepository.GetAvailableTicketListAsync(eventId);

            return Result<List<Domain.Models.Tables.Ticket>>.Success(tickets);
        }

        public async Task<Result<string>> CreateCustomersOneAsync(TicketDto ticketDto)
        {
            if (!await _eventRepository.HasUserAccessToTheEventAsync(ticketDto.EventId,
                    _userAccessor.GetUserId()))
            {
                return Result<string>.Failure("You have no access to this action");
            }
            
            var totalPlaces = await _eventRepository.GetEventsTotalPlacesAsync(ticketDto.EventId);
            
            var createdTicketAmount = await _ticketRepository.GetCreatedEventsTicketAmount(ticketDto.EventId);
            if (totalPlaces - createdTicketAmount < createdTicketAmount + 1) return Result<string>
                .Failure("Failed to create Ticket. Not enough available places");
            
            var ticket = new Domain.Models.Tables.Ticket();

            ticketDto.FinalPrice = await _ticketTypeRepository.GetPriceAsync(ticketDto.TypeId);
            _mapper.Map(ticketDto, ticket);

            var result = await _ticketRepository.AddAsync(ticket) > 0;

            if (!result) return Result<string>.Failure("Failed to create Ticket");

            return Result<string>.Success("Successfully");
        }

        public async Task<Result<string>> EditCustomersOneAsync(TicketDto ticketDto)
        {
            var ticket = await _ticketRepository.GetOneTypeIncludedAsync(ticketDto.Id.Value);

            if (ticket == null) return null;

            var userId = _userAccessor.GetUserId();

            if (!await _ticketOrderRepository.HasUserAccessToTheTicketOrderAsync(ticketDto.Id.Value, userId))
            {
                return Result<string>.Failure("You have no access to this data");
            }

            _mapper.Map(ticketDto, ticket);

            await CalculateTicketPrice(ticket);

            var result = await _ticketRepository.SaveAsync(ticket) > 0;

            if (!result) return Result<string>.Failure("Failed to update Ticket");

            return Result<string>.Success("Successfully");
        }

        public async Task<Result<string>> DeleteCustomersOneAsync(Guid ticketId)
        {
            var ticket = await _ticketRepository.GetOneAsync(ticketId);

            if (ticket == null) return null;

            if (!await _ticketOrderRepository.HasUserAccessToTheTicketOrderAsync(ticketId, _userAccessor.GetUserId()))
            {
                return Result<string>.Failure("You have no access to this data");
            }
            
            if (ticket.isPurchased) return Result<string>.Failure("Unable to delete purchased ticket");

            var result = await _ticketRepository.RemoveAsync(ticket) > 0;

            if (!result) return Result<string>.Failure("Failed to delete Ticket");

            return Result<string>.Success("Successfully");
        }

        public async Task<Result<string>> GenerateEventsTicketList(TicketDto ticketDto, int ticketAmount)
        {
            if (!await _eventRepository.HasUserAccessToTheEventAsync(ticketDto.EventId,
                    _userAccessor.GetUserId()))
            {
                return Result<string>.Failure("You have no access to this action");
            }

            var totalPlaces = await _eventRepository.GetEventsTotalPlacesAsync(ticketDto.EventId);
            if (totalPlaces < ticketAmount) return Result<string>
                .Failure("Failed to create Tickets. Cannot create more tickets than event capacity");
            
            var createdTicketAmount = await _ticketRepository.GetCreatedEventsTicketAmount(ticketDto.EventId);
            if (totalPlaces - createdTicketAmount < ticketAmount) return Result<string>
                .Failure("Failed to create Tickets. Not enough available places");
            
            
            var tickets = new List<Domain.Models.Tables.Ticket>();
            var ticketDtos = new List<TicketDto>();

            ticketDto.FinalPrice = await _ticketTypeRepository.GetPriceAsync(ticketDto.TypeId);
            
            for (int i = 0; i < ticketAmount; i++)
            {
                ticketDtos.Add(ticketDto);
            }
            
            _mapper.Map(ticketDtos, tickets);
            
            var result = await _ticketRepository.AddRangeAsync(tickets) > 0;

            if (!result) return Result<string>.Failure("Failed to create Tickets");

            return Result<string>.Success("Successfully");
        }
        
        public async Task<Result<EventTicketsAmountDto>> GetEventTicketsAmountAsync(Guid eventId)
        {
            var eventTicketsAmount = await _ticketRepository.GetEventsTicketAmountAsync(eventId);

            return Result<EventTicketsAmountDto>.Success(eventTicketsAmount);
        }

        public async Task<Result<string>> ApplyDiscountTransactionAsync(ApplyDiscountDto applyDiscountDto)
        {
            var result = new Result<string>();
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Do context work here
                result = await ApplyDiscountAsync(applyDiscountDto);

                // complete the transaction
                scope.Complete();
            }
            return result;
            
            //---------------------------------------------------------------
            // another way
            
            // start transactions
            // var ticketTransaction = await _ticketRepository.BeginTransaction();
            //
            // var result = await ApplyDiscountAsync(applyDiscountDto, ticketTransaction);
            //
            // // Commit transactions
            // //await _ticketRepository.CommitTransaction(ticketTransaction);
            // await ticketTransaction.CommitAsync();
            //
            // return result;
        }
        
        public async Task<Result<string>> RemoveDiscountTransactionAsync(Guid ticketId)
        {
            var result = new Result<string>();
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Do context work here
                result = await RemoveDiscountAsync(ticketId);

                // complete the transaction
                scope.Complete();
            }
            return result;
        }
        
        private async Task<Result<string>> ApplyDiscountAsync(ApplyDiscountDto applyDiscountDto) 
            //IDbContextTransaction? ticketTransaction)
        {
            // another way continuance
            //await _ticketDiscountRepository.UseTransactionAsync(ticketTransaction);
            
            // check if the ticket exists
            var ticket = await _ticketRepository.GetOneTypeIncludedAsync(applyDiscountDto.TicketId);
            if (ticket == null) return Result<string>.Failure("Invalid ticket");
            
            // activate discount
            //var result = await ActiveDiscount(applyDiscountDto.DiscountCode);
            var result = await _ticketDiscountHandler.ActiveDiscount(applyDiscountDto.DiscountCode);
            if (!result.IsSuccess) return Result<string>.Failure(result.Error);

            var ticketDisount = result.Value;
            
            // add discount to a ticket
            ticket.DiscountId = ticketDisount.Id;
            await CalculateTicketPrice(ticket);
            
            var result2 = await _ticketRepository.SaveAsync(ticket) > 0;
            if (!result2) return Result<string>.Failure("Failed to apply discount");
            
            return Result<string>.Success("Successfully");
        }
        
        private async Task<Result<string>> RemoveDiscountAsync(Guid ticketId)
        {
            var ticket = await _ticketRepository.GetOneTypeIncludedAsync(ticketId);
            if (ticket == null) return Result<string>.Failure("Invalid ticket");
            
            // deactivate discount
            if (ticket.DiscountId == null) return Result<string>.Failure("The ticket has no activated discounts");
            
            //var result = await DeactivateDiscount(ticket.DiscountId.Value);
            var result = await _ticketDiscountHandler.DeactivateDiscount(ticket.DiscountId.Value);
            if (!result.IsSuccess) return Result<string>.Failure(result.Error);

            ticket.DiscountId = null;
            await CalculateTicketPrice(ticket);

            var result2 = await _ticketRepository.SaveAsync(ticket) > 0;
            if (!result2) return Result<string>.Failure("Failed to remove discount");
            
            return Result<string>.Success("Successfully");
        }
        
        private async Task CalculateTicketPrice(Domain.Models.Tables.Ticket ticket)
        {
            double defaultPrice = ticket.Type.Price;
            
            if (ticket.DiscountId == null)
            {
                ticket.FinalPrice = defaultPrice;
                return;
            }
            
            int discount = await _ticketDiscountRepository.GetDiscountPercentageAsync(ticket.DiscountId.Value);
                
            double disocuntValue = (100 - discount) / 100.0;
            double finalPrice = defaultPrice * disocuntValue;

            ticket.FinalPrice = finalPrice;
        }
        
        public async Task<Result<Domain.Models.Tables.Ticket>> GetTicketToBuyAsync(Guid eventId, Guid typeId)
        {
            var ticket = await _ticketRepository.GetOneToBuyDetailedAsync(eventId, typeId);

            if (ticket == null) Result<Domain.Models.Tables.Ticket>.Failure("There are no available tickets.");

            return Result<Domain.Models.Tables.Ticket>.Success(ticket);
        }
    }
}
