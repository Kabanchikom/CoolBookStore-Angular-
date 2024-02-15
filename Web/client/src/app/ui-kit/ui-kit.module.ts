import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardBoxComponent } from './cards/card-box/card-box.component';
import { CardNameComponent } from './cards/card-content/card-name/card-name.component';
import { CardPriceComponent } from './cards/card-content/card-price/card-price.component';
import { CardDescriptionComponent } from './cards/card-content/card-description/card-description.component';
import { ButtonComponent } from './buttons/button/button.component';
import { CardWrapperComponent } from './cards/card-wrapper/card-wrapper.component';
import { FooterComponent } from './layout/footer/footer.component';
import { HeaderComponent } from './layout/header/header.component';
import { MainComponent } from './layout/main/main.component';
import { LogoComponent } from './logo/logo.component';
import { SearchbarComponent } from './inputs/searchbar/searchbar.component';
import { HorizontalLineComponent } from './splitters/horizontal-line/horizontal-line.component';
import { FlexColumnComponent } from './containers/flex-container/flex-column/flex-column.component';
import { FlexRowComponent } from './containers/flex-container/flex-row/flex-row.component';
import { FlexContainerComponent } from './containers/flex-container/flex-container.component';
import { FilterBlockComponent } from './filters/filter-block/filter-block.component';
import { SortBlockComponent } from './filters/sort-block/sort-block.component';
import { FilterButtonComponent } from './filters/filter-button/filter-button.component';
import { PageContainerComponent } from './layout/page-container/page-container.component';
import { WrapRowComponent } from './containers/wrap-row/wrap-row.component';
import { CardThumbComponent } from './cards/card-thumb/card-thumb.component';
import { RowCardComponent } from './cards/row-card/row-card.component';
import { TabComponent } from './tabs/tab/tab.component';
import { TabHeaderComponent } from './tabs/tab-header/tab-header.component';

import { ModalWindowComponent } from './modals/modal-window/modal-window.component';
import { SpinnerComponent } from './spinners/spinner/spinner.component';
import { FlexComponent } from './containers/flex/flex.component';
import { GridComponent } from './containers/grid/grid.component';
import { OutletTabComponent } from './tabs/outlet-tab/outlet-tab.component';
import { TabNavLinkComponent } from './tabs/tab-header/tab-nav-link/tab-nav-link.component';
import { TabsComponent } from './tabs/tabs.component';
import { PopupComponent } from './popups/popup/popup.component';
import { MenuComponent } from './menus/menu/menu.component';
import { MenuOptionComponent } from './menus/menu/menu-option/menu-option.component';



@NgModule({
  declarations: [
    LogoComponent,
    CardBoxComponent,
    CardNameComponent,
    CardPriceComponent,
    CardNameComponent,
    CardDescriptionComponent,
    CardPriceComponent,
    CardDescriptionComponent,
    ButtonComponent,
    CardWrapperComponent,
    HeaderComponent,
    FooterComponent,
    MainComponent,
    LogoComponent,
    SearchbarComponent,
    FlexContainerComponent,
    FlexColumnComponent,
    FlexRowComponent,
    HorizontalLineComponent,
    FilterBlockComponent,
    SortBlockComponent,
    FilterButtonComponent,
    PageContainerComponent,
    WrapRowComponent,
    CardThumbComponent,
    RowCardComponent,
    TabComponent,
    TabHeaderComponent,
    GridComponent,
    FlexComponent,
    TabsComponent,
    TabNavLinkComponent,
    OutletTabComponent,
    ModalWindowComponent,
    SpinnerComponent,
    PopupComponent,
    MenuComponent,
    MenuOptionComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    LogoComponent,
    CardBoxComponent,
    CardNameComponent,
    CardPriceComponent,
    CardNameComponent,
    CardDescriptionComponent,
    CardPriceComponent,
    CardDescriptionComponent,
    ButtonComponent,
    CardWrapperComponent,
    HeaderComponent,
    FooterComponent,
    MainComponent,
    LogoComponent,
    SearchbarComponent,
    FlexContainerComponent,
    FlexColumnComponent,
    FlexRowComponent,
    HorizontalLineComponent,
    FilterBlockComponent,
    SortBlockComponent,
    FilterButtonComponent,
    PageContainerComponent,
    WrapRowComponent,
    CardThumbComponent,
    RowCardComponent,
    TabComponent,
    TabHeaderComponent,
    GridComponent,
    FlexComponent,
    TabsComponent,
    TabNavLinkComponent,
    OutletTabComponent,
    ModalWindowComponent,
    SpinnerComponent,
    PopupComponent,
    MenuComponent,
    MenuOptionComponent
  ]
})
export class UiKitModule { }