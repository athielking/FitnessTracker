export interface IResetPassword{
    userName:string,
    password:string
}

export interface IJWTToken{
    token:string;
    userID:string;
    expiration:string;
}