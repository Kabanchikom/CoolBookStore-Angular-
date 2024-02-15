export interface IUser
{
    id: string,
    username: string,
    lastName: string,
    firstName: string,
    patronymic?: string,
    dateOfBirth: Date
}