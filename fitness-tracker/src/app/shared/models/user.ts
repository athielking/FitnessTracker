
export interface IResetPassword{
    userName:string,
    password:string
}

export interface IJWTToken{
    token:string;
    userName:string;
    expiration:string;
}

export function createUser(user:IUser):User{
    return new User(user);
}

export function createUserAccount(userAccount:IUserAccount):UserAccountImpl{
    return new UserAccountImpl()
}

export interface IUser{
    userName:string;
    firstName?:string;
    lastName?:string;
    email:string;
}

export interface IUserAccount extends IUser {
    password:string;
    reMemberMe?:boolean;
}

class UserAccountImpl implements IUserAccount{
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

class User implements IUser{
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