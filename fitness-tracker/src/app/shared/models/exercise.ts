export function createExercise(exercise?:IExercise){
    return new Exercise(exercise);
}

export interface IExercise{
    exerciseId: number;
    name: string;
}

class Exercise implements IExercise{
    exerciseId: number;
    name: string;
    
    constructor(init?: Partial<IExercise>){
        if(init){
            Object.assign(this, init)
        }
    }
}