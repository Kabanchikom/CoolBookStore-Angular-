export default interface IProductCatalogItem {
    id: string,
    name: string,
    imgSrc: string,
    oldPrice: number,
    newPrice?: number,
    isOnSale: boolean,
    description?: string,
};