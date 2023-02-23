import { Component, Input } from '@angular/core';
import { Weather } from 'app/models/weather';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css'],
})
export class CardComponent {
  @Input()
  isNewsCard: boolean;

  @Input()
  isWeatherCard: boolean;

  @Input()
  headline: string = '';

  @Input()
  source: string = '';

  @Input()
  description: string = '';

  @Input()
  imageUrl: string = '';

  @Input()
  currentWeather: Weather;

  @Input()
  isLoading: boolean = false;
}
