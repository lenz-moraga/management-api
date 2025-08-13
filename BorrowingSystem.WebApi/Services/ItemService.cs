using AutoMapper;
using BorrowingSystem.DTOs;
using BorrowingSystem.Exceptions;
using BorrowingSystem.Interfaces.Repository;
using BorrowingSystem.Interfaces.Services;
using BorrowingSystem.Models;

namespace BorrowingSystem.Services
{
    public class ItemService(IItemRepository itemRepository, IMapper mapper) : IItemService
    {
        private readonly IItemRepository _itemRepository = itemRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ExistingItemDTO>> GetAllItemsAsync()
        {
            var items = await _itemRepository.GetAllItemsAsync();

            return _mapper.Map<IEnumerable<ExistingItemDTO>>(items);
        }


        public async Task<ExistingItemDTO?> GetItemByIdAsync(Guid id)
        {
            var item = await _itemRepository.GetItemByIdAsync(id) 
                ?? throw new ServiceException("Item not found.", ErrorCode.NotFound);

            return _mapper.Map<ExistingItemDTO>(item);
        }

        public async Task<ExistingItemDTO> CreateItemAsync(CreateItemDTO itemDto)
        {
            var item = _mapper.Map<Item>(itemDto);
            var newItem = await _itemRepository.CreateItemAsync(item);

            await _itemRepository.SaveChangesAsync();

            return _mapper.Map<ExistingItemDTO>(newItem);
        }

        public async Task<ExistingItemDTO?> UpdateItemAsync(Guid id, UpdateItemDTO itemDto)
        {
            var existingItem = await _itemRepository.GetItemByIdAsync(id) 
                ?? throw new ServiceException("Item not found.", ErrorCode.NotFound);

            _mapper.Map(itemDto, existingItem);

            var updatedItem = _itemRepository.UpdateItem(existingItem);
            await _itemRepository.SaveChangesAsync();

            return _mapper.Map<ExistingItemDTO>(updatedItem);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var existingItem = await _itemRepository.GetItemByIdAsync(id)
                ?? throw new ServiceException("Item not found.", ErrorCode.NotFound);

            _itemRepository.UpdateItem(existingItem);
            await _itemRepository.SaveChangesAsync();
        }

    }
}
