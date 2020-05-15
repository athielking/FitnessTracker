export interface IUser{
  id
  username:string
  firstName:string
  lastName:string
  street:string
  city:string
  zip:string
  phoneNumber:string
  email:string
}

export interface ILogExercise{
  exerciseId
  exerciseName:string
  reps:number
  weight:number
  targetRep:number
}

export interface ILog{
  logId:number;
  user:IUser;
  set:number;  
  comments:string;  
  created:string; 
  logExercises:ILogExercise[]
}

export interface ISingleLog{
  logId:number;
  user:IUser;
  set:number;  
  comments:string;  
  created:string; 
  exerciseId
  exerciseName:string
  reps:number
  weight:number
  targetRep:number
}

export interface ISaveLog{
  logId:number;
  user:IUser;
  set:number;  
  comments:string;  
  created:string; 
  logExercise:ILogExercise;
}