export class Exercise{
    exerciseId: number;
    name: string;
    
    constructor(init?: Partial<Exercise>){
        if(init){
            Object.assign(this, init)
        }
    }
}