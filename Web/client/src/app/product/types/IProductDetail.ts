export default interface IProductDetail {
    id: string,
    name: string,
    imgSrc: string,
    oldPrice: number,
    newPrice?: number,
    isOnSale: boolean,
    shortDescription: string,
    longDescription: string,
};