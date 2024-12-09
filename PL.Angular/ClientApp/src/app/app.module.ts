import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MatDialogModule } from "@angular/material/dialog";
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { HttpClient, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { NavFooterComponent } from './nav-footer/nav-footer.component';
import { PrivacyComponent } from './privacy/privacy.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component'; 
import { StoreComponent } from './store/store.component';
import { CartComponent } from './cart/cart.component';
import { OrderComponent } from './order/order.component';
import { ProductComponent } from './product/product.component';

import { StoreModule, MetaReducer, ActionReducer } from '@ngrx/store';
import { localStorageSync } from 'ngrx-store-localstorage';
import { storeFreeze } from 'ngrx-store-freeze'
import { userReducer } from './user/user.reducer';

const localStorageSyncReducer = (reducer: ActionReducer<any>): ActionReducer<any> =>
  localStorageSync({
    keys: ['user'],
    rehydrate: true,
  })(reducer);

const metaReducers: MetaReducer<any>[] = [localStorageSyncReducer, storeFreeze];

@NgModule({ declarations: [
        AppComponent,
        NavMenuComponent,
        NavFooterComponent,
        PrivacyComponent,
        LoginComponent,
        RegisterComponent,
        StoreComponent,
        CartComponent,
        OrderComponent,
        ProductComponent,
    ],
    bootstrap: [AppComponent], imports: [BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        FormsModule,
        RouterModule.forRoot([
            { path: 'privacy', component: PrivacyComponent },
            { path: 'register', component: RegisterComponent },
            { path: 'login', component: LoginComponent },
            { path: '', component: StoreComponent, pathMatch: 'full' },
            { path: 'cart', component: CartComponent },
            { path: 'order', component: OrderComponent },
            { path: 'product', component: ProductComponent },
        ]),
        MatDialogModule,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatInputModule,
        MatIconModule,
        MatButtonModule,
        MatMenuModule,
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: (http: HttpClient) => {
                    return new TranslateHttpLoader(http, './assets/i18n/', '.json');
                },
                deps: [HttpClient]
            }
        }),
        BrowserAnimationsModule,
        StoreModule.forRoot({ user: userReducer }, { metaReducers })], providers: [provideHttpClient(withInterceptorsFromDi())] })
export class AppModule { }
