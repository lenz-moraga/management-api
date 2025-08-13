using AutoMapper;
using BorrowingSystem.DTOs;
using BorrowingSystem.Exceptions;
using BorrowingSystem.Interfaces.Repository;
using BorrowingSystem.Interfaces.Services;
using BorrowingSystem.Models;

namespace BorrowingSystem.Services
{
    public class MovementDetailService(IMovementDetailRepository requestItemRepository, IMapper mapper) : IMovementDetailService
    {
        private readonly IMovementDetailRepository _requestItemRepository = requestItemRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ExistingMovementDetailsDTO>> GetAllMovementDetailItemsAsync()
        {
            var movementDetailItems = await _requestItemRepository.GetAllMovementDetailItemsAsync();

            return _mapper.Map<IEnumerable<ExistingMovementDetailsDTO>>(movementDetailItems);
        }

        public async Task<ExistingMovementDetailsDTO?> GetMovementDetailItemByIdAsync(Guid id)
        {
            var movementDetailItem = await _requestItemRepository.GetMovementDetailItemByIdAsync(id) ?? throw new Exception("Movement detail item not found!");

            return _mapper.Map<ExistingMovementDetailsDTO>(movementDetailItem);
        }

        public async Task<ExistingMovementDetailsDTO> CreateMovementDetailItemAsync(CreateMovementDetailsDTO movementDetailItemDto)
        {
            var requestItem = _mapper.Map<MovementDetail>(movementDetailItemDto);
            var newRequestItem = await _requestItemRepository.CreateMovementDetailItemAsync(requestItem);

            await _requestItemRepository.SaveChangesAsync();

            return _mapper.Map<ExistingMovementDetailsDTO>(newRequestItem);
        }

        public async Task<ExistingMovementDetailsDTO?> UpdateRequestItemAsync(Guid id, UpdateMovementDetailsDTO requestItem)
        {
            var existingRequestItem = await _requestItemRepository.GetMovementDetailItemByIdAsync(id) 
                ?? throw new ServiceException("Movement Detail not found.", ErrorCode.NotFound);

            _mapper.Map(requestItem, existingRequestItem);

            var updatedRequestItem = _requestItemRepository.UpdateMovementDetailItem(existingRequestItem);
            await _requestItemRepository.SaveChangesAsync();

            return _mapper.Map<ExistingMovementDetailsDTO>(updatedRequestItem);
        }

        public async Task DeleteRequestItemAsync(Guid id)
        {
            var existingRequestItem = await _requestItemRepository.GetMovementDetailItemByIdAsync(id)
                ?? throw new ServiceException("Movement Detail not found.", ErrorCode.NotFound);

            _requestItemRepository.DeleteMovementDetailItem(existingRequestItem);
            await _requestItemRepository.SaveChangesAsync();
        }


    }
}
