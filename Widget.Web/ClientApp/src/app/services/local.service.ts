import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { HttpClient } from '@angular/common/http';
import { catchError, map, Observable } from 'rxjs';
import { LocalApplication } from 'app/models/local.application';
import { LocalFile } from 'app/models/local.file';
import { ResourceType } from 'app/enums/resource.type';

@Injectable({ providedIn: 'root' })
export class LocalService {
  private targetUrl: string = `${environment.widgetApiBaseUrl}/local`;

  constructor(private httpClient: HttpClient) {}

  // Get by q = search string, date, page, page size, and resource type.
  searchLocalApplications(
    q: string,
    from: Date,
    page: number,
    pageSize: number,
    resourceType: ResourceType
  ): Observable<Array<LocalApplication>> {
    return this.httpClient
      .get<Array<LocalApplication>>(this.targetUrl, {
        params: {
          q: q,
          from: from.toDateString(),
          page: page,
          pageSize: pageSize,
          resourceType: resourceType,
        },
      })
      .pipe(
        map(
          (response: Array<LocalApplication>) => {
            let localApplications: Array<LocalApplication> = response;
            return localApplications;
          },
          catchError(async (err) => console.log(err))
        )
      );
  }

  // Get by q = search string, date, page, page size, and resource type.
  searchLocalFiles(
    q: string,
    from: Date,
    page: number,
    pageSize: number,
    resourceType: ResourceType
  ): Observable<Array<LocalFile>> {
    return this.httpClient
      .get<Array<LocalFile>>(this.targetUrl, {
        params: {
          q: q,
          from: from.toDateString(),
          page: page,
          pageSize: pageSize,
          resourceType: resourceType,
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
