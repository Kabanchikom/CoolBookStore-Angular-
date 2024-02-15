export interface ILoginRequest
{
    userName: string,
    password: string,
}

export interface IAuthResponse
{
    accessToken: string,
    refreshToken: string,
}

export interface IRegisterRequest
{
    "username": string,
    "password": string,
    "passwordConfirm": string,
    "lastName": string,
    "firstName": string,
    "patronymic": string,
    "dateOfBirth": Date
}