using AutoMapper;
using BorrowingSystem.DTOs;
using BorrowingSystem.Exceptions;
using BorrowingSystem.Interfaces.Repository;
using BorrowingSystem.Interfaces.Services;
using BorrowingSystem.Models;

namespace BorrowingSystem.Services
{
    public class MovementTypeService(IMovementTypeRepository movementTypeRepository, IMapper mapper) : IMovementTypeService
    {
        private readonly IMovementTypeRepository _movementTypeRepository = movementTypeRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ExistingMovementTypeDTO>> GetAllMovementTypesAsync()
        {
            var movementTypes = await _movementTypeRepository.GetAllMovementTypesAsync();

            return _mapper.Map<IEnumerable<ExistingMovementTypeDTO>>(movementTypes);
        }

        public async Task<ExistingMovementTypeDTO?> GetMovementTypeByIdAsync(Guid id)
        {
            var movementType = await _movementTypeRepository.GetMovementTypeByIdAsync(id) 
                ?? throw new ServiceException("MovementType not found.", ErrorCode.NotFound);

            return _mapper.Map<ExistingMovementTypeDTO>(movementType);
        }
        public async Task<ExistingMovementTypeDTO> CreateMovementTypeAsync(CreateMovementTypeDTO movementTypeDto)
        {
            var movementTypes = await _movementTypeRepository.GetAllMovementTypesAsync();

            var movement = _mapper.Map<MovementType>(movementTypeDto);
            var newMovement = await _movementTypeRepository.CreateMovementTypeAsync(movement);

            await _movementTypeRepository.SaveChangesAsync();

            return _mapper.Map<ExistingMovementTypeDTO>(newMovement);
        }
        public async Task<ExistingMovementTypeDTO?> UpdateMovementTypeAsync(Guid id, UpdateMovementTypeDTO movementTypeDto)
        {
            var existingMovementType = await _movementTypeRepository.GetMovementTypeByIdAsync(id)
                ?? throw new ServiceException("Movement Type not found", ErrorCode.NotFound);

            _mapper.Map(movementTypeDto, existingMovementType);
            var updatedMovement = _movementTypeRepository.UpdateMovementType(existingMovementType);

            await _movementTypeRepository.SaveChangesAsync();

            return _mapper.Map<ExistingMovementTypeDTO>(updatedMovement);
        }
        public async Task DeleteMovementTypeAsync(Guid id)
        {
            var movementType = await _movementTypeRepository.GetMovementTypeByIdAsync(id)
                ?? throw new ServiceException("Movement Type not found", ErrorCode.NotFound);

            _movementTypeRepository.DeleteMovementType(movementType);
            await _movementTypeRepository.SaveChangesAsync();
        }
    }
}
