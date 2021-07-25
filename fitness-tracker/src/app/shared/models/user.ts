export interface IUser{
    id?:string;
    userName:string;
    firstName?:string;
    lastName?:string;
    email:string;
}

export interface IUserAccount extends IUser {
    password:string;
    confirmPassword?: string;
    reMemberMe?:boolean;
}

export class UserAccountImpl implements IUserAccount{
    userName: string;
    password: string;
    remberMe?: boolean;
    firstName?: string;
    lastName?: string;
    email: string;
    
    constructor(init?: IUserAccount){
        if(init){
          Object.assign(this, init);
        }
    }
}

export class User implements IUser{
    id:string;
    userName: string;
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