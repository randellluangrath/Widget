import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { HttpClient } from '@angular/common/http';
import { catchError, map, Observable } from 'rxjs';
import { Article } from 'app/models/article';

@Injectable({ providedIn: 'root' })
export class NewsService {
  private targetUrl: string = `${environment.widgetApiBaseUrl}/news`;

  constructor(private httpClient: HttpClient) {}

  // Get by q = category, date, page, and page size.
  getNews(
    q: string,
    from: Date,
    page: number,
    pageSize: number
  ): Observable<Array<Article>> {
    return this.httpClient
      .get<Array<Article>>(this.targetUrl, {
        params: {
          q: q,
          from: from.toDateString(),
          page: page,
          pageSize: pageSize,
        },
      })
      .pipe(
        map(
          (response: Array<Article>) => {
            let news: Array<Article> = response;
            return news;
          },
          catchError(async (err) => console.log(err))
        )
      );
  }
}
