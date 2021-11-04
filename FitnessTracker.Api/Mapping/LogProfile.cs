using AutoMapper;
using AutoMapper.EquivalencyExpression;
using FitnessTracker.Api.DTO;
using FitnessTracker.Api.Models;
using FitnessTracker.Core.Entities;
using FitnessTracker.DTO;

namespace FitnessTracker.Mapping
{
    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<Microsoft.Graph.User, UserAD>().ReverseMap();

            CreateMap<UserAD, UserDTO>().ReverseMap()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.GivenName, act => act.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Surname, act => act.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Mail, act => act.MapFrom(src => src.Email))
                .ForMember(dest => dest.DisplayName, act => act.MapFrom(src => src.Username));

            CreateMap<UserAD, RegisterDTO>().ReverseMap()
                .ForMember(dest => dest.GivenName, act => act.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Surname, act => act.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Mail, act => act.MapFrom(src => src.Email))
                .ForMember(dest => dest.DisplayName, act => act.MapFrom(src => src.Username));

            CreateMap<Workout, WorkoutDTO>().ReverseMap();

            CreateMap<LogExercise, LogExerciseDTO>(MemberList.None).ReverseMap();

            CreateMap<Exercise, ExerciseDTO>(MemberList.None).ReverseMap();

            CreateMap<SaveLogDTO, Log>().ConvertUsing<AddToListResolver>();
            CreateMap<RegisterDTO, User>();
        }
    }
}
