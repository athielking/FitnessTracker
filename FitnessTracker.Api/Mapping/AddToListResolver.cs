﻿using System;
using System.Collections.Generic;
using AutoMapper;

using FitnessTracker.Core.Entities;
using FitnessTracker.DTO;

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
            var logExercise = _mapper.Map<LogExerciseDTO, LogExercise>(src.LogExercise);
 
            lst.Add(logExercise);

            Log log = new Log();
            log.User = user;
            log.UserId = user.Id;
            log.Set = src.Set;
            log.Comments = src.Comments;
            log.Created = DateTime.Parse(src.Created);
            log.Modified = DateTime.Now;
            log.LogExercises = new List<LogExercise>(lst);

            if (des != null)
            {               
                log.LogId = (int)src.LogId;
            }

            return log;
        }
    }
}
