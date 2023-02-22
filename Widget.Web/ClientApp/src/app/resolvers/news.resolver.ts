import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import { Article } from 'app/models/article';
import { NewsService } from 'app/services/news.service';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class NewsResolver implements Resolve<Observable<Array<Article>>> {
  constructor(private newsService: NewsService) {}
  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<Array<Article>> {
    return this.newsService.getNews('Apple', new Date(), 1, 2);
  }
}
