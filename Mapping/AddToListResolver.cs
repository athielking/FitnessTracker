using AutoMapper;
using FitnessTracker.Data.Entities;
using FitnessTracker.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Mapping
{
    public class AddToListResolver : ITypeConverter<SaveLogDTO, Log>
    {
        private readonly List<LogExercise> lst = new List<LogExercise>();
        private readonly IMapper _mapper;

        public AddToListResolver(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Log Convert(SaveLogDTO src, Log des, ResolutionContext context)
        {
            var user = _mapper.Map<UserDTO, User>(src.User);
            
            lst.Add(src.LogExercise);

            Log log = new Log();
            log.User = user;
            log.UserId = user.Id;
            log.Set = src.Set;
            log.Comments = src.Comments;
            log.LogExercises = new List<LogExercise>(lst);

            if (des != null)
            {
                des = log;
                des.LogId = (int)src.LogId;
                return des;
            }

            return log;
        }
    }
}
