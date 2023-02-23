import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Article } from 'app/models/article';
import { LocalFile } from 'app/models/local.file';
import { Weather } from 'app/models/weather';
import { LocalService } from 'app/services/local.service';
import { NewsService } from 'app/services/news.service';
import { WeatherService } from 'app/services/weather.service';
import {
  debounceTime,
  distinctUntilChanged,
  map,
  Observable,
  of,
  switchMap,
  tap,
} from 'rxjs';

@UntilDestroy()
@Component({
  selector: 'app-widget',
  templateUrl: './widget.component.html',
  styleUrls: ['./widget.component.css'],
})
export class WidgetComponent implements OnInit {
  currentWeather: Weather = {
    location: {
      name: '',
      region: '',
    },
    current: {
      last_updated: '',
      temp_f: '',
      condition: {
        text: '',
        icon: '',
        code: '',
      },
      wind_mph: '',
      wind_dir: '',
      humidity: '',
    },
  };

  news: Article = {
    source: { id: '', name: '' },
    title: '',
    description: '',
    urlToImage: '',
    url: '',
  };

  initialLocalFiles: LocalFile[] = [];
  localFiles: LocalFile[] = [];

  fcFilter: FormControl;
  fgFilter: FormGroup;

  localLoading: boolean = false;
  weatherLoading: boolean = true;
  newsLoading: boolean = true;

  constructor(
    private localService: LocalService,
    private weatherService: WeatherService,
    private newsService: NewsService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    // Build form.
    this.fcFilter = new FormControl(null);
    this.fgFilter = this.formBuilder.group({ filter: this.fcFilter });

    this.getRecentApplications()
      .pipe(
        untilDestroyed(this),
        tap(() => (this.localLoading = true))
      )
      .subscribe((x: Array<LocalFile>) => {
        this.localFiles = x;
        this.initialLocalFiles = this.localFiles;
        this.localLoading = false;
      });

    this.getCurrentWeather()
      .pipe(
        untilDestroyed(this),
        tap(() => (this.weatherLoading = true))
      )
      .subscribe((x: Weather) => {
        if (x)
          this.currentWeather = {
            location: {
              name: x.location.name,
              region: x.location.region,
            },
            current: {
              last_updated: x.current.last_updated.toString(),
              temp_f: x.current.temp_f.toString(),
              condition: {
                text: x.current.condition.text,
                icon: x.current.condition.icon,
                code: x.current.condition.code,
              },
              wind_mph: x.current.wind_mph.toString(),
              wind_dir: x.current.wind_dir.toString(),
              humidity: x.current.humidity.toString(),
            },
          };

        this.weatherLoading = false;
      });

    this.getNews()
      .pipe(
        untilDestroyed(this),
        tap(() => (this.newsLoading = true))
      )
      .subscribe((x: Array<Article>) => {
        const random = Math.floor(Math.random() * x.length);
        this.news = x[random];
        this.newsLoading = false;
      });

    this.fcFilter.valueChanges
      .pipe(
        distinctUntilChanged(),
        tap(() => (this.localLoading = true)),
        debounceTime(300),
        switchMap((x: string): Observable<Array<LocalFile>> => {
          // Minimum length is 3.
          if (!x) return of(this.initialLocalFiles);
          return this.searchLocal(x).pipe(
            tap((y: Array<LocalFile>) => {
              this.localFiles = y;
            })
          );
        }),
        untilDestroyed(this)
      )
      .subscribe(() => {
        this.localLoading = false;
      });
  }

  public onRemoveItemClicked(index: number): void {
    this.initialLocalFiles.splice(index, 1);
  }

  private getRecentApplications(): Observable<Array<LocalFile>> {
    return this.localService.searchLocal('', 10);
  }

  private getCurrentWeather(): Observable<Weather> {
    return this.weatherService.getCurrentWeather('Nashville');
  }

  private getNews(): Observable<Array<Article>> {
    return this.newsService.getNews('Education', 1, 50);
  }

  private searchLocal(q: string): Observable<Array<LocalFile>> {
    return this.localService.searchLocal(q, 10);
  }
}
