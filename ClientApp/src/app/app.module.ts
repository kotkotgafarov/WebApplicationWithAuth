import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgxMaskModule, IConfig } from 'ngx-mask';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { EnrolleesComponent } from './enrollees/enrollees.component';
import { ProfileComponent } from './profile/profile.component';
import { ApplicationComponent, DialogApplicationComponent} from './application/application.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
// import { NgxDadataModule } from '@kolkov/ngx-dadata'

import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatDialogModule, MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';


export const options: Partial<IConfig> | (() => Partial<IConfig>) = null;


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ProfileComponent,
    ApplicationComponent,
    DialogApplicationComponent,
    EnrolleesComponent,
    CounterComponent,
    FetchDataComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    NgxMaskModule.forRoot(),
    HttpClientModule,
    FormsModule,
    MatAutocompleteModule,
    MatDialogModule,
    MatExpansionModule,
    MatButtonToggleModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    // NgxDadataModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'enrollees', component: EnrolleesComponent },
      { path: 'profile', component: ProfileComponent, canActivate: [AuthorizeGuard] },
      { path: 'profile/:id', component: ProfileComponent, canActivate: [AuthorizeGuard] },
      { path: 'application', component: ApplicationComponent, canActivate: [AuthorizeGuard] },
      { path: 'application/:id', component: ApplicationComponent, canActivate: [AuthorizeGuard] },
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
    ]),
    NoopAnimationsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  entryComponents: [DialogApplicationComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
