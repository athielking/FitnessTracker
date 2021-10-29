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

            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<Workout, WorkoutDTO>().ReverseMap();

            CreateMap<LogExercise, LogExerciseDTO>(MemberList.None).ReverseMap();

            CreateMap<Exercise, ExerciseDTO>(MemberList.None).ReverseMap();

            CreateMap<SaveLogDTO, Log>().ConvertUsing<AddToListResolver>();
            CreateMap<RegisterDTO, User>();
        }
    }
}
