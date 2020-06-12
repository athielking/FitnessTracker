import { IUser } from './user';


export interface ILogExercise{
    logId:number;
    exerciseId:number;
    exerciseName:string;
    reps:number;
    weight:number;
    targetRep:number;
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
    exerciseId;
    exerciseName:string;
    reps:number;
    weight:number;
    targetRep:number;
}

export interface ISaveLog{
    logId:number;
    user:IUser;
    set:number;  
    comments:string;  
    created:string; 
    logExercise:ILogExercise;
}

export function createLog(log?:ILog){
    return new Log(log);
}

export function createSingleLog(log?:ILog, logExercise?:ILogExercise) {
  return new SingleLog(log, logExercise);
}

export function createSaveLog(log?: ISingleLog) {
  return new SaveLog(log);
}

export function createLogExercise(logExercise?: ILogExercise) {
    return new LogExercise(logExercise);
  }

class Log implements ILog{
    logId: number;
    user: IUser;
    set: number;
    comments: string;
    created: string;
    logExercises: ILogExercise[];

    constructor(init?: any){
        if(init){
          Object.assign(this, init);
        }
    }
}

class LogExercise implements ILogExercise{
    logId: number;
    exerciseId: number;
    exerciseName: string;
    reps: number;
    weight: number;
    targetRep: number;

    constructor(init?: Partial<LogExercise>){
        if(init){
          Object.assign(this, init);
        }
    }
}

class SaveLog implements ISaveLog{
    logId: number;
    user: IUser;
    set: number;
    comments: string;
    created: string;
    logExercise: ILogExercise;
  
    constructor(log){
        if(log){
            this.logId = log.logId;
            this.user = log.user;
            this.set = log.set; 
            this.comments = log.comments;
            this.created = log.created;
            this.logExercise =  new LogExercise(log);
        }
    }
}

class SingleLog implements ISingleLog{
    logId: number;
    user: IUser;
    set: number;
    comments: string;
    created: string;
    exerciseId: any;
    exerciseName: string;
    reps: number;
    weight: number;
    targetRep: number;
    
    constructor(log:ILog, logExercise:ILogExercise){
        if(log){
          Object.assign(this, log);
        }
        if(logExercise){
          Object.assign(this, logExercise);
        }
    }
}