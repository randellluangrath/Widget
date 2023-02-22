import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import { Weather } from 'app/models/weather';
import { WeatherService } from 'app/services/weather.service';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class WeatherResolver implements Resolve<Observable<Weather>> {
  constructor(private weatherService: WeatherService) {}
  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<Weather> {
    return this.weatherService.getCurrentWeather('Nashville');
  }
}
