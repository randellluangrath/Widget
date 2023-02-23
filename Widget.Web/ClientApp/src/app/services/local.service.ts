import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { HttpClient } from '@angular/common/http';
import { catchError, map, Observable } from 'rxjs';
import { LocalFile } from 'app/models/local.file';

@Injectable({ providedIn: 'root' })
export class LocalService {
  private targetUrl: string = `${environment.widgetApiBaseUrl}/local`;

  constructor(private httpClient: HttpClient) {}

  // Get by q = search string and page size.
  searchLocal(q: string, pageSize: number): Observable<Array<LocalFile>> {
    return this.httpClient
      .get<Array<LocalFile>>(this.targetUrl, {
        params: {
          q: q,
          pageSize: pageSize,
        },
      })
      .pipe(
        map(
          (response: Array<LocalFile>) => {
            let localFiles: Array<LocalFile> = response;
            return localFiles;
          },
          catchError(async (err) => console.log(err))
        )
      );
  }
}
