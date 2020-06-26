export function createUser(user:IUser){
    return new User(user);
}

export interface IUser{
    id
    username:string;
    firstName:string;
    lastName:string;
    phoneNumber:string;
    email:string;
}

class User implements IUser{
    id: any;
    username: string;
    firstName: string;
    lastName: string;
    phoneNumber: string;
    email: string;

    constructor(init?: IUser){
        if(init){
          Object.assign(this, init);
        }
    }
}