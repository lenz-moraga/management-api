using AutoMapper;
using BorrowingSystem.DTOs;
using BorrowingSystem.Models;

namespace BorrowingSystem.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Items
            CreateMap<Item, CreateItemDTO>().ReverseMap();
            CreateMap<Item, UpdateItemDTO>().ReverseMap();

            // Movement
            CreateMap<Movement, CreateMovementDTO>().ReverseMap();
            CreateMap<Movement, UpdateMovementDTO>().ReverseMap();

            // MovementDetails
            CreateMap<MovementDetail, CreateMovementDetailsDTO>().ReverseMap();
            CreateMap<MovementDetail, UpdateMovementDetailsDTO>().ReverseMap();

            // MovementType
            CreateMap<MovementType, CreateMovementTypeDTO>().ReverseMap();
            CreateMap<MovementType, UpdateMovementTypeDTO>().ReverseMap();

            // Roles
            CreateMap<CreateRoleDTO, Role>().ReverseMap();                    // ↔ yes, we map in both directions
            CreateMap<UpdateRoleDTO, Role>();                                // → one-way only (patch existing)
            CreateMap<Role, ExistingRoleDTO>();                              // → read-only

            // Users
            CreateMap<CreateUserDTO, User>().ReverseMap();                   // ↔ creation
            CreateMap<UpdateUserDTO, User>();                                // → patch existing
            CreateMap<User, ExistingUserDTO>();                              // → display

            // Login
            CreateMap<User, AuthDTO>().ReverseMap();                         // ↔ okay for auth mapping
            CreateMap<LoginDTO, User>();                                     // → login check only
        }
    }
}
