interface ICartLine {
    id: string,
    name: string,
    imgSrc: string,
    oldPrice: number,
    newPrice?: number,
    description?: string,
    productId: string,
    ownerId: string,
    quantity: number,
}

export default ICartLine;