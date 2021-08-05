using AutoMapper;
using AutoMapper.EquivalencyExpression;
using FitnessTracker.Api.DTO;
using FitnessTracker.Core.Entities;
using FitnessTracker.DTO;

namespace FitnessTracker.Mapping
{
    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<Workout, WorkoutDTO>().ReverseMap();

            CreateMap<LogExercise, LogExerciseDTO>(MemberList.None).ReverseMap();

            CreateMap<Exercise, ExerciseDTO>(MemberList.None).ReverseMap();

            CreateMap<SaveLogDTO, Log>().ConvertUsing<AddToListResolver>();
            CreateMap<RegisterDTO, User>();
        }
    }
}
