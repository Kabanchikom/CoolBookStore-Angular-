import { Routes, RouterModule } from "@angular/router";
import { CatalogPageComponent } from "./pages/catalog-page/catalog-page.component";
import { ProductDetailPageComponent } from "./pages/product-detail/product-detail-page.component";

export const PRODUCT_ROUTES: Routes = [
    {
        path: '',
        component: CatalogPageComponent,
        children: [
            { path: 'detail/:id', component: ProductDetailPageComponent },
        ]
    },
];