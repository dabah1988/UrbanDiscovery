import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CitiesComponent } from './cities/cities.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { LoginComponent } from './login/login.component';
import { AddCityComponent } from './add-city/add-city.component';
import { UpdateCityComponent } from './update-city/update-city.component';
import { RegisterComponent } from './register/register.component';


const routes: Routes = [
  { path: 'cities', component: CitiesComponent },  // Route pour le composant CitiesComponent
  { path: 'welcome', component: WelcomeComponent },  // Route pour le composant WelcomeComponent
  { path: 'contactUs', component: ContactUsComponent },  // Route pour le composant WelcomeComponent
  { path: 'login', component: LoginComponent },  // Route pour le composant Login
  { path: 'addCity', component: AddCityComponent }, //Route pour ajouter une nouvelle ville
  { path: 'updateCity', component: UpdateCityComponent }, //Route pour ajouter une nouvelle ville
  { path: 'registerUser', component: RegisterComponent }, //Route pour ajouter une nouvelle ville

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
