import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from './base-service';
import { ILogItem } from '../interfaces/log-item';

@Injectable()

export class LogService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  logs(): Observable<ILogItem[]> {
    return this.http.get<ILogItem[]>(this.baseUrl + this.logsUrl);
  }
}
