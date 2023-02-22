import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Weather } from 'app/models/weather';
import { catchError, map, Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class WeatherService {
  private targetUrl: string = `${environment.widgetApiBaseUrl}/weather`;

  constructor(private httpClient: HttpClient) {}

  // Get by q = city.
  getCurrentWeather(q: string): Observable<Weather> {
    return this.httpClient
      .get<Weather>(this.targetUrl, {
        params: {
          q: q,
        },
      })
      .pipe(
        map(
          (response: Weather) => {
            let weather: Weather = response;
            return weather;
          },
          catchError(async (err) => console.log(err))
        )
      );
  }
}
