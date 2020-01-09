import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from './base-service';
import { IGame } from '../interfaces/game';

@Injectable()

export class GameService extends BaseService {

  //public model: IGame;
  //public round: IRound;

  constructor(private http: HttpClient) {
    super();
  }

  game(model: IGame): Observable<IGame> {
    return this.http.post<IGame>(this.baseUrl + this.gameUrl, model);
  }

  round(model: IGame): Observable<IGame> {
    return this.http.post<IGame>(this.baseUrl + this.roundUrl, model);
  }
}
