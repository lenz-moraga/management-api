using AutoMapper;
using BorrowingSystem.DTOs;
using BorrowingSystem.Exceptions;
using BorrowingSystem.Interfaces.Repository;
using BorrowingSystem.Interfaces.Services;
using BorrowingSystem.Models;
using BorrowingSystem.Services.Strategies;

namespace BorrowingSystem.Services
{
    public class MovementService(
        IMovementRepository movementRepository,
        IMovementTypeRepository movementTypeRepository,
        IUserRepository userRepository,
        IItemRepository itemRepository,
        StockAdjustmentFactory stockAdjustmentFactory,
        IMapper mapper
        ) : IMovementService
    {
        private readonly IMovementRepository _movementRepository = movementRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IItemRepository _itemRepository = itemRepository;
        private readonly IMovementTypeRepository _movementTypeRepository = movementTypeRepository;
        private readonly StockAdjustmentFactory _stockAdjustmentFactory = stockAdjustmentFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ExistingMovementDTO>> GetAllMovementsAsync()
        {
            var movements = _mapper.Map<IEnumerable<ExistingMovementDTO>>(await _movementRepository.GetAllMovementsAsync());

            return movements;
        }

        public async Task<ExistingMovementDTO?> GetMovementByIdAsync(Guid id)
        {
            var movement = _mapper.Map<ExistingMovementDTO>(await _movementRepository.GetMovementByIdAsync(id))
                ?? throw new ServiceException("Movement not found!", ErrorCode.NotFound);

            return movement;
        }

        public async Task<ExistingMovementDTO> CreateMovementAsync(CreateMovementDTO movementDto)
        {
            var user = await _userRepository.GetUserByIdAsync(movementDto.RequestedByUserId)
                ?? throw new ServiceException("User not found", ErrorCode.NotFound);

            var movement = new Movement { RequestedByUserId = user.Id };

            foreach (var detail in movementDto.MovementDetails)
            {
                var item = await _itemRepository.GetItemByIdAsync(detail.ItemId)
                    ?? throw new ServiceException($"Item with ID {detail.ItemId} not found", ErrorCode.NotFound);

                var movementType = await _movementTypeRepository.GetMovementTypeByIdAsync(detail.MovementTypeId)
                    ?? throw new ServiceException($"Movement detail with ID {detail.MovementTypeId} not found", ErrorCode.NotFound);

                if (movementType.Name == "Borrow")
                {
                    if (item.CurrentStock <= detail.Quantity)
                        throw new ServiceException($"Insufficient stock for item {item.Name}", ErrorCode.BadRequest);

                    item.CurrentStock -= detail.Quantity; // Decrease stock for borrow movements
                }
                else if (movementType.Name == "Return")
                {
                    item.CurrentStock += detail.Quantity; // Increase stock for return movements
                }

                var movementDetail = new MovementDetail
                {
                    ItemId = item.Id,
                    MovementTypeId = movementType.Id,
                    Quantity = detail.Quantity
                };

                movement.MovementDetails.Add(movementDetail);
            }

            await _movementRepository.CreateMovementAsync(movement);
            await _movementRepository.SaveChangesAsync();

            return _mapper.Map<ExistingMovementDTO>(movement);
        }

        public async Task<ExistingMovementDTO?> UpdateMovementAsync(Guid id, UpdateMovementDTO movementDto)
        {
            var existingMovement = await _movementRepository.GetMovementByIdAsync(id)
                ?? throw new ServiceException("Movement not found", ErrorCode.NotFound);

            _mapper.Map(movementDto, existingMovement);

            foreach (var detail in existingMovement.MovementDetails)
            {
                detail.MovementType = await _movementTypeRepository.GetMovementTypeByIdAsync(detail.MovementTypeId)
                    ?? throw new ServiceException($"MovementType {detail.MovementTypeId} not found", ErrorCode.NotFound);

                var item = await _itemRepository.GetItemByIdAsync(detail.ItemId)
                    ?? throw new ServiceException($"Item {detail.ItemId} not found", ErrorCode.NotFound);

                _stockAdjustmentFactory.ApplyStrategy(item, detail);
            }

            _movementRepository.UpdateMovement(existingMovement);
            await _movementRepository.SaveChangesAsync();

            return _mapper.Map<ExistingMovementDTO>(existingMovement);
        }

        public async Task DeleteMovementAsync(Guid id)
        {
            var movement = await _movementRepository.GetMovementByIdAsync(id)
                ?? throw new ServiceException("Movement not found", ErrorCode.NotFound);

            _movementRepository.DeleteMovement(movement);
            await _movementRepository.SaveChangesAsync();
        }
    }
}
