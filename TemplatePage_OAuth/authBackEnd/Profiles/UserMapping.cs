using authbackend.Dtos;
using authbackend.Entities;
using AutoMapper;
namespace authbackend.Profiles;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, GetUserResponseDto>().ForMember(
            dest => dest.UserName, 
            opt => opt.MapFrom(opt => opt.Names)
        ).ForMember(
            dest => dest.Email,
            opt => opt.MapFrom(opt => opt.Email)
        );

        CreateMap<RegisterUserDto, User>().ForMember(
            dest => dest.Names, 
            opt => opt.MapFrom(opt => opt.Names)
        ).ForMember(
            dest => dest.LastName,
            opt => opt.MapFrom(opt => opt.LastName)
        ).ForMember(
            dest => dest.SecondLastName,
            opt => opt.MapFrom(opt => opt.SecondLastName)
        ).ForMember(
            dest => dest.Email,
            opt => opt.MapFrom(opt => opt.Email)
        ).ForMember(
            dest => dest.PasswordHash,
            opt => opt.MapFrom(opt => opt.Password)
        );

        CreateMap<User, UserCreatedResponseDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Names))
            .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.UserTokens.FirstOrDefault().Token)) // Asumiendo que quieres el primer token
            .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.UserRefreshToken.FirstOrDefault().RefreshToken)) // Similar para refresh token
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name))) // Si tienes una relación con Roles
            .ForMember(dest => dest.Claims, opt => opt.MapFrom(src => src.UserClaims.Select(uc => new ClaimDto
            {
                Type = uc.ClaimType,
                Value = uc.ClaimValue
            })));
        
        // Mapeo de UserClaim a ClaimDto (Necesario para el mapeo de Claims)
        CreateMap<UserClaim, ClaimDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ClaimType))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ClaimValue));
        
        // Mapeo de UserRole a RoleDto o string (Dependiendo de cómo quieres representarlo)
        CreateMap<UserRole, string>()
            .ConvertUsing(src => src.Role.Name);


    }
}