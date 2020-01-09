import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from './base-service';
import { IRankingItem } from '../interfaces/ranking-item';

@Injectable()

export class RankingService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  ranking(): Observable<IRankingItem[]> {
    return this.http.get<IRankingItem[]>(this.baseUrl + this.rankingUrl);
  }
}
