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
  set:number;  
  reps:number
  weight:number
  targetRep:number
}

export interface ILog{
  logId:number;
  user:IUser;
  comments:string;  
  created:string; 
  logExercises:ILogExercise[]
}