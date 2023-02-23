import { Component, Input } from '@angular/core';
import { Weather } from 'app/models/weather';
import {Article} from "../../../models/article";

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
  news: Article;

  @Input()
  currentWeather: Weather;

  @Input()
  isLoading: boolean = false;
}
