import IPaging from "../../../../shared/types/IPaging";
import IProductCatalogItem from "../../../types/IProductCatalogItem";

export interface IProductCatalogItemsFilter
{
    genres: string[],
    authors: string[],
    minPrice: number,
    maxPrice: number,
    isOnSale: boolean,
    isDiscount: boolean,
}

export interface IProductCatalogItemsRequest {
    paging?: IPaging,
    filter?: IProductCatalogItemsFilter,
}

export interface IProductCatalogItemResponse {
    products: IProductCatalogItem[],
    total: number,
}
