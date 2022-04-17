import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Profile } from '../models/profile';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];
  public customers: Customer[];
  public profile: Profile;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<WeatherForecast[]>(baseUrl + 'weatherforecast').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));

    http.get<Profile>(baseUrl + 'profile/0').subscribe(result => {
      this.profile = result;
    }, error => console.error(error));

  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

interface Customer {
  сompanyName: string;
  address: string;
}

