using AutoMapper;
using FitnessTracker.Data.Entities;
using FitnessTracker.DTO;
using System.Linq;

namespace FitnessTracker.Mapping
{
    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<Log, LogDTO>(MemberList.None);
            CreateMap<Log, LogDTO>(MemberList.None).ReverseMap();

            CreateMap<User, UserDTO>();
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<LogExercise, LogExerciseDTO>(MemberList.None);
            CreateMap<LogExercise, LogExerciseDTO>(MemberList.None).ReverseMap();

            CreateMap<Exercise, ExerciseDTO>(MemberList.None);
            CreateMap<Exercise, ExerciseDTO>(MemberList.None).ReverseMap();

            CreateMap<SaveLogDTO, Log>().ConvertUsing<AddToListResolver>();
        }
    }
}
