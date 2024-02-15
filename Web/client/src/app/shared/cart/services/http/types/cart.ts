export interface ICartAddRequest
{
    productId: string,
    ownerId: string,
    quantity: number,
}

export interface ICartUpdateRequest
{
    productId: string,
    quantity: number,
}
